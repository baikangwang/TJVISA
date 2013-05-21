using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJVISA.Entity;
using Telerik.Web.UI;

namespace TJVISA
{
    public abstract class BaseListPage:BasePage
    {
        protected RadGrid ObjectList { get; set; }

        protected PagingSetting PagingSetting { get; set; }

        protected BaseListPage()
        {
            PagingSetting = new PagingSetting(1, 10, 1, 10);
        }

        protected bool CanAddNew
        {
            get
            {
                if (!Current.IsLogon)
                    return false;
                else
                {
                    UserRole role = Current.LogonUser.Role.GetValueOrDefault(UserRole.Client);

                    switch (this.EntityName)
                    {
                        case "Application":
                            return role == UserRole.Client;
                        case "User":
                            return role == UserRole.Administrator;
                                default:
                            return false;
                    }
                }
            }
        }

        protected bool CanDelete
        {
            get { return CanAddNew; }
        }

        protected ContentPlaceHolder ListContainer
        {
            get
            {
                if(this.Master is TJVISA.List)
                {
                    return (this.Master as TJVISA.List).ListContainer;
                }
                return null;
            }
        }
        
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            //RadButton addNew = new RadButton()
            //{
            //    ID = "btAddNew",
            //    ButtonType = RadButtonType.LinkButton,
            //    NavigateUrl = string.Format("{0}", ObjectDefinition.DetailsPage),
            //    Text = "新建"
            //};

            //ListContainer.Controls.Add(addNew);
            ObjectList = new RadGrid
            {
                ID = "objectList",
                AllowPaging = true,
                AllowCustomPaging = true,
                AllowAutomaticDeletes = false,
                AllowAutomaticInserts = false,
                AllowAutomaticUpdates = false,
                AllowFilteringByColumn = false,
                AllowMultiRowEdit = false,
                AllowMultiRowSelection = false,
                AllowSorting = false,
                Width = Unit.Percentage(99.8),
                Height = Unit.Percentage(100),
                EnableHeaderContextAggregatesMenu = false,
                EnableHeaderContextFilterMenu = false,
                EnableHeaderContextMenu = false,
                EnableLinqExpressions = false,
                EnableViewState = true,
                GroupingEnabled = false,
                ShowFooter = true,
                ShowGroupPanel = false,
                ShowHeader = true,
                ShowStatusBar = true,
                AutoGenerateColumns = false,
                AutoGenerateEditColumn = false,
                AutoGenerateHierarchy = false,
            };
            ObjectList.ClientSettings.AllowColumnHide = false;
            ObjectList.ClientSettings.AllowAutoScrollOnDragDrop = false;
            ObjectList.ClientSettings.AllowColumnsReorder = false;
            ObjectList.ClientSettings.AllowDragToGroup = false;
            ObjectList.ClientSettings.AllowExpandCollapse = false;
            ObjectList.ClientSettings.AllowGroupExpandCollapse = false;
            ObjectList.ClientSettings.AllowKeyboardNavigation = false;
            ObjectList.ClientSettings.AllowRowHide = false;
            ObjectList.ClientSettings.AllowRowsDragDrop = false;
            ObjectList.ClientSettings.EnableAlternatingItems = true;
            ObjectList.ClientSettings.EnablePostBackOnRowClick = false;
            ObjectList.ClientSettings.EnableRowHoverStyle = true;
            ObjectList.PageSize = PagingSetting.PageSize;
            ObjectList.MasterTableView.NoMasterRecordsText = "没有记录";
            ObjectList.MasterTableView.DataKeyNames = new string[] { "ID" };

            ObjectList.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.TopAndBottom;
            ObjectList.MasterTableView.CommandItemSettings.ShowRefreshButton = true;
            ObjectList.MasterTableView.CommandItemSettings.RefreshText = "刷新";

            ObjectList.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = CanAddNew;
            ObjectList.MasterTableView.CommandItemSettings.AddNewRecordText = "新建";
            
            ObjectList.DataBinding += new EventHandler(ObjectList_DataBinding);
            ObjectList.DataBound += new EventHandler(ObjectList_DataBound);
            ObjectList.PageSizeChanged += new GridPageSizeChangedEventHandler(ObjectList_PageSizeChanged);
            ObjectList.PageIndexChanged += new GridPageChangedEventHandler(ObjectList_PageIndexChanged);
            ObjectList.ItemCommand += new GridCommandEventHandler(ObjectList_ItemCommand);

            if (ObjectDefinition == null)
                Debug.Assert(false, "entity object definition was not initialized.");

            ObjectList.Columns.Clear();

            if(CanDelete)
            {
                ObjectList.DeleteCommand += new GridCommandEventHandler(ObjectList_DeleteCommand);
                ObjectList.Columns.Add(
                    new GridButtonColumn()
                    {
                        UniqueName = "DeleteColumn",
                        ButtonType = GridButtonColumnType.ImageButton,
                        CommandName = RadGrid.DeleteCommandName,
                        Text = "删除",
                        ConfirmDialogHeight = Unit.Pixel(100),
                        ConfirmDialogType = GridConfirmDialogType.RadWindow,
                        ConfirmText = "确定要删除该记录吗？",
                    });
            }

