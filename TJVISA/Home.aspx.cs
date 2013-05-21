using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJVISA.Entity;
using TJVISA.Manager;

namespace TJVISA
{
    public partial class Home : BasePage
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if(AjaxManager!=null)
            {
                AjaxManager.AjaxRequest += new Telerik.Web.UI.RadAjaxControl.AjaxRequestDelegate(AjaxManager_AjaxRequest);
            }
        }

        protected void AjaxManager_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            AjaxArguments args=new AjaxArguments(e);
            if(args.CommandName=="RunSearch")
            {
                string appId = args[0];
                string customerId = args[1];
                string result=null;
                
                using (var mgr=new ApplicationManager() )
                {
                    IApplication app = mgr.GetById(appId);
                    if(app!=null && app.CustomerID==customerId)
                    {
                        result = app.ID + "|" + app.DateApplied.GetValueOrDefault().ToString("yyyy年MM月dd日") + "|" +
                                 (app.Status == null
                                     ? JobStatus.None.ToLabel()
                                     : app.Status.Value);
                    }
                }

                AjaxManager.ResponseScripts.Add((result == null ? "ShowResult(null);" : "ShowResult('" + result + "');") +
                                                " return false;");
            }
        }
    }
}
