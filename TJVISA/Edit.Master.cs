using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace TJVISA
{
    public partial class Edit : System.Web.UI.MasterPage
    {
        public ContentPlaceHolder DetailsContainer
        {
            get { return detailsContent; }
        }

        protected BaseEditPage EditPage
        {
            get { return this.Page as BaseEditPage; }
        }

        public RadButton SaveButton
        {
            get { return btSave; }
        }

        public RadButton ReturnButton
        {
            get { return btReturn; }
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            btSave.Click += new EventHandler(btSave_Click);
            btReturn.Click += new EventHandler(btReturn_Click);
        }

        protected void btReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect(EditPage.ObjectDefinition.ListPage, true);
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            using (MessageBuilder valMsg = EditPage.ValidateInput())
            {
                string err = valMsg.Message;
                
                if(!string.IsNullOrEmpty(err))
                {
                    Dialog.Error(this.Page, err);
                    return;
                }
            }

            Dialog.Info(this.Page, EditPage.Save() ? "保存成功！" : "保存失败！");
        }
    }
}