            foreach (IAttributeDefinition attr in ObjectDefinition.Attributes.Values.OrderBy(a => a.OrderIndex))
            {
                if (attr == ObjectDefinition.PrimaryKey)
                {
                    if (attr.ForObject.EntityName == "User")
                        continue;

                    ObjectList.Columns.Add(new GridHyperLinkColumn()
                    {
                        UniqueName = "ID",
                        DataNavigateUrlFields = new string[] { attr.Name },
                        DataNavigateUrlFormatString =
                            ObjectDefinition.DetailsPage + "?" + GlobalConstant.ItemId +
                            "={0}",
                        DataTextField = attr.Name,
                        DataType = typeof(System.String),
                        HeaderText = attr.Label
                    });
                }
                else
                {
                    if (attr.ForObject.EntityName == "User" && attr.Name == "Name")
                    {
                        ObjectList.Columns.Add(new GridHyperLinkColumn()
                        {
                            UniqueName = "ID",
                            DataNavigateUrlFields = new string[] { attr.ForObject.PrimaryKey.Name },
                            DataNavigateUrlFormatString =
                                ObjectDefinition.DetailsPage + "?" + GlobalConstant.ItemId +
                                "={0}",
                            DataTextField = attr.Name,
                            DataType = typeof(System.String),
                            HeaderText = attr.Label
                        });
                        continue;
                    }

                    if (!attr.ShowInList) continue;

                    ObjectList.Columns.Add(new GridBoundColumn()
                    {
                        ReadOnly = true,
                        UniqueName = attr.Name,
                        DataType = typeof(System.String),
                        HeaderText = attr.Label,
                        DataField = attr.Name
                    });
                }
            }

            if(EntityName=="Application")
            {
                var status = AppCore.AppSingleton.FindObjDef<StatusObjDef>().Value;
                
                ObjectList.Columns.Add(new GridBoundColumn()
                {
                    ReadOnly = true,
                    UniqueName = status.Name,
                    DataType = typeof(System.String),
                    HeaderText = status.Label,
                    DataField = status.Name
                });
            }

            if (ListContainer != null)
            {
                ListContainer.Controls.Add(ObjectList);
                ObjectList.DataBind();
                if(AjaxManager!=null)
                {
                    AjaxManager.AjaxSettings.AddAjaxSetting(ObjectList, ObjectList);
                }
            }
        }


        protected virtual void ListDataBound(){}
        protected abstract void ListDataBinding();
        protected abstract void ListAddNewItem(object sender, GridCommandEventArgs e);
        protected abstract void ListRefresh(object sender, GridCommandEventArgs e);
        protected abstract bool ListDeleteItem(object sender, GridCommandEventArgs e);

        protected void ObjectList_DataBinding(object sender, EventArgs e)
        {
            try
            {
                ListDataBinding();
            }
            catch (Exception ex)
            {
                Dialog.Error(this, new MessageBuilder().AppendLine("读取记录出错！").AppendLine(ex));
            }
        }

        protected void ObjectList_DataBound(object sender, EventArgs e)
        {
            try
            {
                ListDataBound();
            }
            catch (Exception ex)
            {
                Dialog.Error(this, new MessageBuilder().AppendLine("读取记录出错！").AppendLine(ex));
            }
        }

        protected void ObjectList_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                Dialog.Info(this, ListDeleteItem(sender, e) ? "删除成功！" : "删除失败！");
            }
            catch (Exception ex)
            {
                Dialog.Error(this, new MessageBuilder().AppendLine("删除失败！").AppendLine(ex));
            }
        }

        protected void ObjectList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case RadGrid.InitInsertCommandName:
                        ListAddNewItem(sender, e);
                        break;
                    case RadGrid.RebindGridCommandName:
                        ListRefresh(sender, e);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Dialog.Error(this, new MessageBuilder().AppendLine("操作出错！").AppendLine(ex));
            }
            
        }

        protected virtual void ObjectList_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            PagingSetting.PageNum = e.NewPageIndex;
        }
        
        protected virtual void ObjectList_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            PagingSetting.PageSize = e.NewPageSize;
        }

        protected override object SaveControlState()
        {
            object obj = base.SaveControlState();
            if (obj == null)
            {
                return PagingSetting;
            }
            else
            {
                return new Pair(obj, PagingSetting);
            }
        }
        
        protected override void LoadControlState(object savedState)
        {
            if (savedState is PagingSetting)
            {
                PagingSetting = savedState as PagingSetting;
            }
            else if (savedState is Pair)
            {
                Pair p = savedState as Pair;
                base.LoadControlState(p.First);
                PagingSetting = p.Second as PagingSetting;
            }
            else
            {
                base.LoadControlState(savedState);
            }
        }
    }
}
