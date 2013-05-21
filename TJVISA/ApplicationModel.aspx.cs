using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJVISA.Entity;
using TJVISA.Manager;
using Telerik.Web.UI;

namespace TJVISA
{
    public partial class ApplicationModel : BasePage
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if(AjaxManager!=null)
            {
                AjaxManager.AjaxSettings.AddAjaxSetting(AjaxManager,btStatus);
                AjaxManager.AjaxSettings.AddAjaxSetting(AjaxManager,menuProcessStatus);
            }
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            AjaxManager.AjaxRequest += new RadAjaxControl.AjaxRequestDelegate(AjaxManager_AjaxRequest);

            if(string.IsNullOrEmpty(ItemId))
            {
                lbAppId.Text = GlobalConstant.None;
                btStatus.Visible = false;
            }
            else
            {
                lbAppId.Text = ItemId;
                btStatus.Visible = true;
            }
            

            if(!this.Current.IsLogon)return;

            //0:App details
            //1:collection
            //2:case
            //3:return
            switch (this.Current.LogonUser.Role)
            {
                case UserRole.Client:
                    RadPanelBar1.Items[1].Visible = true;
                    RadPanelBar1.Items[2].Visible = false;
                    break;
                case UserRole.Administrator:
                    RadPanelBar1.Items[1].Visible = true;
                    RadPanelBar1.Items[2].Visible = true;
                    break;
                case UserRole.Business:
                    RadPanelBar1.Items[1].Visible = false;
                    RadPanelBar1.Items[2].Visible = true;
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrEmpty(ItemId))
            {
                using (var manager = new ApplicationManager())
                {
                    IApplication item = manager.GetById(ItemId);

                    string curStatus = item.Status.Value;
                    InitializeStatusInput(curStatus);
                }
            }
        }

        private void InitializeStatusInput(string curStatus)
        {
            IList<JobStatus> status =
                this.Current.LogonUser.Role.GetValueOrDefault(UserRole.Client).GetProcessStatus(
                    curStatus.ToJobStatus());

            btStatus.Text = curStatus;
            menuProcessStatus.Items.Clear();
            if (status.Count == 0)
            {
                btStatus.EnableSplitButton = false;
            }
            else
            {
                btStatus.EnableSplitButton = true;
                menuProcessStatus.Items.AddRange(status.Select(s => new RadMenuItem(s.ToLabel())));
            }
        }

        protected void AjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            AjaxArguments args = new AjaxArguments(e);

            switch (args.CommandName)
            {
                case "ChangeStatus":
                    string status = args[0];
                    if (!string.IsNullOrEmpty(ItemId))
                    {
                        using (var manager = new StatusManager())
                        {
                            manager.Update(ItemId, status);
                            InitializeStatusInput(status);
                            Dialog.Info(this, "状态更新成功！");
                        }
                        //btStatus.Visible = true;
                    }
                    break;
                case "InitStatus":
                    string appid = args[0];
                    if (!string.IsNullOrEmpty(appid))
                    {
                        using (var manager = new ApplicationManager())
                        {
                            IApplication app = manager.GetById(appid);
                            if (app.Status != null)
                            {
                                InitializeStatusInput(app.Status.Value);
                            }
                        }
                        //btStatus.Visible = true;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
