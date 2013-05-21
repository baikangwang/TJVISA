using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using TJVISA.Entity;

namespace TJVISA.DAL
{
    public static class EntityCreator
    {
        public static T Create<T>(string entityName,IDictionary<IAttributeDefinition,object> values ) where T:class 
        {
            switch (entityName)
            {
                case "Application":
                    return CreateApplication(values) as T;
                case "Customer":
                    return CreateCustomer(values) as T;
                case "User":
                    return CreateUser(values) as T;
                case "Case":
                   return CreateCase(values) as T;
                case "Collection":
                    return CreateCollection(values) as T;
                case "Status":
                    return CreateStatus(values) as T;
                default:
                    Debug.Assert(false,"Didn't implement entity creation for the entity "+entityName);
                    return null;
            }
        }

        private static IApplication CreateApplication(IDictionary<IAttributeDefinition, object> values)
        {
            var od = AppCore.AppSingleton.FindObjDef<ApplicationObjDef>();

            if (!values.ContainsKey(od.PrimaryKey))
                Debug.Assert(false, "the argument of attribute [" + od.PrimaryKey.Name + "] of entity [" + od.EntityName + "] was provided.");
            string id = GetString(values[od.PrimaryKey]);

            if (!values.ContainsKey(od.CustomerID))
                Debug.Assert(false, "the argument of attribute [" + od.CustomerID.Name + "] of entity [" + od.EntityName + "] was provided.");
            string customerId = GetString(values[od.CustomerID]);

            if (!values.ContainsKey(od.Region))
                Debug.Assert(false, "the argument of attribute [" + od.Region.Name + "] of entity [" + od.EntityName + "] was provided.");
            string region = GetString(values[od.Region]);

            if (!values.ContainsKey(od.DateApplied))
                Debug.Assert(false, "the argument of attribute [" + od.DateApplied.Name + "] of entity [" + od.EntityName + "] was provided.");
            DateTime? dateApplied = GetValue<DateTime>(values[od.DateApplied]);

            if (!values.ContainsKey(od.DateTraved))
                Debug.Assert(false, "the argument of attribute [" + od.DateTraved.Name + "] of entity [" + od.EntityName + "] was provided.");
            DateTime? dateTraved = GetValue<DateTime>(values[od.DateTraved]);

            if (!values.ContainsKey(od.OffNoteNo))
                Debug.Assert(false, "the argument of attribute [" + od.OffNoteNo.Name + "] of entity [" + od.EntityName + "] was provided.");
            string offNoteNo = GetString(values[od.OffNoteNo]);

            if (!values.ContainsKey(od.OffNoteDate))
                Debug.Assert(false, "the argument of attribute [" + od.OffNoteDate.Name + "] of entity [" + od.EntityName + "] was provided.");
            DateTime? offNoteDate = GetValue<DateTime>(values[od.OffNoteDate]);

            if (!values.ContainsKey(od.Remark))
                Debug.Assert(false, "the argument of attribute [" + od.Remark.Name + "] of entity [" + od.EntityName + "] was provided.");
            string remark = GetString(values[od.Remark]);

            return new ApplicationProxy(id,customerId,region,dateApplied,dateTraved,offNoteNo,offNoteDate,remark);
        }

        private static ICustomer CreateCustomer(IDictionary<IAttributeDefinition, object> values)
        {
            var od = AppCore.AppSingleton.FindObjDef<CustomerObjDef>();

            if (!values.ContainsKey(od.PrimaryKey))
                Debug.Assert(false, "the argument of attribute [" + od.PrimaryKey.Name + "] of entity [" + od.EntityName + "] was provided.");
            string id = GetString(values[od.PrimaryKey]);

            if (!values.ContainsKey(od.Name))
                Debug.Assert(false, "the argument of attribute [" + od.Name.Name + "] of entity [" + od.EntityName + "] was provided.");
            string name = GetString(values[od.Name]);

            if (!values.ContainsKey(od.Sex))
                Debug.Assert(false, "the argument of attribute [" + od.Sex.Name + "] of entity [" + od.EntityName + "] was provided.");
            string sex = GetString(values[od.Sex]);

            if (!values.ContainsKey(od.Phone))
                Debug.Assert(false, "the argument of attribute [" + od.Phone.Name + "] of entity [" + od.EntityName + "] was provided.");
            string phone = GetString(values[od.Phone]);

            if (!values.ContainsKey(od.Address))
                Debug.Assert(false, "the argument of attribute [" + od.Address.Name + "] of entity [" + od.EntityName + "] was provided.");
            string address = GetString(values[od.Address]);

            return new CustomerProxy(id,name,sex,phone,address);
        }

