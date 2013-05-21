using System;
using TJVISA.Entity;
using TJVISA.Manager;

namespace TJVISA
{
    public partial class UserDetails : BaseEditPage
    {
        private UserManager _mgr;
        private IUser _item;

        public override string EntityName
        {
            get
            {
                return "User";
            }
        }

        protected UserManager Manager
        {
            get { return _mgr ?? (_mgr = new UserManager()); }
        }
        
        protected IUser Item
        {
            get
            {
                if(_item==null)
                {
                    if (!string.IsNullOrEmpty(ItemId))
                        _item = Manager.GetById(ItemId);
                }
                return _item;
            }
        }

        public override bool Save()
        {
            try
            {
                string name = txtUserName.Text;
                int roleVal = int.Parse(cmbRole.SelectedValue);

                if (string.IsNullOrEmpty(ItemId))
                {
                    string password = txtPassword.Text;

                    Manager.Create(name, password, (UserRole)roleVal);
                }
                else
                {
                    Manager.Update(Item, name, (UserRole)roleVal);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected override void Initialize()
        {
            if (string.IsNullOrEmpty(ItemId))
            {
                txtUserName.Text = "";
                txtPassword.Text = "";
                cmbRole.SelectedValue = ((int)UserRole.Client).ToString();
            }
            else
            {
                txtUserName.Text = Item.Name;
                txtPassword.Text = "***";
                cmbRole.SelectedValue = ((int)Item.Role.GetValueOrDefault(UserRole.Client)).ToString();
            }
        }
    }
}
