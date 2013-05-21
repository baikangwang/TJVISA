using System;
using System.Web.UI;
using TJVISA.Entity;
using Telerik.Web.UI;

namespace TJVISA
{
    public abstract class BasePage:Page
    {
        private ISession _session;
        
        public ISession Current
        {
            get { return _session ?? (_session = new Session()); }
        }

        private RadAjaxManager _ajaxMgr;
        public virtual RadAjaxManager AjaxManager
        {
            get { return _ajaxMgr?? (_ajaxMgr= RadAjaxManager.GetCurrent(this)); }
        }

        private string _entityName;
        public virtual string EntityName
        {
            get { return _entityName?? (_entityName=this.Request.Params[GlobalConstant.EntityName]); }
        }

        private string _itemId;

        public virtual string ItemId
        {
            get { return _itemId ?? (_itemId = this.Request.Params[GlobalConstant.ItemId]); }
        }


        private IObjectDefinition _od;
        public virtual IObjectDefinition ObjectDefinition
        {
            get
            {
                if (_od == null)
                {
                    if (!string.IsNullOrEmpty(EntityName))
                        _od = AppCore.AppSingleton.FindObjDef(EntityName);
                }
                return _od;
            }
        }
    }
}
