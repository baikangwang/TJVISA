using System;
using System.Diagnostics;
using System.Reflection;

namespace TJVISA.Entity
{
    public interface IBaseObject
    {
        string ID { get; set; }
        IObjectDefinition ObjectDefinition { get; set; }
        object GetValue(IAttributeDefinition attrDef);
        void SetValue(IAttributeDefinition attrDef, object value);
    }

    public interface IBaseObjectProxy:IBaseObject
    {
        IBaseObjectProxy Original { get; }
    }

    public class BaseObject:IBaseObject
    {
        protected BaseObject(){}
        
        #region Implementation of IBaseObject

        public virtual string ID { get; set; }

        public IObjectDefinition ObjectDefinition { get; set; }

        public virtual object GetValue(IAttributeDefinition attrDef)
        {
            Type it = this.GetType();//this.GetType().Assembly.GetType("TJVISA.Entity." + attrDef.ForObject.EntityName,false, true);
            if (it == null) Debug.Assert(false, attrDef.ForObject.EntityName+ " is unknown object type.");
            PropertyInfo p = it.GetProperty(attrDef.Name);
            if (p != null && p.CanRead)
                return p.GetValue(this, null);
            return null;
        }

        public virtual void SetValue(IAttributeDefinition attrDef, object value)
        {
            Type it = this.GetType();//this.GetType().Assembly.GetType("TJVISA.Entity." + attrDef.ForObject.EntityName,false, true);
            if (it == null) Debug.Assert(false, attrDef.ForObject.EntityName + " is unknown object type.");
            PropertyInfo p = it.GetProperty(attrDef.Name);
            if (p != null && p.CanWrite)
                p.SetValue(this, value, null);
        }
        #endregion
    }
}
