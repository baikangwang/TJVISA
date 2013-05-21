using System;
using System.Data;
using System.Data.OleDb;

namespace TJVISA.Entity
{
    public interface IAttributeDefinition
    {
        string Name { get; set; }
        string Label { get; set; }
        string Column { get; set; }
        OleDbType DataType { get; set; }
        int OrderIndex { get; set; }
        bool IsRequired { get; set; }
        int? Length { get; set; }
        IObjectDefinition ForObject { get; set; }
        bool IsReadOnly { get; set; }
        bool ShowInList { get; set; }
        //bool IsNullable { get; set; }
        //bool IsEditable { get; set; }

    }

    public class AttributeDefinition : IAttributeDefinition
    {
        #region Implementation of IAttributeDefinition

        public string Name { get; set; }

        public string Label { get; set; }

        public string Column { get; set; }

        public OleDbType DataType { get; set; }

        public int OrderIndex { get; set; }

        public bool IsRequired { get; set; }

        public int? Length { get; set; }

        public IObjectDefinition ForObject { get; set; }

        public bool IsReadOnly { get; set; }

        public bool ShowInList { get; set; }

        //public bool IsNullable { get; set; }

        #endregion

        public AttributeDefinition(IObjectDefinition od, string name, string label, string column, OleDbType type,
                                   int? length, int orderIndex, bool isRequired, bool isReadOnly)
        {
            Name = name;
            Label = label;
            Column = column;
            DataType = type;
            Length = length;
            OrderIndex = orderIndex;
            IsRequired = isRequired;
            ForObject = od;
            IsReadOnly = isReadOnly;
            ShowInList = true;
        }

        public AttributeDefinition(IObjectDefinition od, string name, string label, string column, OleDbType type,
                                   int? length, int orderIndex)
            : this(od, name, label, column, type, length, orderIndex, false, false)
        {
        }

        public AttributeDefinition(IObjectDefinition od, string name, string label, string column, OleDbType type,
                                   int? length, int orderIndex, bool isRequired)
            : this(od, name, label, column, type, length, orderIndex, isRequired, false)
        {
        }

        public AttributeDefinition(IObjectDefinition od, string name, string label, string column, OleDbType type,
                                   int orderIndex)
            : this(od, name, label, column, type, null, orderIndex, false, false)
        {
        }

        public AttributeDefinition(IObjectDefinition od, string name, string label, string column, OleDbType type,
                                   int orderIndex, bool isRequired)
            : this(od, name, label, column, type, null, orderIndex, isRequired, false)
        {
        }

        public AttributeDefinition(IObjectDefinition od, string name, string label, string column, OleDbType type,
                                   int orderIndex, bool isRequired, bool isReadOnly)
            : this(od, name, label, column, type, null, orderIndex, isRequired, isReadOnly)
        {
        }

    }
}
