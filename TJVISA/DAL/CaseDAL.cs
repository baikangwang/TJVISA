using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TJVISA.Entity;

namespace TJVISA.DAL
{
    public class CaseDAL
    {
        public static ICase Get(DBCommand cmd, string id)
        {
            return cmd.Get<ICase>("Case", id);
        }

        public static ICase Create(DBCommand cmd, string id, string type, string text)
        {
            var od = AppCore.AppSingleton.FindObjDef<CaseObjDef>();

            return cmd.Create<ICase>("Case", new Dictionary<IAttributeDefinition, object>()
                                                             {
                                                                 {od.PrimaryKey, id},
                                                                 {od.Type, type},
                                                                 {od.Text, text}
                                                             });
        }

        public static bool IsExisting(DBCommand cmd, IDictionary<IAttributeDefinition, object> criterias)
        {
            int count = cmd.Count("Case", criterias);

            return count > 0;
        }

        public static ICase Update(DBCommand cmd, ICase instance, string type, string text)
        {
            instance.Type = type;
            instance.Text = text;

            return cmd.Update("Case", instance);
        }

        public static ICase Delete(DBCommand cmd, ICase instance)
        {
            return cmd.Delete("Case", instance);
        }
    }
}
