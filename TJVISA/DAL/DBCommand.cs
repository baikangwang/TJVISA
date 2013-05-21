using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TJVISA.Entity;

namespace TJVISA.DAL
{
    public class DBCommand:IDisposable
    {
        private readonly OleDbCommand _cmd;
        private bool _commit = true;
        private bool _rollBack;

        public DBCommand()
        {
            _cmd = new OleDbCommand
            {
                Connection =
                    new OleDbConnection(
                    ConfigurationManager.ConnectionStrings["TJVISA"].
                        ConnectionString)
            };

            if (_cmd.Connection.State == ConnectionState.Closed)
            {
                try
                {
                    _cmd.Connection.Open();
                }
                catch
                {
                    ((IDisposable)this).Dispose();
                    throw new Exception("无法连接数据库，请联系系统管理员。");
                }
            }

            _cmd.Transaction = _cmd.Connection.BeginTransaction();

        }

        public T Get<T>(string entityName, string id) where T : class
        {
            if (!AppCore.AppSingleton.ObjectDefinitions.ContainsKey(entityName))
                Debug.Assert(false, "Unimplemented entity [" + entityName + "]");
            IObjectDefinition od = AppCore.AppSingleton.ObjectDefinitions[entityName];

            string tableName = string.Format("[{0}]", od.TableName);
            string fileds = string.Join(",",
                                        od.Attributes.Values.Select(a => string.Format("[{0}]", a.Column)).ToArray());
            string criterion = string.Format("[{0}] = @{0}", od.PrimaryKey.Column);

            string sqlContext = string.Format("select {0} from {1} where {2}", fileds, tableName, criterion);

            _cmd.CommandText = sqlContext;
            _cmd.Parameters.Clear();
            _cmd.Parameters.Add("@" + od.PrimaryKey.Column, OleDbType.VarWChar, od.PrimaryKey.Length.GetValueOrDefault(50)).Value = id;
            T rel = null;

            using (OleDbDataReader reader = _cmd.ExecuteReader())
            {
                if(reader!=null)
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        rel = EntityReader.Read<T>(reader, entityName);
                        reader.Close();
                    }
                }
            }
            return rel;
        }

        public IList<T> GetAll<T>(string entityName, IDictionary<IAttributeDefinition, object> criterias) where T:class
        {
            int pageIndex = 0;
            int pageCount = 1;
            int itemCount = 10;

            return GetAllPaged<T>(entityName, criterias, ref pageCount, ref pageIndex, ref itemCount,-1);
        }

        public IList<T> GetAllPaged<T>(string entityName, IDictionary<IAttributeDefinition, object> criterias, ref int pageCount, ref int pageNum, ref int itemCount, int pageSize) where T:class
        {
            if (!AppCore.AppSingleton.ObjectDefinitions.ContainsKey(entityName))
                Debug.Assert(false, "Unimplemented entity [" + entityName + "]");
            IObjectDefinition od = AppCore.AppSingleton.ObjectDefinitions[entityName];

            string table = string.Format("[{0}]", od.TableName);
            string fileds = string.Join(",", od.Attributes.Values.Select(a => string.Format("[{0}]", a.Column)).ToArray());
            string criterion = string.Join(" and ",
                                           criterias.Keys.Select(a => string.Format("[{0}]=@{0}", a.Column)).ToArray());

            var sqlBuilder=new StringBuilder();

            if (pageSize == -1)
            {
                sqlBuilder.AppendLine(string.Format("select {0} from {1}", fileds, table));
                if (!string.IsNullOrEmpty(criterion))
                    sqlBuilder.AppendLine(string.Format("where {0}", criterion));
            }
            else
            {
                itemCount = Count(entityName, criterias);
                int count = GetPageCount(ref itemCount, pageSize);

                if (pageCount != count)
                {
                    if (pageNum != 1 && pageNum == pageCount)
                    {
                        pageNum = count;
                    }
                    pageCount = count;
                }
                
                string fieldsWithAlias = "comp."+ fileds.Replace(",", ",comp.");
                string criterionWithAlias = string.IsNullOrEmpty(criterion) ? "" : "comp." + criterion.Replace(" and ", " and comp.");

                int pageStart = (pageNum - 1) * pageSize + 1;
                int pageEnd = (pageNum - 1) * pageSize + pageSize;

                sqlBuilder.AppendLine(string.Format("Select top {1} {0}", fileds, pageSize));
                sqlBuilder.AppendLine(string.Format("FROM (SELECT {1}, (select count(org.[{2}])+1 from {0} [org] where org.[{2}] <comp.[{2}]) as Rank",
                                  table, fieldsWithAlias, od.PrimaryKey.Column));
                sqlBuilder.AppendLine(string.Format("FROM {0} [comp]", table));
                if (!string.IsNullOrEmpty(criterionWithAlias))
                    sqlBuilder.AppendLine(string.Format("Where {0}", criterionWithAlias));
                sqlBuilder.AppendLine(string.Format("ORDER BY comp.[{0}]", od.PrimaryKey.Column));
                sqlBuilder.AppendLine(")");
                sqlBuilder.AppendLine(string.Format("where Rank between {0} and {1};", pageStart, pageEnd));
            }

            _cmd.CommandText = sqlBuilder.ToString();
            
            if(pageSize==-1)
            {
                _cmd.Parameters.Clear();
                foreach (IAttributeDefinition attr in criterias.Keys)
                {
                    var para = new OleDbParameter("@" + attr.Column, attr.DataType);
                    if (attr.Length.HasValue)
                        para.Size = attr.Length.Value;
                    para.Value = criterias[attr]??DBNull.Value;
                    para.IsNullable = !attr.IsRequired;
                    _cmd.Parameters.Add(para);
                }
            }

            IList<T> list = new List<T>();

            using (OleDbDataReader reader = _cmd.ExecuteReader())
            {
                if(reader!=null)
                {
                    while (reader.Read())
                    {
                        try
                        {
                            list.Add(EntityReader.Read<T>(reader, entityName));
                        }
                        catch
                        {
                            break;
                        }
                    }
                    reader.Close();
                }
            }

            return list;
        }

        public int Count(string entityName,IDictionary<IAttributeDefinition,object> criterias )
        {
            if (!AppCore.AppSingleton.ObjectDefinitions.ContainsKey(entityName))
                Debug.Assert(false, "Unimplemented entity [" + entityName + "]");
            IObjectDefinition od = AppCore.AppSingleton.ObjectDefinitions[entityName];

            string table = string.Format("[{0}]", od.TableName);
            string criterion = string.Join(" and ",
                                           criterias.Keys.Select(a => string.Format("[{0}]=@{0}", a.Column)).ToArray());
            string sqlContext = string.Format("select count(*) from {0} ", table);
            if (!string.IsNullOrEmpty(criterion))
                sqlContext += string.Format("where {0}", criterion);

            _cmd.CommandText = sqlContext;

            _cmd.Parameters.Clear();
            foreach (IAttributeDefinition attr in criterias.Keys)
            {
                var para = new OleDbParameter("@" + attr.Column, attr.DataType);
                if (attr.Length.HasValue)
                    para.Size = attr.Length.Value;
                para.Value = criterias[attr]??DBNull.Value;
                para.IsNullable = !attr.IsRequired;
                _cmd.Parameters.Add(para);
            }

            object result = _cmd.ExecuteScalar();

            return result == null ? 0 : Convert.ToInt32(result);
        }

        public int Count(string entityName, string sql, IDictionary<IAttributeDefinition, object> criterias)
        {
            //if (!AppCore.AppSingleton.ObjectDefinitions.ContainsKey(entityName))
            //    Debug.Assert(false, "Unimplemented entity [" + entityName + "]");
            //IObjectDefinition od = AppCore.AppSingleton.ObjectDefinitions[entityName];

            //string table = string.Format("[{0}]", od.TableName);
            //string criterion = string.Join(" and ",
            //                               criterias.Keys.Select(a => string.Format("[{0}]=@{0}", a.Column)).ToArray());
            string sqlContext = string.Format("select count(*) from ({0}) ", sql);
            //if (!string.IsNullOrEmpty(criterion))
            //    sqlContext += string.Format("where {0}", criterion);

            _cmd.CommandText = sqlContext;

            _cmd.Parameters.Clear();
            foreach (IAttributeDefinition attr in criterias.Keys)
            {
                var para = new OleDbParameter("@" + attr.Column, attr.DataType);
                if (attr.Length.HasValue)
                    para.Size = attr.Length.Value;
                para.Value = criterias[attr] ?? DBNull.Value;
                para.IsNullable = !attr.IsRequired;
                _cmd.Parameters.Add(para);
            }

            object result = _cmd.ExecuteScalar();

            return result == null ? 0 : Convert.ToInt32(result);
        }

        protected int GetPageCount(ref int itemCount, int pageSize)
        {
            try
            {
                itemCount = itemCount <= 0 ? 1 : itemCount;
                int pageCount = pageSize <= 0
                                          ? 1
                                          : itemCount <= pageSize
                                                ? 1
                                                : itemCount % pageSize == 0
                                                      ? itemCount / pageSize
                                                      : (itemCount / pageSize + 1);
                return pageCount;
            }
            catch
            {
                return 1;
            }
        }

        public T Create<T>(string entityName, IDictionary<IAttributeDefinition, object> values) where T:class
        {
            if (!AppCore.AppSingleton.ObjectDefinitions.ContainsKey(entityName))
                Debug.Assert(false, "Unimplemented entity [" + entityName + "]");
            IObjectDefinition od = AppCore.AppSingleton.ObjectDefinitions[entityName];

            string tableName = od.TableName;
            string fileds = string.Join(",", od.Attributes.Values.Where(a=>!a.IsReadOnly).Select(a => string.Format("[{0}]", a.Column)).ToArray());
            string vals = string.Join(",", od.Attributes.Values.Where(a => !a.IsReadOnly).Select(a => "@" + a.Column).ToArray());

            string sqlContext = string.Format("insert into [{0}] ({1}) values ({2})", tableName, fileds, vals);
            _cmd.CommandText = sqlContext;
            _cmd.Parameters.Clear();

            foreach (IAttributeDefinition attr in od.Attributes.Values)
            {
                if(attr.IsReadOnly)continue;
                
                var para = new OleDbParameter("@" + attr.Column, attr.DataType);
                if (attr.Length.HasValue)
                    para.Size = attr.Length.Value;
                if(!values.ContainsKey(attr))
                    Debug.Assert(false, "the argument of attribute ["+attr.Name+"] of entity [" + entityName + "] was provided.");
                para.Value = values[attr] ?? DBNull.Value;
                para.IsNullable = !attr.IsRequired;
                _cmd.Parameters.Add(para);
            }

            int affectrRows = _cmd.ExecuteNonQuery();
            if(affectrRows!=1)
                throw new Exception("fails to create instance of entity "+entityName);

            if(!values.ContainsKey(od.PrimaryKey))
                Debug.Assert(false, "the argument of attribute [" + od.PrimaryKey.Name + "] of entity [" + entityName + "] was provided.");

            return EntityCreator.Create<T>(entityName, values);
        }

        public T Update<T>(string entityName,T instance) where T:class 
        {
            if(instance==null)
                throw new ArgumentNullException("instance");
            
            IDictionary<IAttributeDefinition, object> changes = EntityChecker.Check(entityName, instance as IBaseObject);

            if (changes.Count == 0) return instance;

            if (!AppCore.AppSingleton.ObjectDefinitions.ContainsKey(entityName))
                Debug.Assert(false, "Unimplemented entity [" + entityName + "]");
            IObjectDefinition od = AppCore.AppSingleton.ObjectDefinitions[entityName];

            string table = od.TableName;
            string fields = string.Join(",", changes.Keys.Select(a => string.Format("[{0}]=@{0}", a.Column)).ToArray());
            string criterion = string.Format("[{0}]=@orgID", od.PrimaryKey.Column);

            string sqlContext = string.Format("update [{0}] set {1} where {2}",table,fields,criterion);

            _cmd.CommandText = sqlContext;
            _cmd.Parameters.Clear();
            foreach (IAttributeDefinition attr in changes.Keys)
            {
                var para = new OleDbParameter("@" + attr.Column, attr.DataType);
                if (attr.Length.HasValue)
                    para.Size = attr.Length.Value;
                para.Value = changes[attr]??DBNull.Value;
                para.IsNullable = !attr.IsRequired;
                _cmd.Parameters.Add(para);
            }

            if (!(instance is IBaseObjectProxy))
                Debug.Assert(false, "The given instance is not proxy type.");

            _cmd.Parameters.Add("@orgID", od.PrimaryKey.DataType,
                                od.PrimaryKey.Length.GetValueOrDefault(50)).
                Value = (instance as IBaseObjectProxy).Original.ID;


            int affectrRows = _cmd.ExecuteNonQuery();
            if (affectrRows != 1)
                throw new Exception("fails to update instance of entity " + entityName);

            return EntityUpdater.Update(this, entityName, instance, changes);
        }

        public T Delete<T>(string entityName,T instance) where T:class
        {
            if (instance == null)
                throw new ArgumentNullException("instance");
            
            if (!AppCore.AppSingleton.ObjectDefinitions.ContainsKey(entityName))
                Debug.Assert(false, "Unimplemented entity [" + entityName + "]");
            IObjectDefinition od = AppCore.AppSingleton.ObjectDefinitions[entityName];

            string table = od.TableName;
            string criterion = string.Format("[{0}]=@{0}", od.PrimaryKey.Column);
            string sqlContext = string.Format("delete from [{0}] where {1}", table, criterion);

            _cmd.CommandText = sqlContext;
            _cmd.Parameters.Clear();
            _cmd.Parameters.Add("@" + od.PrimaryKey.Column, od.PrimaryKey.DataType, od.PrimaryKey.Length.GetValueOrDefault(50)).
                Value = (instance as IBaseObject).ID;

            int affectrRows = _cmd.ExecuteNonQuery();
            if (affectrRows != 1)
                throw new Exception("fails to update instance of entity " + entityName);

            return instance;
        }

        //public IBaseObject GetWithCustomSql(string entityName, string sql)
        //{
        //    return null;
        //}


        public IList<T> GetAllPaged<T>(string entityName, string sql, IDictionary<IAttributeDefinition, object> criterias, ref int pageCount, ref int pageNum, ref int itemCount, int pageSize) where T : class
        {
            if (!AppCore.AppSingleton.ObjectDefinitions.ContainsKey(entityName))
                Debug.Assert(false, "Unimplemented entity [" + entityName + "]");
            IObjectDefinition od = AppCore.AppSingleton.ObjectDefinitions[entityName];

            //string table = string.Format("[{0}]", od.TableName);
            string fileds = string.Join(",", od.Attributes.Values.Select(a => string.Format("[{0}]", a.Column)).ToArray());
            //string criterion = string.Join(" and ",
            //                               criterias.Keys.Select(a => string.Format("[{0}]=@{0}", a.Column)).ToArray());

            var sqlBuilder = new StringBuilder();

            if (pageSize == -1)
            {
                //sqlBuilder.AppendLine(string.Format("select {0} from {1}", fileds, table));
                if (!string.IsNullOrEmpty(sql))
                    sqlBuilder.AppendLine(sql);
            }
            else
            {
                itemCount = Count(entityName, sql,criterias);
                int count = GetPageCount(ref itemCount, pageSize);

                if (pageCount != count)
                {
                    if (pageNum != 1 && pageNum == pageCount)
                    {
                        pageNum = count;
                    }
                    pageCount = count;
                }

                int pageStart = (pageNum - 1) * pageSize + 1;
                int pageEnd = (pageNum - 1) * pageSize + pageSize;

                sqlBuilder.AppendLine(string.Format("Select top {1} {0}", fileds, pageSize));
                sqlBuilder.AppendLine(string.Format("FROM ({0})",sql));
                sqlBuilder.AppendLine(string.Format("where Rank between {0} and {1};", pageStart, pageEnd));
            }

            _cmd.CommandText = sqlBuilder.ToString();

            if (pageSize == -1)
            {
                _cmd.Parameters.Clear();
                foreach (IAttributeDefinition attr in criterias.Keys)
                {
                    var para = new OleDbParameter("@" + attr.Column, attr.DataType);
                    if (attr.Length.HasValue)
                        para.Size = attr.Length.Value;
                    para.Value = criterias[attr] ?? DBNull.Value;
                    para.IsNullable = !attr.IsRequired;
                    _cmd.Parameters.Add(para);
                }
            }

            IList<T> list = new List<T>();

            using (OleDbDataReader reader = _cmd.ExecuteReader())
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        try
                        {
                            list.Add(EntityReader.Read<T>(reader, entityName));
                        }
                        catch
                        {
                            break;
                        }
                    }
                    reader.Close();
                }
            }

            return list;
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (_cmd == null) return;

            if (_cmd.Transaction != null)
            {
                if (_commit)
                    _cmd.Transaction.Commit();
                if (_rollBack)
                    _cmd.Transaction.Rollback();
            }

            if (_cmd.Connection != null)
            {
                if (_cmd.Connection.State == ConnectionState.Open)
                    _cmd.Connection.Close();
                _cmd.Connection.Dispose();
            }

            _cmd.Dispose();
        }

        #endregion

        public void Commit()
        {
            _commit = true;
            _rollBack = false;
        }

        public void RollBack()
        {
            _commit = false;
            _rollBack = true;
        }
    }
}
