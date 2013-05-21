using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using TJVISA.Entity;

namespace TJVISA.DAL
{
    public static class EntityChecker
    {
        public static IDictionary<IAttributeDefinition, object> Check(string entityName, IBaseObject instance)
        {
            switch (entityName)
            {
                case "Application":
                    return CheckApplication(instance);
                case "Customer":
                    return CheckCustomer(instance);
                case "User":
                    return CheckUser(instance);
                case "Case":
                    return CheckCase(instance);
                case "Collection":
                    return CheckCollection(instance);
                case "Status":
                    return CheckStatus(instance);
                default:
                    Debug.Assert(false, "Didn't implement entity check for the entity " + entityName);
                    return null;
            }
        }

        private static IDictionary<IAttributeDefinition, object> CheckCustomer(IBaseObject instance)
        {
            var od = AppCore.AppSingleton.FindObjDef<CustomerObjDef>();
            
            IDictionary<IAttributeDefinition, object> changes = new Dictionary<IAttributeDefinition, object>();

            if (!(instance is CustomerProxy))
                Debug.Assert(false, "The given instance is not proxy type.");

            var entity = instance as CustomerProxy;
            var orginal = entity.Original as CustomerProxy;

            if (entity.ID != orginal.ID)
                changes.Add(od.PrimaryKey, entity.ID);
            if (entity.Name != orginal.Name)
                changes.Add(od.Name, entity.Name);
            if (entity.Sex != orginal.Sex)
                changes.Add(od.Sex, entity.Sex);
            if (entity.Phone != orginal.Phone)
                changes.Add(od.Phone, entity.Phone);
            if (entity.Address != orginal.Address)
                changes.Add(od.Address, entity.Address);

            return changes;
        }

        private static IDictionary<IAttributeDefinition, object> CheckApplication(IBaseObject instance)
        {
            var od = AppCore.AppSingleton.FindObjDef<ApplicationObjDef>();

            IDictionary<IAttributeDefinition, object> changes = new Dictionary<IAttributeDefinition, object>();

            if (!(instance is ApplicationProxy))
                Debug.Assert(false, "The given instance is not proxy type.");

            var entity = instance as ApplicationProxy;
            var orginal = entity.Original as ApplicationProxy;

            if (entity.ID != orginal.ID)
                changes.Add(od.PrimaryKey, entity.ID);
            if (entity.customerId != orginal.customerId)
                changes.Add(od.CustomerID, entity.customerId);
            if (entity.Region != orginal.Region)
                changes.Add(od.Region, entity.Region);
            if (entity.DateApplied != orginal.DateApplied)
                changes.Add(od.DateApplied, entity.DateApplied);
            if (entity.DateTraved != orginal.DateTraved)
                changes.Add(od.DateTraved, entity.DateTraved);
            if (entity.OffNoteNo != orginal.OffNoteNo)
                changes.Add(od.OffNoteNo, entity.OffNoteNo);
            if (entity.OffNoteDate != orginal.OffNoteDate)
                changes.Add(od.OffNoteDate, entity.OffNoteDate);
            if (entity.Remark != orginal.Remark)
                changes.Add(od.Remark, entity.Remark);

            return changes;
        }

        private static IDictionary<IAttributeDefinition, object> CheckUser(IBaseObject instance)
        {
            var od = AppCore.AppSingleton.FindObjDef<UserObjDef>();
            
            IDictionary<IAttributeDefinition, object> changes = new Dictionary<IAttributeDefinition, object>();

            if (!(instance is UserProxy))
                Debug.Assert(false, "The given instance is not proxy type.");

            var entity = instance as UserProxy;
            var orginal = entity.Original as UserProxy;

            if(entity.ID!=orginal.ID)
                changes.Add(od.PrimaryKey,entity.ID);
            if (entity.Name != orginal.Name)
                changes.Add(od.Name, entity.Name);
            if (entity.Password != orginal.Password)
                changes.Add(od.Password, entity.Password);
            if (entity.Role != orginal.Role)
                changes.Add(od.Role, entity.Role);
            return changes;
        }

        private static IDictionary<IAttributeDefinition, object> CheckCase(IBaseObject instance)
        {
            var od = AppCore.AppSingleton.FindObjDef<CaseObjDef>();
            
            IDictionary<IAttributeDefinition, object> changes = new Dictionary<IAttributeDefinition, object>();

            if (!(instance is CaseProxy))
                Debug.Assert(false, "The given instance is not proxy type.");

            var entity = instance as CaseProxy;
            var orginal = entity.Original as CaseProxy;

            //if(entity.AppId!=orginal.AppId)
            //    changes.Add(od.PrimaryKey, entity.AppId);
            if(entity.Type!=orginal.Type)
                changes.Add(od.Type,entity.Type);
            if(entity.Text!=orginal.Text)
                changes.Add(od.Text,entity.Text);

            return changes;
        }

        private static IDictionary<IAttributeDefinition, object> CheckCollection(IBaseObject instance)
        {
            var od = AppCore.AppSingleton.FindObjDef<CollectionObjDef>();

            IDictionary<IAttributeDefinition, object> changes = new Dictionary<IAttributeDefinition, object>();

            if (!(instance is CollectionProxy))
                Debug.Assert(false, "The given instance is not proxy type.");

            var entity = instance as CollectionProxy;
            var orginal = entity.Original as CollectionProxy;

            //if(entity.AppId!=orginal.AppId)
            //    changes.Add(od.PrimaryKey,entity.AppId);
            if(entity.Value!=orginal.Value)
                changes.Add(od.Value,entity.Value);
            if(entity.Offset!=orginal.Offset)
                changes.Add(od.Offset,entity.Offset);

            return changes;
        }

        private static IDictionary<IAttributeDefinition, object> CheckStatus(IBaseObject instance)
        {
            var od = AppCore.AppSingleton.FindObjDef<StatusObjDef>();

            IDictionary<IAttributeDefinition, object> changes = new Dictionary<IAttributeDefinition, object>();

            if (!(instance is StatusProxy))
                Debug.Assert(false,"The given instance is not proxy type.");

            var entity = instance as StatusProxy;
            var orginal = entity.Original as StatusProxy;

            if (entity.Value != orginal.Value)
                changes.Add(od.Value, entity.Value);

            //if (entity.AppId != orginal.AppId)
            //    changes.Add(od.PrimaryKey, entity.AppId);

            return changes;
        }

    }
}
