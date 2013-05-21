using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TJVISA.Entity;

namespace TJVISA.DAL
{
    public class CustomerDAL
    {
        public static ICustomer Get(DBCommand cmd, string id)
        {
            return cmd.Get<ICustomer>("Customer",id);
        }

        public static ICustomer Create(DBCommand cmd, string id, string name, string sex, string phone, string address)
        {
            var od = AppCore.AppSingleton.FindObjDef<CustomerObjDef>();

            return cmd.Create<ICustomer>("Customer", new Dictionary<IAttributeDefinition, object>()
                                                         {
                                                             {od.PrimaryKey, id},
                                                             {od.Name, name},
                                                             {od.Sex, sex},
                                                             {od.Phone, phone},
                                                             {od.Address, address}
                                                         });
        }

        public static IList<ICustomer> GetAll(DBCommand cmd,IDictionary<IAttributeDefinition, object> criterias)
        {
            return cmd.GetAll<ICustomer>("Customer", criterias);
        }

        public static IList<ICustomer> GetAllPaged(DBCommand cmd, IDictionary<IAttributeDefinition, object> criterias, ref int pageNum, int pageSize, ref int pageCount, ref int itemCount)
        {
            return cmd.GetAllPaged<ICustomer>("Customer", criterias, ref pageCount, ref pageNum,ref itemCount, pageSize);
        }

        public static ICustomer Update(DBCommand cmd, ICustomer instance,string id, string name, string sex, string phone, string address)
        {
            instance.ID = id;
            instance.Name = name;
            instance.Sex = sex;
            instance.Phone = phone;
            instance.Address = address;

            return cmd.Update("Customer", instance);
        }

        public static ICustomer Delete(DBCommand cmd, ICustomer instance)
        {
            return cmd.Delete("Customer", instance);
        }

        public static bool IsExisting(DBCommand cmd, IDictionary<IAttributeDefinition, object> criterias)
        {
            int count = cmd.Count("Customer", criterias);

            return count > 0;
        }
    }
}
