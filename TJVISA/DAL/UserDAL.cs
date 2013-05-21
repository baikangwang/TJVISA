using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TJVISA.Entity;

namespace TJVISA.DAL
{
    public static class UserDAL
    {
        public static IUser Get(DBCommand cmd, string id)
        {
            return cmd.Get<IUser>("User", id);
        }

        public static IUser Get(DBCommand cmd,IDictionary<IAttributeDefinition,object> criterias )
        {
            return cmd.GetAll<IUser>("User", criterias).FirstOrDefault();
        }

        public static IUser Create(DBCommand cmd, string id, string name, string password, UserRole? role)
        {
            var od = AppCore.AppSingleton.FindObjDef<UserObjDef>();

            return cmd.Create<IUser>("User", new Dictionary<IAttributeDefinition, object>()
                                                         {
                                                             {od.PrimaryKey, id},
                                                             {od.Name, name},
                                                             {od.Password, password},
                                                             {od.Role, role}
                                                         });
        }

        public static IList<IUser> GetAll(DBCommand cmd, IDictionary<IAttributeDefinition, object> criterias)
        {
            return cmd.GetAll<IUser>("User", criterias);
        }

        public static IList<IUser> GetAllPaged(DBCommand cmd, IDictionary<IAttributeDefinition, object> criterias, ref int pageNum, int pageSize, ref int pageCount,ref int itemCount)
        {
            return cmd.GetAllPaged<IUser>("User", criterias, ref pageCount, ref pageNum, ref itemCount,pageSize);
        }

        public static IUser Update(DBCommand cmd, IUser instance, string name, string password, UserRole? role)
        {
            instance.Name = name;
            instance.Password = password;
            instance.Role = role;

            return cmd.Update("User", instance);
        }

        public static IUser Delete(DBCommand cmd, IUser instance)
        {
            return cmd.Delete("User", instance);
        }

        public static bool IsExisting(DBCommand cmd, IDictionary<IAttributeDefinition,object> criterias )
        {
            int count = cmd.Count("User", criterias);

            return count > 0;
        }

        public static IUser CreateSuperUser(string name)
        {
            return new UserProxy("",name,"",UserRole.Administrator);
        }

        public static IUser CreateAuthenticatedUer(string id,string name,UserRole? role)
        {
            return new UserProxy(id,name,"",role);
        }
    }
}
