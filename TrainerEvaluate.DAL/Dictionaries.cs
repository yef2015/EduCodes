using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TrainerEvaluate.Utility.DB;


//Please add references
namespace TrainerEvaluate.DAL
{
	/// <summary>
	/// 数据访问类:Dictionaries
	/// </summary>
	public partial class Dictionaries
	{
        public Dictionaries()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "Dictionaries");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Dictionaries");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Models.Dictionaries model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Dictionaries(");
            strSql.Append("ID,Name,DType,Dstatus,CreateTime,CreateId,CreateName,LastModifyTime,LastModifyId,LastModifyName)");
            strSql.Append(" values (");
            strSql.Append("@ID,@Name,@DType,@Dstatus,@CreateTime,@CreateId,@CreateName,@LastModifyTime,@LastModifyId,@LastModifyName)");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@DType", SqlDbType.NVarChar,50),
					new SqlParameter("@Dstatus", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CreateName", SqlDbType.NVarChar,50),
					new SqlParameter("@LastModifyTime", SqlDbType.DateTime),
					new SqlParameter("@LastModifyId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@LastModifyName", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.DType;
            parameters[3].Value = model.Dstatus;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = Guid.NewGuid();
            parameters[6].Value = model.CreateName;
            parameters[7].Value = model.LastModifyTime;
            parameters[8].Value = Guid.NewGuid();
            parameters[9].Value = model.LastModifyName;

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
        public bool Update(Models.Dictionaries model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Dictionaries set ");
            strSql.Append("Name=@Name,");
            strSql.Append("DType=@DType,");
            strSql.Append("Dstatus=@Dstatus,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("CreateId=@CreateId,");
            strSql.Append("CreateName=@CreateName,");
            strSql.Append("LastModifyTime=@LastModifyTime,");
            strSql.Append("LastModifyId=@LastModifyId,");
            strSql.Append("LastModifyName=@LastModifyName");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@DType", SqlDbType.NVarChar,50),
					new SqlParameter("@Dstatus", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CreateName", SqlDbType.NVarChar,50),
					new SqlParameter("@LastModifyTime", SqlDbType.DateTime),
					new SqlParameter("@LastModifyId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@LastModifyName", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.DType;
            parameters[2].Value = model.Dstatus;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.CreateId;
            parameters[5].Value = model.CreateName;
            parameters[6].Value = model.LastModifyTime;
            parameters[7].Value = model.LastModifyId;
            parameters[8].Value = model.LastModifyName;
            parameters[9].Value = model.ID;

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
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Dictionaries ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = ID;

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
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Dictionaries ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
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
        public Models.Dictionaries GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Name,DType,Dstatus,CreateTime,CreateId,CreateName,LastModifyTime,LastModifyId,LastModifyName from Dictionaries ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = ID;

            Models.Dictionaries model = new Models.Dictionaries();
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
        public Models.Dictionaries DataRowToModel(DataRow row)
        {
            Models.Dictionaries model = new Models.Dictionaries();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["DType"] != null)
                {
                    model.DType = row["DType"].ToString();
                }
                if (row["Dstatus"] != null && row["Dstatus"].ToString() != "")
                {
                    model.Dstatus = int.Parse(row["Dstatus"].ToString());
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }
                if (row["CreateId"] != null && row["CreateId"].ToString() != "")
                {
                    model.CreateId = new Guid(row["CreateId"].ToString());
                }
                if (row["CreateName"] != null)
                {
                    model.CreateName = row["CreateName"].ToString();
                }
                if (row["LastModifyTime"] != null && row["LastModifyTime"].ToString() != "")
                {
                    model.LastModifyTime = DateTime.Parse(row["LastModifyTime"].ToString());
                }
                if (row["LastModifyId"] != null && row["LastModifyId"].ToString() != "")
                {
                    model.LastModifyId = new Guid(row["LastModifyId"].ToString());
                }
                if (row["LastModifyName"] != null)
                {
                    model.LastModifyName = row["LastModifyName"].ToString();
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
            strSql.Append("select ID,Name,DType,Dstatus,CreateTime,CreateId,CreateName,LastModifyTime,LastModifyId,LastModifyName ");
            strSql.Append(" FROM Dictionaries ");
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
            strSql.Append(" ID,Name,DType,Dstatus,CreateTime,CreateId,CreateName,LastModifyTime,LastModifyId,LastModifyName ");
            strSql.Append(" FROM Dictionaries ");
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
            strSql.Append("select count(1) FROM Dictionaries ");
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
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex,string order )
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER ( ");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append(" order by T." + orderby +"  "+ order);
            }
            else
            {
                strSql.Append(" order by T.ID desc");
            }
            strSql.Append(" )AS Row, T.*  from Dictionaries T ");
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
            parameters[0].Value = "Dictionaries";
            parameters[1].Value = "ID";
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

