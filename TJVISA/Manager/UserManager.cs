using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using TJVISA.BLL;
using TJVISA.Entity;

namespace TJVISA.Manager
{
    public class UserManager:IManager
    {
        protected UserBLL UserBLL { get; set; }

        public UserManager()
        {
            UserBLL=new UserBLL();
        }
        
        public IUser Create(string name,string password,UserRole? role)
        {
            return UserBLL.Create(name, password, role);
        }

        public IUser GetById(string id)
        {
            return UserBLL.Get(id);
        }

        public IUser Update(IUser item, string name,UserRole? role)
        {
            return UserBLL.Update(item, name, item.Password, role);
        }

        public IUser Authenticate(string name,string password)
        {
            return UserBLL.Get(name, password);
        }

        public IUser CreateAuthenticatedUser(string id, string name,UserRole? role)
        {
            return UserBLL.CreateAuthenticatedUser(id, name, role);
        }

        public IUser CreateSuperUser(string name)
        {
            return UserBLL.CreateSuperUser(name);
        }

        public IUser Delete(string id)
        {
            IUser user = UserBLL.Get(id);
            if (user != null)
                user= UserBLL.Delete(user);
            return user;
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            UserBLL = null;
        }

        #endregion

        #region Implementation of IModelManager

        public DataTable GetSource(IDictionary<IAttributeDefinition, object> criterias, PagingSetting pagingSetting)
        {
            int pageNum = pagingSetting.PageNum;
            int pageCount = pagingSetting.PageCount;
            int itemCount = pagingSetting.ItemCount;

            IList<IUser> users = UserBLL.GetAllPaged(new Dictionary<IAttributeDefinition, object>(), ref pageNum,
                                                     ref pageCount, pagingSetting.PageSize, ref itemCount);
            pagingSetting.PageNum = pageNum;
            pagingSetting.PageCount = pageCount;
            pagingSetting.ItemCount = itemCount;

            var od = AppCore.AppSingleton.FindObjDef<UserObjDef>();

            DataTable source=new DataTable("UserList");
            source.Columns.AddRange(new DataColumn[]
                                        {
                                            new DataColumn(od.ID.Name,typeof(System.String)),
                                            new DataColumn(od.Name.Name,typeof(System.String)),
                                            new DataColumn(od.Role.Name,typeof(System.String)) 
                                        });

            foreach (IUser user in users)
            {
                source.Rows.Add(new object[]
                                    {
                                        user.ID,
                                        user.Name,
                                        user.Role.GetValueOrDefault(UserRole.Client).ToLabel()
                                    });
            }

            return source;
        }

        #endregion
    }
}
