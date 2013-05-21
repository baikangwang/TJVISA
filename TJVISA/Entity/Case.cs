using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TJVISA.Entity;

namespace TJVISA.Entity
{


    public interface ICase : IBaseObject
    {
        IApplication Application { get; set; }
        //ICustomer Customer { get; set; }
        //string ID { get; }
        string AppType { get; }
        string CustomerName { get; }
        string CustomerID { get; }
        string Region { get; }
        string Type { get; set; }
        string Text { get; set; }
    }

    public class Case:BaseObject,ICase
    {
        #region Implementation of ICase

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
        public virtual string CustomerName { get { return Application.Customer.Name; } }

        public virtual string CustomerID
        {
            get { return Application.Customer.ID; }
        }
        public virtual string Region
        {
            get { return Application.Region; }
        }
        public virtual string Type { get; set; }

        public virtual string Text { get; set; }

        #endregion

        public override string ToString()
        {
            return string.Format("{0} | {1}", ID, Type);
        }

        protected Case(){}
    }

}

namespace TJVISA.DAL
{
    public sealed class CaseProxy : Case, IBaseObjectProxy
    {
        internal string AppId;
        internal IApplication App;
        private readonly CaseProxy _original;

        public override string ID
        {
            get
            {
                return AppId;
            }
        }
        
        private CaseProxy():base(){}

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
                                    string err = builder.AppendLine("初始化实体Case的Application属性错误！").AppendLine(ex).Message;
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

        public CaseProxy(string applicationId,string type,string text):this()
        {
            AppId = applicationId;
            Type = type;
            Text = text;

            _original=new CaseProxy()
                          {
                              AppId = applicationId,
                              Type = type,
                              Text = text
                          };
        }
        #region Implementation of IBaseObjectProxy<CaseProxy>

        public IBaseObjectProxy Original
        {
            get { return _original; }
        }

        #endregion
    }
}
