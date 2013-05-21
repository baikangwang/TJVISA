using System.Collections.Generic;
using System.Data;
using TJVISA.BLL;
using TJVISA.Entity;

namespace TJVISA.Manager
{
    public class CaseManager:IManager
    {
        private CaseBLL _caseBLL;
        private ApplicationBLL _applicationBLL;

        protected StatusBLL StatusBLL { get; set; }
        protected CaseBLL CaseBLL
        {
            get { return _caseBLL; }
        }

        protected ApplicationBLL ApplicationBLL
        {
            get { return _applicationBLL; }
        }

        public CaseManager()
        {
            _caseBLL=new CaseBLL();
            _applicationBLL=new ApplicationBLL();
            StatusBLL=new StatusBLL();
        }
        
        #region Implementation of IDisposable

        public void Dispose()
        {
            _caseBLL = null;
            _applicationBLL = null;
            StatusBLL = null;
        }

        public DataTable GetSource(IDictionary<IAttributeDefinition, object> criterias, PagingSetting pagingSetting)
        {
            return new DataTable("CaseList");
        }

        #endregion

        public ICase Create(string appId, string type, string text)
        {
            IStatus status = StatusBLL.Get(appId);

            status = status == null
                         ? StatusBLL.Create(appId, JobStatus.HasCase.ToLabel())
                         : StatusBLL.Update(status, JobStatus.HasCase.ToLabel());

            return CaseBLL.Create(appId,type,text);
        }

        public ICase GetById(string id)
        {
            return CaseBLL.Get(id);
        }

        public ICase Update(ICase item, string type, string text)
        {
            return CaseBLL.Update(item,type,text);
        }

        public IApplication GetApplicationById(string appId)
        {
            return ApplicationBLL.Get(appId);
        }
    }
}
