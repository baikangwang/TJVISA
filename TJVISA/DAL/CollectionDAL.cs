using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TJVISA.Entity;

namespace TJVISA.DAL
{
    public static class CollectionDAL
    {
        public static ICollection Get(DBCommand cmd, string id)
        {
            return cmd.Get<ICollection>("Collection", id);
        }

        public static ICollection Create(DBCommand cmd, string id, decimal? value, decimal? offset)
        {
            var od = AppCore.AppSingleton.FindObjDef<CollectionObjDef>();

            return cmd.Create<ICollection>("Collection", new Dictionary<IAttributeDefinition, object>()
                                                             {
                                                                 {od.PrimaryKey, id},
                                                                 {od.Value, value == null ? null : value.Value + ""},
                                                                 {od.Offset, offset == null ? null : offset.Value + ""}
                                                             });
        }

        public static bool IsExisting(DBCommand cmd, IDictionary<IAttributeDefinition, object> criterias)
        {
            int count = cmd.Count("Collection", criterias);

            return count > 0;
        }

        public static ICollection Update(DBCommand cmd, ICollection instance, decimal? value, decimal? offset)
        {
            instance.Offset = offset;
            instance.Value = value;

            return cmd.Update("Collection", instance);
        }

        public static ICollection Delete(DBCommand cmd, ICollection instance)
        {
            return cmd.Delete("Collection", instance);
        }
    }
}
