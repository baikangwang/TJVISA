using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;

namespace TJVISA.Entity
{
    public class AttributeDefinitionFactory
    {
        public static IDictionary<string, IAttributeDefinition> CreateAttributes(string entityName, IObjectDefinition od)
        {
            switch (entityName)
            {
                case "Application":
                    return CreateApplicationAttributes(od);
                case "Customer":
                    return CreateCustomerAttributes(od);
                case "Case":
                    return CreateCaseAttributes(od);
                case "Collection":
                    return CreateCollectionAttributes(od);
                case "Status":
                    return CreateStatusAttributes(od);
                case "User":
                    return CreateUserAttributes(od);
                default:
                    Debug.Assert(false,"Encounter an unknown object type "+entityName);
                    return new Dictionary<string,IAttributeDefinition>();

            }
        }

        protected static IDictionary<string, IAttributeDefinition> CreateCustomerAttributes(IObjectDefinition od)
        {
            IDictionary<string, IAttributeDefinition> attrs = new Dictionary<string,IAttributeDefinition>();
            attrs.Add("ID", new AttributeDefinition(od,"ID", "客户身份证号", "clientnum",OleDbType.VarWChar, 20, 0,true));
            attrs.Add("Name", new AttributeDefinition(od, "Name", "客户姓名", "clientname", OleDbType.VarWChar, 10, 1,true));
            attrs.Add("Sex", new AttributeDefinition(od, "Sex", "客户性别", "clientsex", OleDbType.VarWChar, 10, 2));
            attrs.Add("Phone", new AttributeDefinition(od, "Phone", "联系电话", "clientphone", OleDbType.VarWChar, 20, 3));
            attrs.Add("Address", new AttributeDefinition(od, "Address", "收件地址", "clientaddress", OleDbType.VarWChar, 225, 4));
            return attrs;
        }

        protected static IDictionary<string, IAttributeDefinition> CreateApplicationAttributes(IObjectDefinition od)
        {
            IDictionary<string, IAttributeDefinition> attrs = new Dictionary<string,IAttributeDefinition>();
            attrs.Add("ID", new AttributeDefinition(od, "ID", "业务单号", "busnum", OleDbType.VarWChar, 20, 0, true));
            attrs.Add("Type", new AttributeDefinition(od, "Type", "业务类别", "busstyle", OleDbType.VarWChar, 10, 1,false,true));
            attrs.Add("SubType", new AttributeDefinition(od, "SubType", "数量", "buscount", OleDbType.VarWChar, 10, 2, false, true));
            attrs.Add("CustomerName", new AttributeDefinition(od, "CustomerName", "客户姓名", "clientname", OleDbType.VarWChar, 10, 3,false,true));
            attrs.Add("CustomerSex", new AttributeDefinition(od, "CustomerSex", "客户性别", "clientsex", OleDbType.VarWChar, 10, 4,false,true));
            attrs.Add("CustomerID", new AttributeDefinition(od, "CustomerID", "客户身份证号", "clientnum", OleDbType.VarWChar, 20, 5));
            attrs.Add("Region", new AttributeDefinition(od, "Region", "申请国家", "applycountry", OleDbType.VarWChar, 10, 6));
            attrs.Add("DateApplied", new AttributeDefinition(od, "DateApplied", "申请日期", "applydate", OleDbType.Date, 10, 7));
            attrs.Add("DateTraved", new AttributeDefinition(od, "DateTraved", "启程日期", "traveldate", OleDbType.Date, 20, 8) { ShowInList = false });
            attrs.Add("OffNoteNo", new AttributeDefinition(od, "OffNoteNo", "照会号", "offnotenum", OleDbType.VarWChar, 10, 9) { ShowInList = false });
            attrs.Add("OffNoteDate", new AttributeDefinition(od, "OffNoteDate", "照会日期", "offnotedate", OleDbType.Date, 20, 10) { ShowInList = false });
            attrs.Add("Remark", new AttributeDefinition(od, "Remark", "材料说明", "remark", OleDbType.VarWChar, 255, 11) { ShowInList = false });
            return attrs;
        }

