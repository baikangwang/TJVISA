using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using TJVISA.Entity;

namespace TJVISA.DAL
{
    public static class EntityReader
    {
        public static T Read<T>(OleDbDataReader reader, string entityName) where T : class
        {
            switch (entityName)
            {
                case "Application":
                    return ReadToApplication(reader) as T;
                case "Customer":
                    return ReadToCustomer(reader) as T;
                case "Collection":
                    return ReadToCollection(reader) as T;
                case "Case":
                    return ReadToCase(reader) as T;
                case "Status":
                    return ReadToStatus(reader) as T;
                case "User":
                    return ReadToUser(reader) as T;
                default:
                    Debug.Assert(false,
                                 string.Format("The type,{0} hasn't been implemented Translator.", entityName));
                    return null;
            }
        }

        private static IStatus ReadToStatus(OleDbDataReader reader)
        {
            var od = AppCore.AppSingleton.FindObjDef<StatusObjDef>();

            return new StatusProxy(GetString(reader, od.PrimaryKey.Column),
                                   GetString(reader, od.Value.Column));
        }

        private static ICase ReadToCase(OleDbDataReader reader)
        {
            var od = AppCore.AppSingleton.FindObjDef<CaseObjDef>();

            return new CaseProxy(GetString(reader, od.PrimaryKey.Column),
                                       GetString(reader, od.Type.Column),
                                       GetString(reader, od.Text.Column));
        }

        private static ICollection ReadToCollection(OleDbDataReader reader)
        {
            var od = AppCore.AppSingleton.FindObjDef<CollectionObjDef>();

            string valueVal = GetString(reader, od.Value.Column);
            string offsetVal = GetString(reader, od.Offset.Column);

            decimal? value = string.IsNullOrEmpty(valueVal) ? (decimal?) null : decimal.Parse(valueVal);
            decimal? offset = string.IsNullOrEmpty(offsetVal) ? (decimal?)null : decimal.Parse(offsetVal);
            
            return new CollectionProxy(GetString(reader, od.PrimaryKey.Column),
                                       value,offset);
        }

        private static ICustomer ReadToCustomer(OleDbDataReader reader)
        {
            var od = AppCore.AppSingleton.FindObjDef<CustomerObjDef>();

            return new CustomerProxy(GetString(reader, od.PrimaryKey.Column),
                                     GetString(reader, od.Name.Column),
                                     GetString(reader, od.Sex.Column),
                                     GetString(reader, od.Phone.Column),
                                     GetString(reader, od.Address.Column));
        }

        private static IApplication ReadToApplication(OleDbDataReader reader)
        {
            var od = AppCore.AppSingleton.FindObjDef<ApplicationObjDef>();

            return new ApplicationProxy(GetString(reader, od.PrimaryKey.Column),
                                        GetString(reader, od.CustomerID.Column),
                                        GetString(reader, od.Region.Column),
                                        GetValue<DateTime>(reader, od.DateApplied.Column),
                                        GetValue<DateTime>(reader, od.DateTraved.Column),
                                        GetString(reader, od.OffNoteNo.Column),
                                        GetValue<DateTime>(reader, od.OffNoteDate.Column),
                                        GetString(reader, od.Remark.Column));
        }

        private static IUser ReadToUser(OleDbDataReader reader)
        {
            var od = AppCore.AppSingleton.FindObjDef<UserObjDef>();

            return new UserProxy(GetString(reader, od.PrimaryKey.Column),
                                 GetString(reader, od.Name.Column),
                                 GetString(reader, od.Password.Column),
                                 GetValue<UserRole>(reader, od.Role.Column));
        }

        private static TK? GetValue<TK>(OleDbDataReader reader, string fieldName) where TK : struct
        {
            Type tarType = typeof(TK);
            int ordinal = reader.GetOrdinal(fieldName);

            if (reader.IsDBNull(ordinal))
            {
                return null;
            }
            if (tarType == typeof(Guid))
            {
                return (TK)Convert.ChangeType(reader.GetGuid(ordinal), tarType);
            }
            else if (tarType == typeof(int) || tarType == typeof(Int32))
            {
                return (TK)Convert.ChangeType(reader.GetInt32(ordinal), tarType);
            }
            else if (tarType == typeof(bool) || tarType == typeof(Boolean))
            {
                return (TK)Convert.ChangeType(reader.GetBoolean(ordinal), tarType);
            }
            else if (tarType == typeof(DateTime))
            {
                return (TK)Convert.ChangeType(reader.GetDateTime(ordinal), tarType);
            }
            else if (tarType == typeof(decimal))
            {
                return (TK)Convert.ChangeType(reader.GetDecimal(ordinal), tarType);
            }
            else if (tarType == typeof(UserRole))
            {
                return (TK)Convert.ChangeType((UserRole)reader.GetInt32(ordinal), tarType);
            }
            else
            {
                Debug.Assert(true, string.Format("Not implement GetValue for the type {0}", tarType.FullName));
                return default(TK);
            }
        }

        private static string GetString(OleDbDataReader reader, string fieldName)
        {
            int ord = reader.GetOrdinal(fieldName);
            if (ord < 0 || reader.IsDBNull(ord))
                return string.Empty;
            else
                return reader.GetString(ord);
        }

        private static Guid GetGuid(OleDbDataReader reader, string fieldName)
        {
            int ord = reader.GetOrdinal(fieldName);
            if (ord < 0 || reader.IsDBNull(ord))
                return Guid.Empty;
            else
                return reader.GetGuid(ord);
        }
    }

    public static class Parameter
    {
        public static object ToValue<T>(T? val) where T : struct
        {
            return val.HasValue ? (object)val.Value : DBNull.Value;
        }

        public static object ToValue(object val)
        {
            if (val is IBaseObject)
            {
                return (val as IBaseObject).ID;
            }
            else
            {
                return val ?? DBNull.Value;
            }
        }
    }
}
