using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TJVISA.DAL;
using TJVISA.Entity;

namespace TJVISA.BLL
{
    public class StatusBLL
    {
        public IStatus Get(string id)
        {
            try
            {
                using (var cmd = new DBCommand())
                {
                    return StatusDAL.Get(cmd, id);
                }
            }
            catch (Exception ex)
            {
                using (var builder = new MessageBuilder())
                {
                    string err = builder.AppendLine("读取状态信息错误！").AppendLine(ex).Message;
                    throw new Exception(err);
                }
            }
        }

        public IStatus Create(string id, string value)
        {
            using (var cmd = new DBCommand())
            {
                try
                {
                    IStatus instance = StatusDAL.Create(cmd, id,value);
                    cmd.Commit();
                    return instance;
                }
                catch (Exception ex)
                {
                    cmd.RollBack();
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("创建状态信息错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }

        public IStatus Update(IStatus instance, string value)
        {
            using (var cmd = new DBCommand())
            {
                try
                {
                    StatusDAL.Update(cmd, instance, value);

                    cmd.Commit();
                    return instance;
                }
                catch (Exception ex)
                {
                    cmd.RollBack();
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("更新状态信息错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }

        public IStatus Delete(IStatus instance)
        {
            using (var cmd = new DBCommand())
            {
                try
                {
                    StatusDAL.Delete(cmd, instance);
                    cmd.Commit();
                    return instance;
                }
                catch (Exception ex)
                {
                    cmd.RollBack();
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("删除状态信息错误！").AppendLine(ex).Message;
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
                    var od = AppCore.AppSingleton.FindObjDef<StatusObjDef>();
                    return StatusDAL.IsExisting(cmd,
                                              new Dictionary<IAttributeDefinition, object>() { { od.ID, appId } });
                }
                catch (Exception ex)
                {
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("查找状态记录错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }
    }
}
