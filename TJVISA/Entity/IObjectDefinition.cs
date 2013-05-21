using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TJVISA.Entity
{
    public interface IObjectDefinition
    {
        string EntityName { get; }
        string TableName { get;}
        IDictionary<string, IAttributeDefinition> Attributes { get; }
        IAttributeDefinition PrimaryKey { get; }
        string ListPage { get; }
        string DetailsPage { get; }
    }

    public abstract class ObjectDefinition : IObjectDefinition
    {
        #region Implementation of IObjectDefinition

        private readonly string _entityName;
        private readonly string _tableName;

        public string EntityName
        {
            get { return _entityName; }
        }

        public string TableName
        {
            get { return _tableName; }
        }

        private readonly IDictionary<string,IAttributeDefinition> _attrs;
        public IDictionary<string, IAttributeDefinition> Attributes
        {
            get { return _attrs; }
        }

        public IAttributeDefinition PrimaryKey
        {
            get { return _attrs["ID"]; }
        }

        public virtual string ListPage
        {
            get
            {
                return string.Format("EntityList.aspx?{0}={1}", GlobalConstant.EntityName, EntityName);
            }
        }

        public virtual string DetailsPage
        {
            get
            {
                return string.Format("EntityDetails.aspx?{0}={1}", GlobalConstant.EntityName, EntityName);
            }
        }

        #endregion

        protected ObjectDefinition(string entityName,string tableName)
        {
            _entityName = entityName;
            _tableName = tableName;
            _attrs = CreateAttributeDefinition(entityName);
        }

        protected IDictionary<string, IAttributeDefinition> CreateAttributeDefinition(string entityName)
        {
            return AttributeDefinitionFactory.CreateAttributes(entityName,this);
        }
    }

    public class ApplicationObjDef:ObjectDefinition
    {
        public ApplicationObjDef(string entityName, string tableName) : base(entityName, tableName)
        {

        }

        public IAttributeDefinition ID
        {
            get { return this.Attributes["ID"]; }
        }
        public IAttributeDefinition Type
        {
            get { return this.Attributes["Type"]; }
        }
        public IAttributeDefinition SubType
        {
            get { return this.Attributes["SubType"]; }
        }
        public IAttributeDefinition CustomerName
        {
            get { return this.Attributes["CustomerName"]; }
        }
        public IAttributeDefinition CustomerSex
        {
            get { return this.Attributes["CustomerSex"]; }
        }
        public IAttributeDefinition CustomerID
        {
            get { return this.Attributes["CustomerID"]; }
        }
        public IAttributeDefinition Region
        {
            get { return this.Attributes["Region"]; }
        }
        public IAttributeDefinition DateApplied
        {
            get { return this.Attributes["DateApplied"]; }
        }
        public IAttributeDefinition DateTraved
        {
            get { return this.Attributes["DateTraved"]; }
        }
        public IAttributeDefinition OffNoteNo
        {
            get { return this.Attributes["OffNoteNo"]; }
        }
        public IAttributeDefinition OffNoteDate
        {
            get { return this.Attributes["OffNoteDate"]; }
        }
        public IAttributeDefinition Remark
        {
            get { return this.Attributes["Remark"]; }
        }

        public override string DetailsPage
        {
            get
            {
                return "ApplicationModel.aspx";
            }
        }
    }

    public class CustomerObjDef:ObjectDefinition
    {
        public CustomerObjDef(string entityName, string tableName) : base(entityName, tableName)
        {
        }
        public IAttributeDefinition ID
        {
            get { return this.Attributes["ID"]; }
        }
        public IAttributeDefinition Name
        {
            get { return this.Attributes["Name"]; }
        }
        public IAttributeDefinition Sex
        {
            get { return this.Attributes["Sex"]; }
        }
        public IAttributeDefinition Phone
        {
            get { return this.Attributes["Phone"]; }
        }
        public IAttributeDefinition Address
        {
            get { return this.Attributes["Address"]; }
        }

        public override string ListPage
        {
            get
            {
                return string.Format("EntityList.aspx?{0}={1}", GlobalConstant.EntityName, "Application");
            }
        }

        public override string DetailsPage
        {
            get
            {
                return "ApplicationDetails.aspx";
            }
        }

    }

    public class UserObjDef:ObjectDefinition
    {
        public UserObjDef(string entityName, string tableName) : base(entityName, tableName)
        {
        }
        public IAttributeDefinition ID
        {
            get { return this.Attributes["ID"]; }
        }
        public IAttributeDefinition Name
        {
            get { return this.Attributes["Name"]; }
        }
        public IAttributeDefinition Password
        {
            get { return this.Attributes["Password"]; }
        }
        public IAttributeDefinition Role
        {
            get { return this.Attributes["Role"]; }
        }
        public override string DetailsPage
        {
            get
            {
                return "UserDetails.aspx";
            }
        }
    }

    public class CollectionObjDef:ObjectDefinition
    {
        public CollectionObjDef(string entityName, string tableName) : base(entityName, tableName)
        {
        }
        public IAttributeDefinition ID
        {
            get { return this.Attributes["ID"]; }
        }
        public IAttributeDefinition CustomerName
        {
            get { return this.Attributes["CustomerName"]; }
        }
        public IAttributeDefinition Value
        {
            get { return this.Attributes["Value"]; }
        }
        public IAttributeDefinition Offset
        {
            get { return this.Attributes["Offset"]; }
        }
        public override string ListPage
        {
            get
            {
                return string.Format("EntityList.aspx?{0}={1}", GlobalConstant.EntityName, "Application");
            }
        }
        public override string DetailsPage
        {
            get
            {
                return "CaseDetails.aspx";
            }
        }
    }

    public class CaseObjDef:ObjectDefinition
    {
        public CaseObjDef(string entityName, string tableName) : base(entityName, tableName)
        {
        }
        public IAttributeDefinition ID
        {
            get { return this.Attributes["ID"]; }
        }
        public IAttributeDefinition AppType
        {
            get { return this.Attributes["AppType"]; }
        }
        public IAttributeDefinition CustomerName
        {
            get { return this.Attributes["CustomerName"]; }
        }
        public IAttributeDefinition CustomerID
        {
            get { return this.Attributes["CustomerID"]; }
        }
        public IAttributeDefinition Region
        {
            get { return this.Attributes["Region"]; }
        }
        public IAttributeDefinition Type
        {
            get { return this.Attributes["Type"]; }
        }
        public IAttributeDefinition Text
        {
            get { return this.Attributes["Text"]; }
        }

        public override string ListPage
        {
            get
            {
                return string.Format("EntityList.aspx?{0}={1}", GlobalConstant.EntityName, "Application");
            }
        }

        public override string DetailsPage
        {
            get
            {
                return "CaseDetails.aspx";
            }
        }

    }

    public class StatusObjDef:ObjectDefinition
    {
        public StatusObjDef(string entityName, string tableName) : base(entityName, tableName)
        {
        }
        public IAttributeDefinition ID
        {
            get { return this.Attributes["ID"]; }
        }
        public IAttributeDefinition AppType
        {
            get { return this.Attributes["AppType"]; }
        }
        public IAttributeDefinition CustomerName
        {
            get { return this.Attributes["CustomerName"]; }
        }
        public IAttributeDefinition Value
        {
            get { return this.Attributes["Value"]; }
        }
        public override string DetailsPage
        {
            get { return ""; }
        }
        public override string ListPage
        {
            get
            {
                return "";
            }
        }
    }
}
