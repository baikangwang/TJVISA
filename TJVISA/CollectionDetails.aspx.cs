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
    public partial class CollectionDetails : BaseEditPage
    {
        private CollectionManager _mgr;
        private ICollection _item;

        public override string EntityName
        {
            get
            {
                return "Collection";
            }
        }

        protected CollectionManager Manager
        {
            get { return _mgr ?? (_mgr = new CollectionManager()); }
        }

        protected ICollection Item
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
                decimal? value = (decimal)txtValue.Value.GetValueOrDefault(0);
                decimal? offset = (decimal)txtOffset.Value.GetValueOrDefault(0);

                if (Item == null)
                {
                    Manager.Create(ItemId, value, offset);
                }
                else
                {
                    Manager.Update(Item, value, offset);
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
                        if (Item != null && Item.Application != null && Item.Application.Status != null)
                        {
                            string status = Item.Application.Status.Value;
                            if (status == JobStatus.End.ToLabel())
                                SaveButton.Visible = false;
                        }
                        else
                            SaveButton.Visible = !string.IsNullOrEmpty(ItemId);
                        break;
                    default:
                        SaveButton.Visible = false;
                        break;
                }
            }

            if (string.IsNullOrEmpty(ItemId))
            {
                lbCustomerName.Text = GlobalConstant.None;
            }
            else
            {
                lbCustomerName.Text = AppItem.CustomerName;
            }

            txtValue.MaxValue = double.MaxValue;
            txtValue.MinValue = 0;
            txtOffset.MaxValue = double.MaxValue;
            txtOffset.MinValue = 0;

            if (Item == null)
            {
                txtValue.Value = 0;
                txtOffset.Value = 0;
            }
            else
            {
                txtValue.Value = (double)Item.Value.GetValueOrDefault(0);
                txtOffset.Value = (double)Item.Offset.GetValueOrDefault(0);
            }
        }
    }
}
