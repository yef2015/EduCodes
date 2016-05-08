using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.DAL
{
    public class ClassAttach
    {
        public ClassAttach()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(Guid Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ClassAttach");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Models.ClassAttach model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ClassAttach(");
            strSql.Append("Id,ClassId,Name,Url,IsValid,CreateId,CreateTime,FileType,Remark,CreateUserName)");
            strSql.Append(" values (");
            strSql.Append("@Id,@ClassId,@Name,@Url,@IsValid,@CreateId,@CreateTime,@FileType,@Remark,@CreateUserName)");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ClassId", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,500),
					new SqlParameter("@Url", SqlDbType.NVarChar,500),
					new SqlParameter("@IsValid", SqlDbType.Bit,1),
					new SqlParameter("@CreateId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@FileType", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
                    new SqlParameter("@CreateUserName", SqlDbType.NVarChar,50)};
            parameters[0].Value =model.Id;
            parameters[1].Value = model.ClassId;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.Url;
            parameters[4].Value = model.IsValid;
            parameters[5].Value = Guid.NewGuid();
            parameters[6].Value = model.CreateTime;
            parameters[7].Value = model.FileType;
            parameters[8].Value = model.Remark;
            parameters[9].Value = model.CreateUserName;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Models.ClassAttach model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ClassAttach set ");
            strSql.Append("ClassId=@ClassId,");
            strSql.Append("Name=@Name,");
            strSql.Append("Url=@Url,");
            strSql.Append("IsValid=@IsValid,");
            strSql.Append("CreateId=@CreateId,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("FileType=@FileType,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CreateUserName=@CreateUserName");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@ClassId", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,500),
					new SqlParameter("@Url", SqlDbType.NVarChar,500),
					new SqlParameter("@IsValid", SqlDbType.Bit,1),
					new SqlParameter("@CreateId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@FileType", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CreateUserName", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ClassId;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Url;
            parameters[3].Value = model.IsValid;
            parameters[4].Value = model.CreateId;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = model.FileType;
            parameters[7].Value = model.Remark;
            parameters[8].Value = model.Id;
            parameters[9].Value = model.CreateUserName;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(Guid Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ClassAttach ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }  
        
        public bool DeleteLogic(Guid Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ClassAttach  set IsValid=0 ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ClassAttach ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Models.ClassAttach GetModel(Guid Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,ClassId,Name,Url,IsValid,CreateId,CreateTime,FileType,Remark,CreateUserName from ClassAttach ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = Id;

            ClassAttach model = new ClassAttach();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Models.ClassAttach DataRowToModel(DataRow row)
        {
            Models.ClassAttach model = new Models.ClassAttach();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = new Guid(row["Id"].ToString());
                }
                if (row["ClassId"] != null && row["ClassId"].ToString() != "")
                {
                    model.ClassId = int.Parse(row["ClassId"].ToString());
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["Url"] != null)
                {
                    model.Url = row["Url"].ToString();
                }
                if (row["IsValid"] != null && row["IsValid"].ToString() != "")
                {
                    if ((row["IsValid"].ToString() == "1") || (row["IsValid"].ToString().ToLower() == "true"))
                    {
                        model.IsValid = true;
                    }
                    else
                    {
                        model.IsValid = false;
                    }
                }
                if (row["CreateId"] != null && row["CreateId"].ToString() != "")
                {
                    model.CreateId = new Guid(row["CreateId"].ToString());
                }
                if (row["CreateUserName"] != null)
                {
                    model.CreateUserName = row["CreateUserName"].ToString();
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }
                if (row["FileType"] != null)
                {
                    model.FileType = row["FileType"].ToString();
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ClassId,Name,Url,IsValid,CreateId,CreateTime,FileType,Remark,CreateUserName  ");
            strSql.Append(" FROM ClassAttach ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Id,ClassId,Name,Url,IsValid,CreateId,CreateTime,FileType,Remark,CreateUserName ");
            strSql.Append(" FROM ClassAttach ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM ClassAttach ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.Id desc");
            }
            strSql.Append(")AS Row, T.*  from ClassAttach T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "ClassAttach";
            parameters[1].Value = "Id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
