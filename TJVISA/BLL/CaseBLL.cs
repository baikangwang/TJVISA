using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TJVISA.DAL;
using TJVISA.Entity;

namespace TJVISA.BLL
{
    public class CaseBLL
    {
        public ICase Get(string id)
        {
            try
            {
                using (var cmd = new DBCommand())
                {
                    return CaseDAL.Get(cmd, id);
                }
            }
            catch (Exception ex)
            {
                using (var builder = new MessageBuilder())
                {
                    string err = builder.AppendLine("读取问题信息错误！").AppendLine(ex).Message;
                    throw new Exception(err);
                }
            }
        }

        public ICase Create(string id, string type, string text)
        {
            using (var cmd = new DBCommand())
            {
                try
                {
                    ICase instance = CaseDAL.Create(cmd, id, type, text);
                    cmd.Commit();
                    return instance;
                }
                catch (Exception ex)
                {
                    cmd.RollBack();
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("创建问题信息错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }

        public ICase Update(ICase instance, string type, string text)
        {
            using (var cmd = new DBCommand())
            {
                try
                {
                    CaseDAL.Update(cmd, instance, type, text);

                    cmd.Commit();
                    return instance;
                }
                catch (Exception ex)
                {
                    cmd.RollBack();
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("更新问题信息错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }

        public ICase Delete(ICase instance)
        {
            using (var cmd = new DBCommand())
            {
                try
                {
                    CaseDAL.Delete(cmd, instance);
                    cmd.Commit();
                    return instance;
                }
                catch (Exception ex)
                {
                    cmd.RollBack();
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("删除问题信息错误！").AppendLine(ex).Message;
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
                    var od = AppCore.AppSingleton.FindObjDef<CaseObjDef>();
                    return CaseDAL.IsExisting(cmd,
                                              new Dictionary<IAttributeDefinition, object>() { { od.ID, appId } });
                }
                catch (Exception ex)
                {
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("查找问题案错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }
    }
}
