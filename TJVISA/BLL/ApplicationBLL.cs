using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TJVISA.DAL;
using TJVISA.Entity;

namespace TJVISA.BLL
{
    public class ApplicationBLL
    {
        public IApplication Get(string id)
        {
            try
            {
                using (var cmd = new DBCommand())
                {
                    return ApplicationDAL.Get(cmd, id);
                }
            }
            catch (Exception ex)
            {
                using(var builder=new MessageBuilder())
                {
                    string err = builder.AppendLine("读取申请单错误！").AppendLine(ex).Message;
                    throw new Exception(err);
                }
            }
        }

        public IApplication Create(string id, string customerId, string region, DateTime? dateApplied, DateTime? dateTraved, string offNoteNo, DateTime? offNoteDate, string remark)
        {
            using (var cmd = new DBCommand())
            {
                try
                {

                    IApplication instance = ApplicationDAL.Create(cmd, id, customerId, region, dateApplied, dateTraved,
                                                                  offNoteNo, offNoteDate,
                                                                  remark);
                    cmd.Commit();
                    return instance;
                }
                catch (Exception ex)
                {
                    cmd.RollBack();
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("创建申请单错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }

        public IApplication Update(IApplication instance, string id, string customerId, string region, DateTime? dateApplied, DateTime? dateTraved, string offNoteNo, DateTime? offNoteDate, string remark)
        {
            using (var cmd=new DBCommand())
            {
                try
                {
                    ApplicationDAL.Update(cmd, instance, id, customerId, region, dateApplied, dateTraved, offNoteNo,
                                          offNoteDate, remark);
                    
                    cmd.Commit();
                    return instance;
                }
                catch (Exception ex)
                {
                    cmd.RollBack();
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("更新申请单错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }

        public IApplication Delete(IApplication instance)
        {
            using (var cmd=new DBCommand())
            {
                try
                {
                    ApplicationDAL.Delete(cmd, instance);
                    cmd.Commit();
                    return instance;
                }
                catch (Exception ex)
                {
                    cmd.RollBack();
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("删除申请单错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }

        public IList<IApplication> GetAll(IDictionary<IAttributeDefinition,object> criterias )
        {
            using (var cmd=new DBCommand())
            {
                try
                {
                    return ApplicationDAL.GetAll(cmd, criterias);
                }
                catch (Exception ex)
                {
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("读取申请单列表错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }

        public IList<IApplication> GetAllPaged(IDictionary<IAttributeDefinition, object> criterias, ref int pageNum, ref int pageCount, int pageSize, ref int itemCount)
        {
            using (var cmd = new DBCommand())
            {
                try
                {
                    return ApplicationDAL.GetAllPaged(cmd, criterias, ref pageNum,pageSize,ref pageCount,ref itemCount);
                }
                catch (Exception ex)
                {
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("读取申请单列表错误！").AppendLine(ex).Message;
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
                    var od = AppCore.AppSingleton.FindObjDef<ApplicationObjDef>();
                    return ApplicationDAL.IsExisting(cmd,
                                              new Dictionary<IAttributeDefinition, object>() { { od.ID, appId } });
                }
                catch (Exception ex)
                {
                    using (var builder = new MessageBuilder())
                    {
                        string err = builder.AppendLine("查找申请表错误！").AppendLine(ex).Message;
                        throw new Exception(err);
                    }
                }
            }
        }
    }
}
