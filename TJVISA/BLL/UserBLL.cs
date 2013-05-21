using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TJVISA.DAL;
using TJVISA.Entity;

namespace TJVISA.BLL
{
    public class UserBLL
    {
        public IUser Get(string id)
        {
            try
            {
                using (var cmd = new DBCommand())
                {
                    return UserDAL.Get(cmd, id);
                }
            }
            catch (Exception ex)
            {
                using (var builder = new MessageBuilder())
                {
                    string err = builder.AppendLine("读取用户信息错误！").AppendLine(ex).Message;
                    throw new Exception(err);
                }
            }
        }

        public IUser Get(string name, string password)
        {
            using (var cmd = new DBCommand())
            {
                try
                {
                    var od = AppCore.AppSingleton.FindObjDef<UserObjDef>();
                    IUser user = UserDAL.Get(cmd,
                                             new Dictionary<IAttributeDefinition, object>()
                                                 {{od.Name, name}, {od.Password, password}});
                    return user;
                }
                catch (Exception ex)
                {
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("查找用户错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }

        public bool IsExisting(string name)
        {
            using (var cmd = new DBCommand())
            {
                try
                {
                    var od = AppCore.AppSingleton.FindObjDef<UserObjDef>();
                    return UserDAL.IsExisting(cmd,
                                              new Dictionary<IAttributeDefinition, object>() {{od.Name, name}});
                }
                catch (Exception ex)
                {
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("查找用户错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }

        public IUser Create(string name, string password, UserRole? role)
        {
            using (var cmd = new DBCommand())
            {
                try
                {
                    string id = Guid.NewGuid().ToString();
                    IUser instance = UserDAL.Create(cmd, id,name, password, role);
                    cmd.Commit();
                    return instance;
                }
                catch (Exception ex)
                {
                    cmd.RollBack();
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("创建用户信息错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }

        public IUser Update(IUser instance, string name, string password, UserRole? role)
        {
            using (var cmd = new DBCommand())
            {
                try
                {
                    UserDAL.Update(cmd, instance, name, password,role);

                    cmd.Commit();
                    return instance;
                }
                catch (Exception ex)
                {
                    cmd.RollBack();
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("更新用户信息错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }

        public IUser Delete(IUser instance)
        {
            using (var cmd = new DBCommand())
            {
                try
                {
                    UserDAL.Delete(cmd, instance);
                    cmd.Commit();
                    return instance;
                }
                catch (Exception ex)
                {
                    cmd.RollBack();
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("删除用户信息错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }

        public IList<IUser> GetAll(IDictionary<IAttributeDefinition, object> criterias)
        {
            using (var cmd = new DBCommand())
            {
                try
                {
                    return UserDAL.GetAll(cmd, criterias);
                }
                catch (Exception ex)
                {
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("读取用户信息列表错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }

        public IList<IUser> GetAllPaged(IDictionary<IAttributeDefinition, object> criterias, ref int pageNum, ref int pageCount, int pageSize, ref int itemCount)
        {
            using (var cmd = new DBCommand())
            {
                try
                {
                    return UserDAL.GetAllPaged(cmd, criterias, ref pageNum, pageSize, ref pageCount,ref itemCount);
                }
                catch (Exception ex)
                {
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("读取用户信息列表错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }

        public IUser CreateSuperUser(string name)
        {
            return UserDAL.CreateSuperUser(name);
        }

        public IUser CreateAuthenticatedUser(string id,string name,UserRole? role)
        {
            return UserDAL.CreateAuthenticatedUer(id, name, role);
        }
    }
}
