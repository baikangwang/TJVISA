using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using TJVISA.Entity;

namespace TJVISA.Entity
{
    public interface IUser:IBaseObject,IPrincipal
    {
        //string ID { get; set; }
        string Name { get; set; }
        string Password { get; set; }
        UserRole? Role { get; set; }
    }
    
    public class User:BaseObject,IUser
    {
        #region Implementation of IUser

        public override string ID { get; set; }
        public virtual string Name { get; set; }
        public virtual string Password { get; set; }
        public virtual UserRole? Role { get; set; }

        #endregion

        protected User(){}

        #region Implementation of IPrincipal

        public bool IsInRole(string role)
        {
            return true;
        }

        public IIdentity Identity
        {
            get { return new GenericIdentity(Name); }
        }

        #endregion
    }

}

namespace TJVISA.DAL
{
    public sealed class UserProxy: User,IBaseObjectProxy
    {
        private readonly UserProxy _original;

        UserProxy():base()
        {
            
        }

        public UserProxy(string id, string name, string password, UserRole? role):this()
        {
            ID = id;
            Name = name;
            Password = password;
            Role = role;

            _original = new UserProxy() { ID = id, Name = name, Password = password, Role = role };
        }

        #region Implementation of IBaseObjectProxy<UserProxy>

        public IBaseObjectProxy Original
        {
            get { return _original; }
        }

        #endregion
    }
}
