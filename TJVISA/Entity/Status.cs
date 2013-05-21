using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TJVISA.Entity;

namespace TJVISA.Entity
{
    public interface IStatus:IBaseObject
    {
        IApplication Application { get; set; }
        //ICustomer Customer { get; set; }
        //string ID { get; }
        string AppType { get; }
        string CustomerName { get; }
        string Value { get; set; }
    }
    
    public class Status:BaseObject,IStatus
    {
        #region Implementation of IStatus

        public virtual IApplication Application { get; set; }
        //public virtual ICustomer Customer { get; set; }
        public override string ID
        {
            get { return Application.ID; }
        }
        public virtual string AppType
        {
            get { return Application.Type; }
        }
        public virtual string CustomerName
        {
            get { return Application.Customer.Name; }
        }

        public virtual string Value { get; set; }

        #endregion

        protected Status(){}
    }
}

namespace TJVISA.DAL
{

    public sealed class StatusProxy:Status,IBaseObjectProxy
    {
        private readonly StatusProxy _original;
        internal string AppId;
        internal IApplication App;

        StatusProxy():base(){}

        public override string ID
        {
            get
            {
                return AppId;
            }
        }

        public StatusProxy(string appid,string value):this()
        {
            AppId = appid;
            Value = value;

            _original=new StatusProxy()
                          {
                              AppId = appid,
                              Value = value
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
                                    string err = builder.AppendLine("初始化实体Status的Application属性错误！").AppendLine(ex).Message;
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

        #region Implementation of IBaseObjectProxy<StatusProxy>

        public IBaseObjectProxy Original
        {
            get { return _original; }
        }

        #endregion
    }
}
