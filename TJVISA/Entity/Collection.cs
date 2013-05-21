using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TJVISA.Entity;

namespace TJVISA.Entity
{
    public interface ICollection:IBaseObject
    {
        IApplication Application { get; set; }
        //ICustomer Customer { get; set; }
        //string ID { get; }
        string CustomerName { get; }
        decimal? Value { get; set; }
        decimal? Offset { get; set; }
    }

    public class Collection:BaseObject,ICollection
    {
        #region Implementation of ICollection

        public virtual IApplication Application { get; set; }
        //public virtual ICustomer Customer { get; set; }
        public override string ID
        {
            get { return Application.ID; }
        }
        public virtual string CustomerName
        {
            get { return Application.Customer.Name; }
        }
        public virtual decimal? Value { get; set; }
        public virtual decimal? Offset { get; set; }

        #endregion

        protected Collection(){}
    }

}

namespace TJVISA.DAL
{
    public sealed class CollectionProxy:Collection,IBaseObjectProxy
    {
        internal string AppId;
        internal IApplication App;
        private readonly CollectionProxy _original;

        public override string ID
        {
            get { return AppId; }
        }
        
        private CollectionProxy():base(){}

        public CollectionProxy(string appId,decimal? value,decimal? offset)
        {
            AppId = appId;
            Value = value;
            Offset = offset;

            _original=new CollectionProxy()
                          {
                              AppId = appId,
                              Value = value,
                              Offset = offset
                          };
        }

        public override IApplication Application
        {
            get
            {
                if (App == null || App.ID != AppId)
                    if (!string.IsNullOrEmpty(AppId))
                    {
                        using (var cmd = new DBCommand())
                        {
                            try
                            {
                                App = ApplicationDAL.Get(cmd, AppId);
                            }
                            catch (Exception ex)
                            {
                                using (var builder = new MessageBuilder())
                                {
                                    string err =
                                        builder.AppendLine("初始化实体Collection的Application属性错误！").AppendLine(ex).Message;
                                    throw new Exception(err);
                                }
                            }
                        }
                    }
                return App;
            }
            set
            {
                App = value;
                if (value != null)
                    AppId = value.ID;
            }
        }
        
        #region Implementation of IBaseObjectProxy<CollectionProxy>

        public IBaseObjectProxy Original
        {
            get { return _original; }
        }

        #endregion
    }
}
