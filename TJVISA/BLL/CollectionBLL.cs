using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TJVISA.DAL;
using TJVISA.Entity;

namespace TJVISA.BLL
{
    public class CollectionBLL
    {
        public ICollection Get(string id)
        {
            try
            {
                using (var cmd = new DBCommand())
                {
                    return CollectionDAL.Get(cmd, id);
                }
            }
            catch (Exception ex)
            {
                using (var builder = new MessageBuilder())
                {
                    string err = builder.AppendLine("读取付费信息错误！").AppendLine(ex).Message;
                    throw new Exception(err);
                }
            }
        }

        public ICollection Create(string id, decimal? value, decimal? offset)
        {
            using (var cmd = new DBCommand())
            {
                try
                {
                    ICollection instance = CollectionDAL.Create(cmd, id, value,offset);
                    cmd.Commit();
                    return instance;
                }
                catch (Exception ex)
                {
                    cmd.RollBack();
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("创建付费信息错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }

        public ICollection Update(ICollection instance, decimal? value, decimal? offset)
        {
            using (var cmd = new DBCommand())
            {
                try
                {
                    CollectionDAL.Update(cmd, instance, value,offset);

                    cmd.Commit();
                    return instance;
                }
                catch (Exception ex)
                {
                    cmd.RollBack();
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("更新付费信息错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }

        public ICollection Delete(ICollection instance)
        {
            using (var cmd = new DBCommand())
            {
                try
                {
                    CollectionDAL.Delete(cmd, instance);
                    cmd.Commit();
                    return instance;
                }
                catch (Exception ex)
                {
                    cmd.RollBack();
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("删除付费信息错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }

        public bool IsExisting(string appId)
        {
            using (var cmd = new DBCommand())
            {
                try
                {
                    var od = AppCore.AppSingleton.FindObjDef<CollectionObjDef>();
                    return CollectionDAL.IsExisting(cmd,
                                              new Dictionary<IAttributeDefinition, object>() { { od.ID, appId } });
                }
                catch (Exception ex)
                {
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("查找付款记录错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }
    }
}
