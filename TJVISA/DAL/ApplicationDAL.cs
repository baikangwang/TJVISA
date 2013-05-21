using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using TJVISA.Entity;

namespace TJVISA.DAL
{
    public static class ApplicationDAL
    {
        public static IApplication Get(DBCommand cmd, string id)
        {
            return cmd.Get<IApplication>("Application", id);
        }

        public static IApplication Create(DBCommand cmd, string id, /*string type, string subType,*/
            string customerId, string region, DateTime? dateApplied,
            DateTime? dateTraved, string offNoteNo, DateTime? offNoteDate, string remark)
        {
            var od = AppCore.AppSingleton.FindObjDef<ApplicationObjDef>();

            return cmd.Create<IApplication>("Application", new Dictionary<IAttributeDefinition, object>()
                                                               {
                                                                   {od.PrimaryKey, id},
                                                                   //{od.Type, type},
                                                                   //{od.SubType, subType},
                                                                   {od.CustomerID, customerId},
                                                                   //{od.CustomerName, customerName},
                                                                   //{od.CustomerSex, customerSex},
                                                                   {od.Region, region},
                                                                   {od.DateApplied, dateApplied},
                                                                   {od.DateTraved, dateTraved},
                                                                   {od.OffNoteNo, offNoteNo},
                                                                   {od.OffNoteDate, offNoteDate},
                                                                   {od.Remark, remark}
                                                               });
        }

        public static IList<IApplication> GetAll(DBCommand cmd, IDictionary<IAttributeDefinition, object> criterias)
        {
            return cmd.GetAll<IApplication>("Application", criterias);
        }

        public static IList<IApplication> GetAllPaged(DBCommand cmd, IDictionary<IAttributeDefinition, object> criterias, ref int pageNum, int pageSize, ref int pageCount, ref int itemCount)
        {
            if (!AppCore.AppSingleton.ObjectDefinitions.ContainsKey("Application"))
                Debug.Assert(false, "Unimplemented entity [Application]");
            ApplicationObjDef appOd = AppCore.AppSingleton.FindObjDef<ApplicationObjDef>();

            if (!AppCore.AppSingleton.ObjectDefinitions.ContainsKey("Status"))
                Debug.Assert(false, "Unimplemented entity [Status]");
            StatusObjDef statusOd = AppCore.AppSingleton.FindObjDef<StatusObjDef>();

            if (!AppCore.AppSingleton.ObjectDefinitions.ContainsKey("User"))
                Debug.Assert(false, "Unimplemented entity [User]");
            UserObjDef userOd = AppCore.AppSingleton.FindObjDef<UserObjDef>();

            if(!criterias.ContainsKey(userOd.Role))
                Debug.Assert(false, "Criterias should contains UserRole of entity [User]");
            UserRole role = (UserRole) Convert.ChangeType(criterias[userOd.Role], typeof (UserRole));
            if(role==UserRole.None)role=UserRole.Client;
            criterias.Remove(userOd.Role);
            
            var sqlBuilder = new StringBuilder();

            string table = string.Format("[{0}]", appOd.TableName);
            string tabStatus = string.Format("[{0}]", statusOd.TableName);
            string fileds = string.Join(",", appOd.Attributes.Values.Select(a => string.Format("[{0}]", a.Column)).ToArray());
            string criterion = string.Join(" and ",
                                           criterias.Keys.Where(a=>a!=userOd.Role).Select(a => string.Format("[{0}]=@{0}", a.Column)).ToArray());

            string fieldsWithAlias = "comp." + fileds.Replace(",", ",comp.");
            string criterionWithAlias = string.IsNullOrEmpty(criterion) ? "" : "comp." + criterion.Replace(" and ", " and comp.");

            string statusCriterion = "";
            switch (role)
            {
                    case UserRole.Client:
                    statusCriterion = string.Format("s.[{0}] in ( {1} ) or s.[{0}] is Null or s.[{0}]=''",
                                                    statusOd.Value.Column, string.Join(",",role.GetVisibleStatus().Select(r =>"'" + r.ToLabel() + "'").ToArray()));
                    break;
                    case UserRole.Business:
                    case UserRole.Administrator:
                default:
                    statusCriterion = string.Format("s.[{0}] in ( {1} )",statusOd.Value.Column, string.Join(",", role.GetVisibleStatus().Select(r => "'" + r.ToLabel() + "'").ToArray()));
                    break;
            }

            if (!string.IsNullOrEmpty(criterionWithAlias))
                statusCriterion = string.Format("({0}) and ({1})", statusCriterion, criterionWithAlias);
            
            
            sqlBuilder.AppendLine(string.Format("SELECT {1}, (select count(org.[{2}])+1 from {0} [org] where org.[{2}] <comp.[{2}]) as Rank",
                              table, fieldsWithAlias, appOd.PrimaryKey.Column));
            sqlBuilder.AppendLine(string.Format("FROM {0} [comp] left join {1} [s] on s.[{2}]=comp.[{3}]", table,tabStatus,statusOd.PrimaryKey.Column,appOd.PrimaryKey.Column));
            if (!string.IsNullOrEmpty(statusCriterion))
                sqlBuilder.AppendLine(string.Format("Where {0}", statusCriterion));
            sqlBuilder.AppendLine(string.Format("ORDER BY comp.[{0}]", appOd.PrimaryKey.Column));
            
            string sql = sqlBuilder.ToString();

            return cmd.GetAllPaged<IApplication>("Application",sql, criterias, ref pageCount, ref pageNum,ref itemCount, pageSize);
        }
        
        public static IApplication Update(DBCommand cmd, IApplication instance, string id,
            string customerId, string region, DateTime? dateApplied,
            DateTime? dateTraved, string offNoteNo, DateTime? offNoteDate, string remark)
        {
            instance.ID = id;
            instance.CustomerID = customerId;
            instance.Region = region;
            instance.DateApplied = dateApplied;
            instance.DateTraved = dateTraved;
            instance.OffNoteDate = offNoteDate;
            instance.OffNoteNo = offNoteNo;
            instance.Remark = remark;

            return cmd.Update("Application", instance);
        }

        public static IApplication Delete(DBCommand cmd, IApplication instance)
        {
            return cmd.Delete("Application", instance);
        }

        public static bool IsExisting(DBCommand cmd, IDictionary<IAttributeDefinition, object> criterias)
        {
            int count = cmd.Count("Application", criterias);

            return count > 0;
        }
    }
}
