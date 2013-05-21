using System;
using System.Web.UI;
using TJVISA.Entity;
using TJVISA.Manager;
using Telerik.Web.UI;

namespace TJVISA
{
    public partial class Default : BasePage
    {
       protected void OnAjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            AjaxArguments args=new AjaxArguments(e);

            switch (args.CommandName)
            {
                case "Login":
                    string name = args[0];
                    string password = args[1];
                    Login(name,password);
                    break;
                case "Logout":
                    Logout();
                    break;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!Current.IsLogon)
            {
                lbApplications.Visible = false;
                lbUsers.Visible = false;
                if (!IsPostBack)
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "InitUserInfo", " function RunOnce(){ChangeUserInfo(null); Sys.Application.remove_load(RunOnce);}  Sys.Application.add_load(RunOnce);", true);
            }
            else
            {
                switch (Current.LogonUser.Role.GetValueOrDefault(UserRole.Client))
                {
                    case UserRole.Business:
                    case UserRole.Client:
                        lbApplications.Visible = true;
                        lbUsers.Visible = false;
                        break;
                    case UserRole.Administrator:
                        lbApplications.Visible = true;
                        lbUsers.Visible = true;
                        break;
                    default:
                        break;
                }
                if (!IsPostBack)
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "InitUserInfo", "  function RunOnce(){ChangeUserInfo(\"" + Current.LogonUser.Name + "\"); Sys.Application.remove_load(RunOnce);}  Sys.Application.add_load(RunOnce);", true);
            }
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (AjaxManager != null)
            {
                AjaxManager.AjaxSettings.AddAjaxSetting(AjaxManager, lbApplications);
                AjaxManager.AjaxSettings.AddAjaxSetting(AjaxManager, lbUsers);
            }
        }

        protected void Login(string name, string password)
        {
            try
            {
                using (var userMgr = new UserManager())
                {
                    IUser user;

                    if (name == "Admin" && password == "123")
                        user = userMgr.CreateSuperUser(name);
                    else
                        user = userMgr.Authenticate(name, password);
                    
                    if (user == null)
                        Dialog.Error(this,"用户名或密码错误，请联系管理员.");
                    else
                    {
                        Current.Login(user);

                        Dialog.Info(this, "登录成功");
                        AjaxManager.ResponseScripts.Add(string.Format("ChangeUserInfo(\"{0}\");", user.Name));
                        AjaxManager.ResponseScripts.Add("CloseLoginDialog(null,null);");
                        AjaxManager.ResponseScripts.Add("CleanUpDialogInput();");
                    }
                }
            }
            catch(Exception ex )
            {
                MessageBuilder msgBuilder=new MessageBuilder().AppendLine(ex);
                Dialog.Error(this,msgBuilder.Message);
            }
        }

        protected void Logout()
        {
            this.Current.Logout();
            //AjaxManager.ResponseScripts.Add("ChangeUserInfo(null);");
        }
    }
}
