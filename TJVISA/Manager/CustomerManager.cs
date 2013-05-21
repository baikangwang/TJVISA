using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TJVISA.BLL;
using TJVISA.Entity;

namespace TJVISA.Manager
{
    public class CustomerManager:IManager
    {
        protected CustomerBLL CustomerBLL { get; set; }

        public CustomerManager()
        {
            CustomerBLL=new CustomerBLL();
        }
        
        #region Implementation of IDisposable

        public void Dispose()
        {
            CustomerBLL = null;
        }

        public DataTable GetSource(IDictionary<IAttributeDefinition, object> criterias, PagingSetting pagingSetting)
        {
            int pageNum = pagingSetting.PageNum;
            int pageCount = pagingSetting.PageCount;
            int itemCount = pagingSetting.ItemCount;

            IList<ICustomer> items = CustomerBLL.GetAllPaged(new Dictionary<IAttributeDefinition, object>(), ref pageNum,
                                                     ref pageCount, pagingSetting.PageSize, ref itemCount);
            pagingSetting.PageNum = pageNum;
            pagingSetting.PageCount = pageCount;
            pagingSetting.ItemCount = itemCount;

            var od = AppCore.AppSingleton.FindObjDef<CustomerObjDef>();

            DataTable source = new DataTable("CustomerList");
            source.Columns.AddRange(new DataColumn[]
                                        {
                                            new DataColumn(od.ID.Name,typeof(System.String)),
                                            new DataColumn(od.Name.Name,typeof(System.String)),
                                            new DataColumn(od.Sex.Name,typeof(System.String)),
                                            new DataColumn(od.Phone.Name,typeof(System.String)),
                                            new DataColumn(od.Address.Name,typeof(System.String)),
                                        });

            foreach (ICustomer item in items)
            {
                source.Rows.Add(new object[]
                                    {
                                        item.ID,
                                        item.Name,
                                        item.Sex,
                                        item.Phone,
                                        item.Address
                                    });
            }

            return source;
        }

        #endregion
    }
}
