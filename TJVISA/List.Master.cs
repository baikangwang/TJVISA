using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TJVISA
{
    public partial class List : System.Web.UI.MasterPage
    {
        public ContentPlaceHolder ListContainer
        {
            get { return ListContent; }
        }

    }
}
