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
    public partial class ApplicationDetails : BaseEditPage
    {
        private ApplicationManager _mgr;
        private IApplication _item;

        public override string EntityName
        {
            get
            {
                return "Application";
            }
        }

        protected ApplicationManager Manager
        {
            get { return _mgr ?? (_mgr = new ApplicationManager()); }
        }

        protected IApplication Item
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

        public override bool Save()
        {
            try
            {
                var type = (AppType)int.Parse(cmbAppType.SelectedValue);
                var subType = (AppSubType)int.Parse(cmbSubType.SelectedValue);

                string customerId = txtCustomerID.Text;
                string customerName = txtCustomerName.Text;
                string customerSex = cmbCustomerSex.SelectedValue;
                string customerPhone = txtCustomerPhone.Text;
                string customerAddress = txtCustomerAddress.Text;

                string region = cmbRegion.SelectedValue;
                DateTime? dateApplied = dpkAppliedDate.SelectedDate;
                DateTime? dateTraved = dpkTravedDate.SelectedDate;
                string offNoteNo = txtOffNoteNo.Text;
                DateTime? dateOffNote = dpkOffNoteDate.SelectedDate;
                string remark = txtRemark.Content;

                string id;
                if (Item == null)
                {
                    id = new Identifier(type, subType).Value;
                    Manager.Create(id, customerId, customerName, customerSex,
                        customerPhone, customerAddress, region, dateApplied, dateTraved, offNoteNo, dateOffNote, remark);
                    
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "ResetAppId", "Sys.Application.add_load(function() {ResetAppId('" + id + "');return false;});", true);
                
                }
                else
                {
                    if (Item.Identifier.AppType != type)
                        Item.Identifier.UpdateAppType(type);
                    if (Item.Identifier.AppSubType != subType)
                        Item.Identifier.UpdateAppSubType(subType);

                    id = Item.ID;

                    Manager.Update(Item, id, Item.Customer, customerId, customerName, customerSex,
                        customerPhone, customerAddress, region, dateApplied, dateTraved, offNoteNo, dateOffNote, remark);
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
                    case UserRole.Client:
                        if (Item != null && Item.Status != null)
                        {
                            string status = Item.Status.Value;
                            if (status == JobStatus.End.ToLabel()
                                || status == JobStatus.Over.ToLabel())

                                SaveButton.Visible = false;
                        }
                        else
                        {
                            SaveButton.Visible = true;
                        }
                        break;
                    default:
                        SaveButton.Visible = false;
                        break;
                }
            }


            dpkAppliedDate.MinDate = DateTime.Now.AddYears(-100);
            dpkTravedDate.MinDate = DateTime.Now.AddYears(-100);
            dpkOffNoteDate.MinDate = DateTime.Now.AddYears(-100);

            if (string.IsNullOrEmpty(ItemId))
            {
                cmbAppType.SelectedIndex = 0;
                var type = (AppType)int.Parse(cmbAppType.SelectedValue);
                cmbSubType.Items.Clear();
                cmbSubType.Items.Add(new RadComboBoxItem(AppSubType.Single.ToLabel(type), (int)AppSubType.Single + ""));
                cmbSubType.Items.Add(new RadComboBoxItem(AppSubType.NonSingle.ToLabel(type), (int)AppSubType.NonSingle + ""));

                txtCustomerID.Text = "";
                txtCustomerName.Text = "";
                cmbCustomerSex.SelectedIndex = 0;
                txtCustomerPhone.Text = "";
                txtCustomerAddress.Text = "";

                cmbRegion.SelectedIndex = 0;

                dpkAppliedDate.SelectedDate = DateTime.Now;
                dpkTravedDate.SelectedDate = null;
                dpkOffNoteDate.SelectedDate = null;

                txtOffNoteNo.Text = "";
                txtRemark.Content = "";
            }
            else
            {
                cmbAppType.SelectedValue = Item.Type;
                var type = (AppType)int.Parse(cmbAppType.SelectedValue);
                cmbSubType.Items.Clear();
                cmbSubType.Items.Add(new RadComboBoxItem(AppSubType.Single.ToLabel(type), (int)AppSubType.Single + ""));
                cmbSubType.Items.Add(new RadComboBoxItem(AppSubType.NonSingle.ToLabel(type), (int)AppSubType.NonSingle + ""));

                txtCustomerID.Text = Item.CustomerID;
                txtCustomerName.Text = Item.CustomerName;
                cmbCustomerSex.SelectedValue = Item.CustomerSex;
                txtCustomerPhone.Text = Item.Customer.Phone;
                txtCustomerAddress.Text = Item.Customer.Address;

                cmbRegion.SelectedValue = Item.Region;

                dpkAppliedDate.SelectedDate = Item.DateApplied;
                dpkTravedDate.SelectedDate = Item.DateTraved;
                dpkOffNoteDate.SelectedDate = Item.OffNoteDate;

                txtOffNoteNo.Text = Item.OffNoteNo;
                txtRemark.Content = Item.Remark;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if(AjaxManager!=null)
            {
                AjaxManager.AjaxSettings.AddAjaxSetting(cmbAppType,cmbSubType);
            }
        }

        protected void AppType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var type = (AppType)int.Parse(e.Value);
            cmbSubType.Items.Clear();
            cmbSubType.Items.Add(new RadComboBoxItem(AppSubType.Single.ToLabel(type), (int)AppSubType.Single + ""));
            cmbSubType.Items.Add(new RadComboBoxItem(AppSubType.NonSingle.ToLabel(type), (int)AppSubType.NonSingle + ""));
        }
    }
}
