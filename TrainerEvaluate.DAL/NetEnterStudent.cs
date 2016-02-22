using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.DAL
{
    /// <summary>
    /// 数据访问类:NetEnterStudent
    /// </summary>
    public partial class NetEnterStudent
    {
        public NetEnterStudent()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(Guid Guid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from NetEnterStudent");
            strSql.Append(" where Guid=@Guid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Guid", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = Guid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Models.NetEnterStudent model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into NetEnterStudent(");
            strSql.Append("Guid,StudentId,StuName,NetEnteryId,NetEnterName,CreateId,CreateName,CreateTime,LastUpdateTime,IsDelete)");
            strSql.Append(" values (");
            strSql.Append("@Guid,@StudentId,@StuName,@NetEnteryId,@NetEnterName,@CreateId,@CreateName,@CreateTime,@LastUpdateTime,@IsDelete)");
            SqlParameter[] parameters = {
					new SqlParameter("@Guid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@StudentId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@StuName", SqlDbType.NVarChar,50),
					new SqlParameter("@NetEnteryId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@NetEnterName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateId", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateTime", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.Bit,1)};
            parameters[0].Value = Guid.NewGuid();
            parameters[1].Value = model.StudentId;
            parameters[2].Value = model.StuName;
            parameters[3].Value = model.NetEnteryId;
            parameters[4].Value = model.NetEnterName;
            parameters[5].Value = model.CreateId;
            parameters[6].Value = model.CreateName;
            parameters[7].Value = model.CreateTime;
            parameters[8].Value = model.LastUpdateTime;
            parameters[9].Value = model.IsDelete;

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
        public bool Update(Models.NetEnterStudent model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update NetEnterStudent set ");
            strSql.Append("StudentId=@StudentId,");
            strSql.Append("StuName=@StuName,");
            strSql.Append("NetEnteryId=@NetEnteryId,");
            strSql.Append("NetEnterName=@NetEnterName,");
            strSql.Append("CreateId=@CreateId,");
            strSql.Append("CreateName=@CreateName,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("LastUpdateTime=@LastUpdateTime,");
            strSql.Append("IsDelete=@IsDelete");
            strSql.Append(" where Guid=@Guid ");
            SqlParameter[] parameters = {
					new SqlParameter("@StudentId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@StuName", SqlDbType.NVarChar,50),
					new SqlParameter("@NetEnteryId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@NetEnterName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateId", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateTime", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.Bit,1),
					new SqlParameter("@Guid", SqlDbType.UniqueIdentifier,16)};
            parameters[0].Value = model.StudentId;
            parameters[1].Value = model.StuName;
            parameters[2].Value = model.NetEnteryId;
            parameters[3].Value = model.NetEnterName;
            parameters[4].Value = model.CreateId;
            parameters[5].Value = model.CreateName;
            parameters[6].Value = model.CreateTime;
            parameters[7].Value = model.LastUpdateTime;
            parameters[8].Value = model.IsDelete;
            parameters[9].Value = model.Guid;

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
        public bool Delete(Guid Guid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from NetEnterStudent ");
            strSql.Append(" where Guid=@Guid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Guid", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = Guid;

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
        public bool DeleteList(string Guidlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from NetEnterStudent ");
            strSql.Append(" where Guid in (" + Guidlist + ")  ");
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
        public Models.NetEnterStudent GetModel(Guid Guid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Guid,StudentId,StuName,NetEnteryId,NetEnterName,CreateId,CreateName,CreateTime,LastUpdateTime,IsDelete from NetEnterStudent ");
            strSql.Append(" where Guid=@Guid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Guid", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = Guid;

            Models.NetEnterStudent model = new Models.NetEnterStudent();
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
        public Models.NetEnterStudent DataRowToModel(DataRow row)
        {
            Models.NetEnterStudent model = new Models.NetEnterStudent();
            if (row != null)
            {
                if (row["Guid"] != null && row["Guid"].ToString() != "")
                {
                    model.Guid = new Guid(row["Guid"].ToString());
                }
                if (row["StudentId"] != null && row["StudentId"].ToString() != "")
                {
                    model.StudentId = new Guid(row["StudentId"].ToString());
                }
                if (row["StuName"] != null)
                {
                    model.StuName = row["StuName"].ToString();
                }
                if (row["NetEnteryId"] != null && row["NetEnteryId"].ToString() != "")
                {
                    model.NetEnteryId = new Guid(row["NetEnteryId"].ToString());
                }
                if (row["NetEnterName"] != null)
                {
                    model.NetEnterName = row["NetEnterName"].ToString();
                }
                if (row["CreateId"] != null)
                {
                    model.CreateId = row["CreateId"].ToString();
                }
                if (row["CreateName"] != null)
                {
                    model.CreateName = row["CreateName"].ToString();
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }
                if (row["LastUpdateTime"] != null && row["LastUpdateTime"].ToString() != "")
                {
                    model.LastUpdateTime = DateTime.Parse(row["LastUpdateTime"].ToString());
                }
                if (row["IsDelete"] != null && row["IsDelete"].ToString() != "")
                {
                    if ((row["IsDelete"].ToString() == "1") || (row["IsDelete"].ToString().ToLower() == "true"))
                    {
                        model.IsDelete = true;
                    }
                    else
                    {
                        model.IsDelete = false;
                    }
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
            strSql.Append("select Guid,StudentId,StuName,NetEnteryId,NetEnterName,CreateId,CreateName,CreateTime,LastUpdateTime,IsDelete ");
            strSql.Append(" FROM NetEnterStudent ");
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
            strSql.Append(" Guid,StudentId,StuName,NetEnteryId,NetEnterName,CreateId,CreateName,CreateTime,LastUpdateTime,IsDelete ");
            strSql.Append(" FROM NetEnterStudent ");
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
            strSql.Append("select count(1) FROM NetEnterStudent ");
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
                strSql.Append("order by T.Guid desc");
            }
            strSql.Append(")AS Row, T.*  from NetEnterStudent T ");
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
            parameters[0].Value = "NetEnterStudent";
            parameters[1].Value = "Guid";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        /// <summary>
        /// 取消报名,删除报名记录
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public bool CancelEnterFor(string userid, string netid)
        {
            string sql = "update NetEnterStudent set IsDelete = 1,LastUpdateTime = getdate() where StudentId = '" + userid + "' and NetEnteryId = '" + netid + "' ";
            int rows = DbHelperSQL.ExecuteSql(sql);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        #endregion  ExtensionMethod
    }
}

