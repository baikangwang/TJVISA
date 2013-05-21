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
    public partial class CaseDetails :BaseEditPage
    {
        private CaseManager _mgr;
        private ICase _item;

        public override string EntityName
        {
            get
            {
                return "Case";
            }
        }

        protected CaseManager Manager
        {
            get { return _mgr ?? (_mgr = new CaseManager()); }
        }

        protected ICase Item
        {
            get
            {
                if (_item == null)
                {
                    if (!string.IsNullOrEmpty(ItemId))
                        _item = Manager.GetById(ItemId);
                }
                return _item;
            }
        }

        private IApplication _appItem;
        protected IApplication AppItem
        {
            get
            {
                if (_appItem == null)
                {
                    if (!string.IsNullOrEmpty(ItemId))
                        _appItem = Manager.GetApplicationById(ItemId);
                }
                return _appItem;
            }
        }

        public override bool Save()
        {
            try
            {
                string text = txtValue.Content;
                string type = cmbType.SelectedValue;

                if (Item == null)
                {
                    Manager.Create(ItemId, type, text);

                    ScriptManager.RegisterStartupScript(this, typeof(Page), "ResetAppId", "Sys.Application.add_load(function() {ResetAppId('" + ItemId + "');return false;});", true);
                }
                else
                {
                    Manager.Update(Item, type, text);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected override void Initialize()
        {
            ReturnButton.Visible = false;

            if (this.Current.IsLogon)
            {
                switch (this.Current.LogonUser.Role)
                {
                    case UserRole.Business:
                        if (Item != null && Item.Application != null && Item.Application.Status != null)
                        {
                            string status = Item.Application.Status.Value;

                            if (status == JobStatus.Finish.ToLabel())
                                SaveButton.Visible = false;
                        }
                        else
                            SaveButton.Visible = true;
                        break;
                    default:
                        SaveButton.Visible = false;
                        break;
                }
            }

            if (string.IsNullOrEmpty(ItemId))
            {
                lbAppType.Text = GlobalConstant.None;
                lbCustomerID.Text = GlobalConstant.None;
                lbCustomerName.Text = GlobalConstant.None;
                lbRegion.Text = GlobalConstant.None;
            }
            else
            {
                lbAppType.Text = AppItem.Type;
                lbCustomerID.Text = AppItem.CustomerID;
                lbCustomerName.Text = AppItem.CustomerName;
                lbRegion.Text = AppItem.Region;
            }

            if (Item == null)
            {
                txtValue.Content = "";
                cmbType.SelectedIndex = 0;
            }
            else
            {
                txtValue.Content = Item.Text;
                cmbType.SelectedValue = Item.Type;
            }
        }
    }
}
