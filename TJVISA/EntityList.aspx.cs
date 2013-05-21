using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TJVISA.Entity;
using TJVISA.Manager;
using Telerik.Web.UI;

namespace TJVISA
{
    public partial class EntityList : TJVISA.BaseListPage
    {
        #region Overrides of BaseListPage

        protected override void ListDataBinding()
        {
                DataTable source;
                switch (EntityName)
                {
                    case "User":
                        using (var manager = new UserManager())
                        {
                            source = manager.GetSource(new Dictionary<IAttributeDefinition, object>(), PagingSetting);
                        }
                        break;
                    case "Application":
                        using (var manager = new ApplicationManager())
                        {
                            var criterias = new Dictionary<IAttributeDefinition, object>();
                            if(this.Current.IsLogon)
                                criterias.Add(AppCore.AppSingleton.FindObjDef<UserObjDef>().Role,this.Current.LogonUser.Role.GetValueOrDefault(UserRole.Client));
                            source = manager.GetSource(criterias, PagingSetting);
                        }
                        break;
                    case "Customer":
                        using (var manager = new CustomerManager())
                        {
                            source = manager.GetSource(new Dictionary<IAttributeDefinition, object>(), PagingSetting);
                        }
                        break;
                    default:
                        source = new DataTable("NoValue");
                        break;
                }

                ObjectList.MasterTableView.DataSource = source;
                ObjectList.MasterTableView.CurrentPageIndex = PagingSetting.PageNum;
                ObjectList.MasterTableView.VirtualItemCount = PagingSetting.ItemCount;
        }

        protected override bool ListDeleteItem(object sender, GridCommandEventArgs e)
        {
            try
            {
                var item = e.Item as GridDataItem;

                string id = (item.DataItem as DataRowView)["ID"].ToString();
                object result = null;

                switch (EntityName)
                {
                    case "User":
                        using (var manager = new UserManager())
                        {
                            result = manager.Delete(id);
                        }
                        break;
                    case "Application":
                        using (var manager = new ApplicationManager())
                        {
                            result = manager.Delete(id);
                        }
                        break;
                    default:
                        break;
                }

                if (result != null)
                {
                    ObjectList.DataBind();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected override void ListAddNewItem(object sender, GridCommandEventArgs e)
        {
            Response.Redirect(ObjectDefinition.DetailsPage);
        }

        protected override void ListRefresh(object sender, GridCommandEventArgs e)
        {
            ObjectList.Rebind();
        }

        #endregion

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
        }
    }
}
