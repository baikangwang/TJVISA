using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using TJVISA.Entity;

namespace TJVISA.DAL
{
    public static class EntityUpdater
    {
        public static T Update<T>(DBCommand cmd, string entityName, T instance, IDictionary<IAttributeDefinition, object> changes) where T:class
        {
            switch (entityName)
            {
                case "Application":
                    return UpdateApplication(cmd, instance as IApplication, changes) as T;
                case "Customer":
                    return UpdateCustomer(cmd, instance as ICustomer, changes) as T;
                case "User":
                    return UpdateUser(cmd, instance as IUser, changes) as T;
                case "Collection":
                    return UpdateCollection(cmd, instance as ICollection, changes) as T;
                case "Case":
                    return UpdateCase(cmd, instance as ICase, changes) as T;
                case "Status":
                    return UpdateStatus(cmd, instance as IStatus, changes) as T;
                default:
                    Debug.Assert(false, "Didn't implement entity updater for the entity " + entityName);
                    return null;
            }
        }

        private static IApplication UpdateApplication(DBCommand cmd, IApplication instance, IDictionary<IAttributeDefinition, object> changes)
        {
            if (!(instance is ApplicationProxy))
                Debug.Assert(false, "The given instance is not proxy type.");

            var entity = instance as ApplicationProxy;
            var orginal = entity.Original as ApplicationProxy;

            foreach (IAttributeDefinition attr in changes.Keys)
            {
                orginal.SetValue(attr, changes[attr]);
            }

            if (entity.customerId == orginal.customerId && entity.customer != null)
                orginal.customer = cmd.Update("Customer", entity.customer) as ICustomer;

            return entity;
        }

        private static ICustomer UpdateCustomer(DBCommand cmd, ICustomer instance, IDictionary<IAttributeDefinition, object> changes)
        {
            if (!(instance is CustomerProxy))
                Debug.Assert(false, "The given instance is not proxy type.");

            var entity = instance as CustomerProxy;
            var orginal = entity.Original as CustomerProxy;

            foreach (IAttributeDefinition attr in changes.Keys)
            {
                orginal.SetValue(attr, changes[attr]);
            }

            return entity;
        }

        private static IUser UpdateUser(DBCommand cmd, IUser instance, IDictionary<IAttributeDefinition, object> changes)
        {
            if (!(instance is UserProxy))
                Debug.Assert(false, "The given instance is not proxy type.");

            var entity = instance as UserProxy;
            var orginal = entity.Original as UserProxy;

            foreach (IAttributeDefinition attr in changes.Keys)
            {
                orginal.SetValue(attr, changes[attr]);
            }

            return entity;
        }

        private static ICollection UpdateCollection(DBCommand cmd, ICollection instance, IDictionary<IAttributeDefinition, object> changes)
        {
            var od = AppCore.AppSingleton.FindObjDef<CollectionObjDef>();

            if (!(instance is CollectionProxy))
                Debug.Assert(false, "The given instance is not proxy type.");

            var entity = instance as CollectionProxy;
            var orginal = entity.Original as CollectionProxy;

            foreach (IAttributeDefinition attr in changes.Keys)
            {
                if (attr == od.PrimaryKey) continue;
                orginal.SetValue(attr, changes[attr]);
            }

            //if (entity.AppId == orginal.AppId && entity.App != null)
            //    orginal.App = cmd.Update("Application", entity.App) as IApplication;

            return entity;
        }

        private static ICase UpdateCase(DBCommand cmd, ICase instance, IDictionary<IAttributeDefinition, object> changes)
        {
            var od = AppCore.AppSingleton.FindObjDef<CaseObjDef>();

            if (!(instance is CaseProxy))
                Debug.Assert(false, "The given instance is not proxy type.");

            var entity = instance as CaseProxy;
            var orginal = entity.Original;

            foreach (IAttributeDefinition attr in changes.Keys)
            {
                if (attr == od.PrimaryKey) continue;
                orginal.SetValue(attr, changes[attr]);
            }

            //if (entity.AppId == orginal.AppId && entity.App != null)
            //    orginal.App = cmd.Update("Application", entity.App) as IApplication;

            return entity;
        }

        private static IStatus UpdateStatus(DBCommand cmd, IStatus instance, IDictionary<IAttributeDefinition, object> changes)
        {
            var od = AppCore.AppSingleton.FindObjDef<StatusObjDef>();

            if (!(instance is StatusProxy))
                Debug.Assert(false, "The given instance is not proxy type.");

            var entity = instance as StatusProxy;
            var orginal = entity.Original;

            foreach (IAttributeDefinition attr in changes.Keys)
            {
                if(attr==od.PrimaryKey) continue;
                orginal.SetValue(attr,changes[attr]);
            }

            //if (entity.AppId == orginal.AppId && entity.App != null)
            //    orginal.App = cmd.Update("Application", entity.App) as IApplication;

            return entity;
        }
    }
}
