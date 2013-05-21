using System;
using TJVISA.DAL;
using TJVISA.Entity;

namespace TJVISA.Entity
{
    public interface IApplication : IBaseObject
    {
        Identifier Identifier { get; set; }
        ICustomer Customer { get; set; }
        string Type { get; set; }
        string SubType { get; set; }
        string CustomerName { get; set; }
        string CustomerSex { get; set; }
        string CustomerID { get; set; }
        string Region { get; set; }
        DateTime? DateApplied { get; set; }
        DateTime? DateTraved { get; set; }
        string OffNoteNo { get; set; }
        DateTime? OffNoteDate { get; set; }
        string Remark { get; set; }
        IStatus Status { get; }
    }

    public class Application : BaseObject, IApplication
    {
        #region Implementation of IApplication

        public virtual string Type
        {
            get { return Identifier.AppType.ToLabel(); }
            set { Identifier.UpdateAppType(value); }
        }

        public virtual string SubType
        {
            get { return Identifier.AppSubType.ToLabel(Identifier.AppType); }
            set { Identifier.UpdateAppSubType(value); }
        }

        public virtual string CustomerName
        {
            get { return Customer.Name; }
            set { Customer.Name = value; }
        }

        public virtual string CustomerSex
        {
            get { return Customer.Sex; }
            set { Customer.Name = value; }
        }

        public virtual string CustomerID
        {
            get { return Customer.ID; }
            set { Customer.ID = value; }
        }

        public virtual Identifier Identifier { get; set; }
        public virtual ICustomer Customer { get; set; }
        public virtual IStatus Status { get; protected set; }

        public override string ID
        {
            get { return Identifier.Value; }
            set { Identifier = Identifier.Load(value); }
        }

        public virtual string Region { get; set; }
        public virtual DateTime? DateApplied { get; set; }
        public virtual DateTime? DateTraved { get; set; }
        public virtual string OffNoteNo { get; set; }
        public virtual DateTime? OffNoteDate { get; set; }
        public virtual string Remark { get; set; }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}", ID);
        }

        protected Application()
        {
        }
    }

}

namespace TJVISA.DAL
{
    public sealed class ApplicationProxy : Application, IBaseObjectProxy
    {
        internal string customerId;
        internal ICustomer customer;

        internal IStatus status;
        private readonly ApplicationProxy _original;

        public override ICustomer Customer
        {
            get
            {
                if (customer == null || customer.ID != customerId)
                    if (!string.IsNullOrEmpty(customerId))
                    {
                        using (var cmd = new DBCommand())
                        {
                            try
                            {
                                customer = CustomerDAL.Get(cmd, customerId);
                            }
                            catch (Exception ex)
                            {
                                using (var builder = new MessageBuilder())
                                {
                                    string err = builder.AppendLine("初始化实体Application的Customer属性错误！").AppendLine(ex).Message;
                                    throw new Exception(err);
                                }
                            }
                        }
                    }

                return customer;
            }
            set
            {
                customer = value;
                if (value != null)
                    customerId = value.ID;
            }
        }

        public override IStatus Status
        {
            get
            {
                if (status == null)
                    if (!string.IsNullOrEmpty(ID))
                    {
                        using (var cmd = new DBCommand())
                        {
                            try
                            {
                                status = StatusDAL.Get(cmd, ID);
                            }
                            catch (Exception ex)
                            {
                                using (var builder = new MessageBuilder())
                                {
                                    string err = builder.AppendLine("初始化实体Application的Customer属性错误！").AppendLine(ex).Message;
                                    throw new Exception(err);
                                }
                            }
                        }
                    }

                return status;
            }
        }

        private ApplicationProxy() : base()
        {
        }

        public ApplicationProxy(string id, string customerId, string region,
                                DateTime? dateApplied, DateTime? dateTraved, string offNoteNo, DateTime? offNoteDate,
                                string remark)
        {
            ID = id;
            this.customerId = customerId;
            Region = region;
            DateApplied = dateApplied;
            DateTraved = dateTraved;
            OffNoteDate = offNoteDate;
            OffNoteNo = offNoteNo;
            Remark = remark;

            _original = new ApplicationProxy()
                            {
                                ID = id,
                                customerId = customerId,
                                Region = region,
                                DateApplied = dateApplied,
                                DateTraved = dateTraved,
                                OffNoteDate = offNoteDate,
                                OffNoteNo = offNoteNo,
                                Remark = remark
                            };
        }

        #region Implementation of IBaseObjectProxy<Application>

        public IBaseObjectProxy Original
        {
            get { return _original; }
        }

        #endregion
    }
}
