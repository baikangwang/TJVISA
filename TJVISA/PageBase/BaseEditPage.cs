using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using TJVISA.Entity;
using Telerik.Web.UI;

namespace TJVISA
{
    public abstract class BaseEditPage:BasePage
    {
        protected RadButton SaveButton
        {
            get { return (this.Master as TJVISA.Edit).SaveButton; }
        }

        protected RadButton ReturnButton
        {
            get { return (this.Master as TJVISA.Edit).ReturnButton; }
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            Initialize();
        }

        public abstract bool Save();

        public virtual MessageBuilder ValidateInput()
        {
            return new MessageBuilder();
        }

        protected abstract void Initialize();
    }
}
