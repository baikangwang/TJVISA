using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using TJVISA.Entity;

namespace TJVISA.Manager
{
    public interface IManager:IDisposable
    {
        DataTable GetSource(IDictionary<IAttributeDefinition, object> criterias,
                        PagingSetting pageSetting);
    }
}
