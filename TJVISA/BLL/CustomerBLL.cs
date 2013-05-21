using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TJVISA.DAL;
using TJVISA.Entity;

namespace TJVISA.BLL
{
    public class CustomerBLL
    {
        public ICustomer Get(string id)
        {
            try
            {
                using (var cmd = new DBCommand())
                {
                    return CustomerDAL.Get(cmd, id);
                }
            }
            catch (Exception ex)
            {
                using (var builder = new MessageBuilder())
                {
                    string err = builder.AppendLine("读取客户信息错误！").AppendLine(ex).Message;
                    throw new Exception(err);
                }
            }
        }

        public ICustomer Create(string id, string name, string sex, string phone, string address)
        {
            using (var cmd = new DBCommand())
            {
                try
                {

                    ICustomer instance = CustomerDAL.Create(cmd, id,name,sex,phone,address);
                    cmd.Commit();
                    return instance;
                }
                catch (Exception ex)
                {
                    cmd.RollBack();
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("创建客户信息错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }

        public ICustomer Update(ICustomer instance, string id, string name, string sex, string phone, string address)
        {
            using (var cmd = new DBCommand())
            {
                try
                {
                    CustomerDAL.Update(cmd,instance,id, name,sex,phone,address);

                    cmd.Commit();
                    return instance;
                }
                catch (Exception ex)
                {
                    cmd.RollBack();
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("更新客户信息错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }

        public ICustomer Delete(ICustomer instance)
        {
            using (var cmd = new DBCommand())
            {
                try
                {
                    CustomerDAL.Delete(cmd, instance);
                    cmd.Commit();
                    return instance;
                }
                catch (Exception ex)
                {
                    cmd.RollBack();
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("删除客户信息错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }

        public IList<ICustomer> GetAll(IDictionary<IAttributeDefinition, object> criterias)
        {
            using (var cmd = new DBCommand())
            {
                try
                {
                    return CustomerDAL.GetAll(cmd, criterias);
                }
                catch (Exception ex)
                {
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("读取客户信息列表错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }

        public IList<ICustomer> GetAllPaged(IDictionary<IAttributeDefinition, object> criterias, ref int pageNum, ref int pageCount, int pageSize,ref int itemCount)
        {
            using (var cmd = new DBCommand())
            {
                try
                {
                    return CustomerDAL.GetAllPaged(cmd, criterias, ref pageNum, pageSize, ref pageCount,ref itemCount);
                }
                catch (Exception ex)
                {
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("读取客户信息列表错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }

        public bool IsExisting(string customerId)
        {
            using (var cmd = new DBCommand())
            {
                try
                {
                    var od = AppCore.AppSingleton.FindObjDef<CustomerObjDef>();
                    return CustomerDAL.IsExisting(cmd,
                                              new Dictionary<IAttributeDefinition, object>() { { od.ID, customerId } });
                }
                catch (Exception ex)
                {
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("查找申请人错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }
    }
}
