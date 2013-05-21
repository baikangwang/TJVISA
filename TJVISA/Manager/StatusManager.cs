using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TJVISA.BLL;
using TJVISA.Entity;

namespace TJVISA.Manager
{
    public class StatusManager : IDisposable, IManager
    {
        protected StatusBLL StatusBLL { get; set; }
        
        #region Implementation of IDisposable

        public StatusManager()
        {
            StatusBLL=new StatusBLL();
        }

        public void Dispose()
        {
            StatusBLL = null;
        }

        public DataTable GetSource(IDictionary<IAttributeDefinition, object> criterias, PagingSetting pageSetting)
        {
           return new DataTable("StatusList");
        }

        public IStatus Create(string appId, string value)
        {
            return StatusBLL.Create(appId, value);
        }

        public IStatus GetById(string id)
        {
            return StatusBLL.Get(id);
        }

        public IStatus Update(IStatus item, string value)
        {
            return StatusBLL.Update(item, value);
        }

        public IStatus Update(string appId,string value)
        {
            IStatus status = GetById(appId);
            status = Update(status, value);
            return status;
        }

        #endregion
    }
}
