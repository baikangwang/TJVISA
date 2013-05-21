using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.Security;
using TJVISA.Entity;
using TJVISA.Manager;

namespace TJVISA
{
    
    public interface IAppCore
    {
        IDictionary<string, IObjectDefinition> ObjectDefinitions { get; }
        void Start();
        void End();
        T FindObjDef<T>() where T : class;
        IObjectDefinition FindObjDef(string entityName);
    }
    
    public class AppCore:IAppCore
    {
        private static AppCore _appInstance = null;
        
        public IDictionary<string, IObjectDefinition> ObjectDefinitions { get; protected set; }

        private AppCore()
        {
            ObjectDefinitions = new Dictionary<string, IObjectDefinition>();
            Start();
        }

        public static AppCore  AppSingleton
        {
            get { return _appInstance ?? (_appInstance = new AppCore()); }
        }

        public void Start()
        {
            ObjectDefinitions.Add("User", new UserObjDef("User", "User"));
            ObjectDefinitions.Add("Customer", new CustomerObjDef("Customer", "Client"));
            ObjectDefinitions.Add("Application", new ApplicationObjDef("Application", "Business"));
            ObjectDefinitions.Add("Collection", new CollectionObjDef("Collection", "Money"));
            ObjectDefinitions.Add("Case", new CaseObjDef("Case", "Problem"));
            ObjectDefinitions.Add("Status", new StatusObjDef("Status", "Process"));
        }

        public T FindObjDef<T>() where T: class
        {
            Type type = typeof (T);

            string name = type.Name;
            string entityName;
            switch (name)
            {
                case "ApplicationObjDef":
                    entityName = "Application";
                    break;
                case "UserObjDef":
                    entityName = "User";
                    break;
                case "CustomerObjDef":
                    entityName = "Customer";
                    break;
                case "CollectionObjDef":
                    entityName = "Collection";
                    break;
                case "CaseObjDef":
                    entityName = "Case";
                    break;
                case "StatusObjDef":
                    entityName = "Status";
                    break;
                default:
                    Debug.Assert(false,"Unknown entity object definition");
                    entityName = "";
                    break;
            }

            if (!ObjectDefinitions.ContainsKey(entityName))
                Debug.Assert(false, "The object definition of " + entityName + " entity was not initialized.");

            var od = ObjectDefinitions[entityName] as T;
            if (od == null)
                Debug.Assert(false, "The object definition of " + entityName + " entity was initialized uncorrect.");

            return od;
        }

        public IObjectDefinition FindObjDef(string entityName)
        {
            if (!ObjectDefinitions.ContainsKey(entityName))
                Debug.Assert(false, "The object definition of " + entityName + " entity was not initialized.");
            
            return ObjectDefinitions[entityName];
        }

        public void End()
        {
            ObjectDefinitions.Clear();
        }
    }

    public interface ISession
    {
        IUser LogonUser { get; }
        void Login(IUser user);
        void Logout();
        bool IsLogon { get; }
    }

    public class Session:ISession
    {
        public IUser LogonUser { get { return HttpContext.Current.User as IUser; } }

        private static ISession _instance;

        public static ISession Singleton
        {
            get { return _instance ?? (_instance = new Session()); }
        }
        
        public Session()
        {
            StarUp();
        }

        protected void StarUp()
        {
            IUser user = null;
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[GlobalConstant.CookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null)
                {
                    try
                    {
                        string[] userInfo = authTicket.UserData.Split('|');
                        if (userInfo.Length == 3)
                        {
                            string id = userInfo[0];
                            string name = userInfo[1];
                            UserRole? role = string.IsNullOrEmpty(userInfo[2])
                                                 ? UserRole.Client
                                                 : (UserRole)int.Parse(userInfo[2]);

                            using (var userManager = new UserManager())
                            {
                                user = userManager.CreateAuthenticatedUser(id, name, role);
                            }
                        }

                    }
                    catch
                    {
                        user = null;
                    }
                }
            }

            HttpContext.Current.User = user;
        }

        public void Login(IUser user)
        {
            string userInfo = string.Format("{0}|{1}|{2}", user.ID, user.Name,
                                            (int)user.Role.GetValueOrDefault(UserRole.Business));

            var authTicket = new FormsAuthenticationTicket(1, user.Name,
                                                           DateTime.Now,
                                                           DateTime.Now.AddYears(1),
                                                           true, userInfo,
                                                           FormsAuthentication.
                                                               FormsCookiePath);
            //Encrypt custom data
            string encryptiedTicket = FormsAuthentication.Encrypt(authTicket);

            HttpCookie authCookie = HttpContext.Current.Request.Cookies[GlobalConstant.CookieName];
            if (authCookie != null)
            {
                authCookie.Value = encryptiedTicket;
                authCookie.Expires = authTicket.Expiration;
                HttpContext.Current.Response.Cookies.Set(authCookie);
            }
            else
            {
                authCookie = new HttpCookie(GlobalConstant.CookieName, encryptiedTicket) { Expires = authTicket.Expiration };
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }

            HttpContext.Current.User = user;
        }

        public void Logout()
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[GlobalConstant.CookieName];
            if (authCookie != null)
            {
                authCookie.Expires = DateTime.Now.AddYears(-1);
                HttpContext.Current.Response.Cookies.Set(authCookie);
            }
            HttpContext.Current.User = null;
        }

        public bool IsLogon
        {
            get { return LogonUser != null; }
        }

        public void CleanUp()
        {
            HttpContext.Current.User = null;
        }
    }
}
