using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TJVISA.BLL;
using TJVISA.Entity;

namespace TJVISA.Manager
{
    public class CollectionManager:IManager
    {
        private CollectionBLL _collectionBLL;
        private ApplicationBLL _applicationBLL;
        
        protected CollectionBLL CollectionBLL
        {
            get { return _collectionBLL; }
        }

        protected ApplicationBLL ApplicationBLL
        {
            get { return _applicationBLL; }
        }

        public CollectionManager()
        {
            _collectionBLL=new CollectionBLL();
            _applicationBLL=new ApplicationBLL();
        }
        
        #region Implementation of IDisposable

        public void Dispose()
        {
            _collectionBLL = null;
            _applicationBLL = null;
        }

        public DataTable GetSource(IDictionary<IAttributeDefinition, object> criterias, PagingSetting pageSetting)
        {
            return new DataTable("CollectionList");
        }

        #endregion

        public ICollection Create(string appId, decimal? value, decimal? offset)
        {
            return CollectionBLL.Create(appId, value, offset);
        }

        public ICollection GetById(string id)
        {
            return CollectionBLL.Get(id);
        }

        public ICollection Update(ICollection item, decimal? value, decimal? offset)
        {
            return CollectionBLL.Update(item,value,offset);
        }

        public IApplication GetApplicationById(string appId)
        {
            return ApplicationBLL.Get(appId);
        }
    }
}