        private static IUser CreateUser(IDictionary<IAttributeDefinition, object> values)
        {
            var od = AppCore.AppSingleton.FindObjDef<UserObjDef>();

            if (!values.ContainsKey(od.PrimaryKey))
                Debug.Assert(false, "the argument of attribute [" + od.PrimaryKey.Name + "] of entity [" + od.EntityName + "] was provided.");
            string id = GetString(values[od.PrimaryKey]);

            if (!values.ContainsKey(od.Name))
                Debug.Assert(false, "the argument of attribute [" + od.Name.Name + "] of entity [" + od.EntityName + "] was provided.");
            string userName = GetString(values[od.Name]);

            if (!values.ContainsKey(od.Password))
                Debug.Assert(false, "the argument of attribute [" + od.Password.Name + "] of entity [" + od.EntityName + "] was provided.");
            string password = GetString(values[od.Password]);

            if (!values.ContainsKey(od.Role))
                Debug.Assert(false, "the argument of attribute [" + od.Role.Name + "] of entity [" + od.EntityName + "] was provided.");
            UserRole? role = GetValue<UserRole>(values[od.Role]);
            
            return new UserProxy(id,userName,password,role);
        }

        private static ICollection CreateCollection(IDictionary<IAttributeDefinition, object> values)
        {
            var od = AppCore.AppSingleton.FindObjDef<CollectionObjDef>();

            if (!values.ContainsKey(od.PrimaryKey))
                Debug.Assert(false, "the argument of attribute [" + od.PrimaryKey.Name + "] of entity [" + od.EntityName + "] was provided.");
            string appId = GetString(values[od.PrimaryKey]);

            if (!values.ContainsKey(od.Value))
                Debug.Assert(false, "the argument of attribute [" + od.Value.Name + "] of entity [" + od.EntityName + "] was provided.");
            decimal? value = GetValue<decimal>(values[od.Value]);

            if (!values.ContainsKey(od.Offset))
                Debug.Assert(false, "the argument of attribute [" + od.Offset.Name + "] of entity [" + od.EntityName + "] was provided.");
            decimal? offset = GetValue<decimal>(values[od.Offset]);

            return new CollectionProxy(appId, value, offset);
        }

        private static ICase CreateCase(IDictionary<IAttributeDefinition, object> values)
        {
            var od = AppCore.AppSingleton.FindObjDef<CaseObjDef>();

            if (!values.ContainsKey(od.PrimaryKey))
                Debug.Assert(false, "the argument of attribute [" + od.PrimaryKey.Name + "] of entity [" + od.EntityName + "] was provided.");
            string appId = GetString(values[od.PrimaryKey]);

            if (!values.ContainsKey(od.Type))
                Debug.Assert(false, "the argument of attribute [" + od.Type.Name + "] of entity [" + od.EntityName + "] was provided.");
            string type = GetString(values[od.Type]);

            if (!values.ContainsKey(od.Text))
                Debug.Assert(false, "the argument of attribute [" + od.Text.Name + "] of entity [" + od.EntityName + "] was provided.");
            string text = GetString(values[od.Text]);
            
            return new CaseProxy(appId,type,text);
        }

        private static IStatus CreateStatus(IDictionary<IAttributeDefinition, object> values)
        {
            var od = AppCore.AppSingleton.FindObjDef<StatusObjDef>();
            
            if(!values.ContainsKey(od.PrimaryKey))
                Debug.Assert(false, "the argument of attribute [" + od.PrimaryKey.Name + "] of entity [" + od.EntityName + "] was provided.");
            string appId = GetString(values[od.PrimaryKey]);
            
            if(!values.ContainsKey(od.Value))
                Debug.Assert(false, "the argument of attribute [" + od.Value.Name + "] of entity [" + od.EntityName + "] was provided.");
            string value = GetString(values[od.Value]);

            return new StatusProxy(appId,value);
        }

        private static TK? GetValue<TK>(object val) where TK : struct
        {
            Type tarType = typeof(TK);

            if (val ==null)
            {
                return null;
            }
            if (tarType == typeof(Guid))
            {
                return (TK)Convert.ChangeType(new Guid(val.ToString()), tarType);
            }
            else if (tarType == typeof(int) || tarType == typeof(Int32))
            {
                return (TK)Convert.ChangeType(Convert.ToInt32(val), tarType);
            }
            else if (tarType == typeof(bool) || tarType == typeof(Boolean))
            {
                return (TK)Convert.ChangeType(Convert.ToBoolean(val), tarType);
            }
            else if (tarType == typeof(DateTime))
            {
                return (TK)Convert.ChangeType(Convert.ToDateTime(val), tarType);
            }
            else if (tarType == typeof(decimal))
            {
                return (TK)Convert.ChangeType(Convert.ToDecimal(val), tarType);
            }
            else if (tarType == typeof(UserRole))
            {
                return (TK)Convert.ChangeType((UserRole)Convert.ToInt32(val), tarType);
            }
            else
            {
                Debug.Assert(true, string.Format("Not implement GetValue for the type {0}", tarType.FullName));
                return default(TK);
            }
        }

        private static string GetString(object val)
        {
            if (val == null)
                return null;
            else if (val is string)
                return (string) val;
            else
                return val.ToString();
        }

        private static Guid GetGuid(object val)
        {
            if (val == null)
                return Guid.Empty;

            else if (val is Guid)
                return (Guid) val;
            else
                return new Guid(GetString(val));
        }
    }
}
