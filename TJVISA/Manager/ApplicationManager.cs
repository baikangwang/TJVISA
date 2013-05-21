using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TJVISA.BLL;
using TJVISA.Entity;

namespace TJVISA.Manager
{
    public class ApplicationManager:IManager
    {
        protected ApplicationBLL ApplicationBLL { get; set; }
        protected CustomerBLL CustomerBLL { get; set; }
        protected CaseBLL CaseBLL { get; set; }
        protected CollectionBLL CollectionBLL { get; set; }
        protected StatusBLL StatusBLL { get; set; }

        public ApplicationManager()
        {
            ApplicationBLL=new ApplicationBLL();
            CustomerBLL=new CustomerBLL();
            CaseBLL=new CaseBLL();
            CollectionBLL = new CollectionBLL();
            StatusBLL=new StatusBLL();
        }
        
        #region Implementation of IDisposable

        public void Dispose()
        {
            ApplicationBLL = null;
            CustomerBLL = null;
            CaseBLL = null;
            CollectionBLL = null;
            StatusBLL = null;
        }

        public DataTable GetSource(IDictionary<IAttributeDefinition, object> criterias, PagingSetting pagingSetting)
        {
            int pageNum = pagingSetting.PageNum;
            int pageCount = pagingSetting.PageCount;
            int itemCount = pagingSetting.ItemCount;

            IList<IApplication> items = ApplicationBLL.GetAllPaged(criterias, ref pageNum,
                                                     ref pageCount, pagingSetting.PageSize, ref itemCount);
            pagingSetting.PageNum = pageNum;
            pagingSetting.PageCount = pageCount;
            pagingSetting.ItemCount = itemCount;

            var od = AppCore.AppSingleton.FindObjDef<ApplicationObjDef>();

            DataTable source = new DataTable("ApplicaitonList");
            source.Columns.AddRange(new DataColumn[]
                                        {
                                            new DataColumn(od.ID.Name,typeof(System.String)),
                                            new DataColumn(od.Type.Name,typeof(System.String)),
                                            new DataColumn(od.SubType.Name,typeof(System.String)),
                                            new DataColumn(od.CustomerID.Name,typeof(System.String)),
                                            new DataColumn(od.CustomerName.Name,typeof(System.String)),
                                            new DataColumn(od.CustomerSex.Name,typeof(System.String)),
                                            new DataColumn(od.Region.Name,typeof(System.String)),
                                            new DataColumn(od.DateApplied.Name,typeof(System.String)),
                                            new DataColumn(AppCore.AppSingleton.FindObjDef<StatusObjDef>().Value.Name,typeof(System.String)), 
                                        });

            foreach (IApplication item in items)
            {
                source.Rows.Add(new object[]
                                    {
                                        item.ID,
                                        item.Type,
                                        item.SubType,
                                        item.CustomerID,
                                        item.CustomerName,
                                        item.CustomerSex,
                                        item.Region,
                                        item.DateApplied,
                                        item.Status==null?JobStatus.None.ToLabel():item.Status.Value
                                    });
            }

            return source;
        }

        #endregion

        public IApplication Create(string appId,
            string customerId,string customerName,string customerSex,string customerPhone,string customerAddress,
            string region,DateTime? dateApplied,DateTime? dateTraved,string offNoteNo, DateTime? offNoteDate,string remark)
        {
            ICustomer customer;
            if (CustomerBLL.IsExisting(customerId))
            {
                customer = CustomerBLL.Get(customerId);
                customer = CustomerBLL.Update(customer, customerId, customerName, customerSex, customerPhone,
                                              customerAddress);
            }
            else
                customer = CustomerBLL.Create(customerId, customerName, customerSex, customerPhone,
                                                customerAddress);

            IApplication app = ApplicationBLL.Create(appId, customer.ID, region, dateApplied, dateTraved, offNoteNo,
                                                     offNoteDate,
                                                     remark);
            IStatus status = StatusBLL.Create(appId, JobStatus.None.ToLabel());
            return app;
        }

        public IApplication GetById(string id)
        {
            return ApplicationBLL.Get(id);
        }

        public IApplication Update(IApplication item, string appId, ICustomer customer,
            string customerId, string customerName, string customerSex, string customerPhone, string customerAddress,
            string region, DateTime? dateApplied, DateTime? dateTraved, string offNoteNo, DateTime? offNoteDate, string remark)
        {
            if (!CustomerBLL.IsExisting(customerId))
                customer = CustomerBLL.Create(customerId, customerName, customerSex, customerPhone, customerAddress);
            customer = CustomerBLL.Update(customer, customerId, customerName, customerSex, customerPhone,
                                          customerAddress);

            return ApplicationBLL.Update(item, appId, customer.ID, region, dateApplied, dateTraved, offNoteNo,
                                         offNoteDate, remark);

        }

        public IApplication Delete(string id)
        {
            IApplication app = ApplicationBLL.Get(id);
            if (app != null)
                app = ApplicationBLL.Delete(app);
            ICase c = CaseBLL.Get(id);
            if (c != null)
                c = CaseBLL.Delete(c);
            ICollection col = CollectionBLL.Get(id);
            if (col != null)
                col = CollectionBLL.Delete(col);
            
            return app;
        }
    }
}