        protected static IDictionary<string, IAttributeDefinition> CreateCaseAttributes(IObjectDefinition od)
        {
            IDictionary<string, IAttributeDefinition> attrs = new Dictionary<string,IAttributeDefinition>();
            attrs.Add("ID", new AttributeDefinition(od, "ID", "业务单号", "busnum", OleDbType.VarWChar, 20, 0, true));
            attrs.Add("AppType", new AttributeDefinition(od, "AppType", "业务类别", "busstyle", OleDbType.VarWChar, 10, 1, false, true));
            attrs.Add("CustomerName", new AttributeDefinition(od, "CustomerName", "客户姓名", "clientname", OleDbType.VarWChar, 10, 2, false, true));
            attrs.Add("CustomerID", new AttributeDefinition(od, "CustomerID", "客户身份证号", "clientnum", OleDbType.VarWChar, 20, 3, false, true));
            attrs.Add("Region", new AttributeDefinition(od, "Region", "申请国家", "applycountry", OleDbType.VarWChar, 10, 4, false, true));
            attrs.Add("Type", new AttributeDefinition(od, "Type", "问题案类型", "prostyle", OleDbType.VarWChar, 10, 5));
            attrs.Add("Text", new AttributeDefinition(od, "Text", "问题案信息", "protext", OleDbType.VarWChar, 255, 6));
            return attrs;
        }

        protected static IDictionary<string, IAttributeDefinition> CreateCollectionAttributes(IObjectDefinition od)
        {
            IDictionary<string, IAttributeDefinition> attrs = new Dictionary<string,IAttributeDefinition>();
            attrs.Add("ID",new AttributeDefinition(od, "ID", "业务单号", "busnum", OleDbType.VarWChar, 20, 0, true));
            attrs.Add("CustomerName",new AttributeDefinition(od, "CustomerName", "客户姓名", "clientname", OleDbType.VarWChar, 10, 1, false, true));
            attrs.Add("Value",new AttributeDefinition(od, "Value", "收款金额", "moneys", OleDbType.VarWChar, 10, 2));
            attrs.Add("Offset",new AttributeDefinition(od, "Offset", "退补金额", "moneyt", OleDbType.VarWChar, 10, 3));
            return attrs;
        }

        protected static IDictionary<string, IAttributeDefinition> CreateStatusAttributes(IObjectDefinition od)
        {
            IDictionary<string, IAttributeDefinition> attrs = new Dictionary<string,IAttributeDefinition>();
            attrs.Add("ID",new AttributeDefinition(od,"ID", "业务单号", "busnum", OleDbType.VarWChar, 20, 0, true));
            attrs.Add("AppType", new AttributeDefinition(od, "AppType", "业务类别", "busstyle", OleDbType.VarWChar, 10, 1, false, true));
            attrs.Add("CustomerName",new AttributeDefinition(od,"CustomerName", "客户姓名", "clientname", OleDbType.VarWChar, 10, 2, false,true));
            attrs.Add("Value", new AttributeDefinition(od, "Value", "进程编号", "staynum", OleDbType.VarWChar, 10, 3));
            return attrs;
        }

        protected static IDictionary<string, IAttributeDefinition> CreateUserAttributes(IObjectDefinition od)
        {
            IDictionary<string, IAttributeDefinition> attrs = new Dictionary<string,IAttributeDefinition>();
            attrs.Add("ID", new AttributeDefinition(od, "ID", "编号", "id", OleDbType.VarWChar, 32, 0, true) { ShowInList = false });
            attrs.Add("Name",new AttributeDefinition(od, "Name", "员工名", "username", OleDbType.VarWChar, 10, 1, true));
            attrs.Add("Password",new AttributeDefinition(od, "Password", "密码", "password", OleDbType.VarWChar, 10, 2, true){ShowInList = false});
            attrs.Add("Role",new AttributeDefinition(od, "Role", "角色", "role", OleDbType.Integer, 3));
            return attrs;
        }
    }
}
