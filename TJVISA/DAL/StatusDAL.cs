using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TJVISA.Entity;

namespace TJVISA.DAL
{
    public static class StatusDAL
    {
        public static IStatus Get(DBCommand cmd, string id)
        {
            return cmd.Get<IStatus>("Status", id);
        }

        public static IStatus Create(DBCommand cmd, string id,string value)
        {
            var od = AppCore.AppSingleton.FindObjDef<StatusObjDef>();

            return cmd.Create<IStatus>("Status", new Dictionary<IAttributeDefinition, object>()
                                                             {
                                                                 {od.PrimaryKey, id},
                                                                 {od.Value, value},
                                                             });
        }

        public static bool IsExisting(DBCommand cmd, IDictionary<IAttributeDefinition, object> criterias)
        {
            int count = cmd.Count("Status", criterias);

            return count > 0;
        }

        public static IStatus Update(DBCommand cmd, IStatus instance, string value)
        {
            instance.Value = value;

            return cmd.Update("Status", instance);
        }

        public static IStatus Delete(DBCommand cmd, IStatus instance)
        {
            return cmd.Delete("Status", instance);
        }
    }
}
