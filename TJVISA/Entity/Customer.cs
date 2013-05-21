using System;
using TJVISA.Entity;

namespace TJVISA.Entity
{
    public interface ICustomer:IBaseObject
    {
        //string ID { get; set; }
        string Name { get; set; }
        string Sex { get; set; }
        string Phone { get; set; }
        string Address { get; set; }
    }

    public class Customer:BaseObject,ICustomer
    {
        #region Implementation of ICustomer

        public override string ID{get; set;}

        public virtual string Name{get;set;}

        public virtual string Sex{get;set;}

        public virtual string Phone{get;set;}

        public virtual string Address{get;set;}

        #endregion

        public override string ToString()
        {
            return string.Format("{0} | {1} | {2}", Name, Sex, ID);
        }

        protected Customer(){}
    }
}

namespace TJVISA.DAL
{
    public sealed class CustomerProxy : Customer, IBaseObjectProxy
    {
        private readonly CustomerProxy _original;

        CustomerProxy() : base() { }

        public CustomerProxy(string id, string name, string sex,string phone,string address):this()
        {
            ID = id;
            Name = name;
            Sex = sex;
            Phone = phone;
            Address = address;

            _original = new CustomerProxy() { ID = id, Name = name, Sex = sex, Phone = phone, Address = address };
        }

        #region Implementation of IBaseObjectProxy<Customer>

        public IBaseObjectProxy Original
        {
            get { return _original; }
        }

        #endregion
    }
}
