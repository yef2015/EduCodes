using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

//Please add references
using TrainerEvaluate.Utility.DB;


namespace TrainerEvaluate.DAL
{
    /// <summary>
    /// 数据访问类:StuAttBaseInfo
    /// </summary>
    public partial class StuAttBaseInfo
    {
        public StuAttBaseInfo()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(Guid Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from StuAttBaseInfo");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Models.StuAttBaseInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into StuAttBaseInfo(");
            strSql.Append("Id,StudentId,ClassId,CourseID,TheDay,CreateTime,CreateId,CreateName,Status,Remark,IsValid,LastModifyTime,LastModifyId)");
            strSql.Append(" values (");
            strSql.Append("@Id,@StudentId,@ClassId,@CourseID,@TheDay,@CreateTime,@CreateId,@CreateName,@Status,@Remark,@IsValid,@LastModifyTime,@LastModifyId)");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@StudentId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ClassId", SqlDbType.Int,4),
					new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@TheDay", SqlDbType.DateTime),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CreateName", SqlDbType.NVarChar,50),
					new SqlParameter("@Status", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@IsValid", SqlDbType.Bit,1),
					new SqlParameter("@LastModifyTime", SqlDbType.DateTime),
					new SqlParameter("@LastModifyId", SqlDbType.UniqueIdentifier,16)};
            parameters[0].Value = Guid.NewGuid();
            parameters[1].Value = Guid.NewGuid();
            parameters[2].Value = model.ClassId;
            parameters[3].Value = Guid.NewGuid();
            parameters[4].Value = model.TheDay;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = Guid.NewGuid();
            parameters[7].Value = model.CreateName;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.Remark;
            parameters[10].Value = model.IsValid;
            parameters[11].Value = model.LastModifyTime;
            parameters[12].Value = Guid.NewGuid();

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
        public bool Update(Models.StuAttBaseInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update StuAttBaseInfo set ");
            strSql.Append("StudentId=@StudentId,");
            strSql.Append("ClassId=@ClassId,");
            strSql.Append("CourseID=@CourseID,");
            strSql.Append("TheDay=@TheDay,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("CreateId=@CreateId,");
            strSql.Append("CreateName=@CreateName,");
            strSql.Append("Status=@Status,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("IsValid=@IsValid,");
            strSql.Append("LastModifyTime=@LastModifyTime,");
            strSql.Append("LastModifyId=@LastModifyId");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@StudentId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ClassId", SqlDbType.Int,4),
					new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@TheDay", SqlDbType.DateTime),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CreateName", SqlDbType.NVarChar,50),
					new SqlParameter("@Status", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@IsValid", SqlDbType.Bit,1),
					new SqlParameter("@LastModifyTime", SqlDbType.DateTime),
					new SqlParameter("@LastModifyId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16)};
            parameters[0].Value = model.StudentId;
            parameters[1].Value = model.ClassId;
            parameters[2].Value = model.CourseID;
            parameters[3].Value = model.TheDay;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.CreateId;
            parameters[6].Value = model.CreateName;
            parameters[7].Value = model.Status;
            parameters[8].Value = model.Remark;
            parameters[9].Value = model.IsValid;
            parameters[10].Value = model.LastModifyTime;
            parameters[11].Value = model.LastModifyId;
            parameters[12].Value = model.Id;

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
            strSql.Append("delete from StuAttBaseInfo ");
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
            strSql.Append("delete from StuAttBaseInfo ");
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
        public Models.StuAttBaseInfo GetModel(Guid Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,StudentId,ClassId,CourseID,TheDay,CreateTime,CreateId,CreateName,Status,Remark,IsValid,LastModifyTime,LastModifyId from StuAttBaseInfo ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = Id;

            Models.StuAttBaseInfo model = new Models.StuAttBaseInfo();
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
        public Models.StuAttBaseInfo DataRowToModel(DataRow row)
        {
            Models.StuAttBaseInfo model = new Models.StuAttBaseInfo();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = new Guid(row["Id"].ToString());
                }
                if (row["StudentId"] != null && row["StudentId"].ToString() != "")
                {
                    model.StudentId = new Guid(row["StudentId"].ToString());
                }
                if (row["ClassId"] != null && row["ClassId"].ToString() != "")
                {
                    model.ClassId = int.Parse(row["ClassId"].ToString());
                }
                if (row["CourseID"] != null && row["CourseID"].ToString() != "")
                {
                    model.CourseID = new Guid(row["CourseID"].ToString());
                }
                if (row["TheDay"] != null && row["TheDay"].ToString() != "")
                {
                    model.TheDay = DateTime.Parse(row["TheDay"].ToString());
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
                if (row["Status"] != null)
                {
                    model.Status = row["Status"].ToString();
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
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
                if (row["LastModifyTime"] != null && row["LastModifyTime"].ToString() != "")
                {
                    model.LastModifyTime = DateTime.Parse(row["LastModifyTime"].ToString());
                }
                if (row["LastModifyId"] != null && row["LastModifyId"].ToString() != "")
                {
                    model.LastModifyId = new Guid(row["LastModifyId"].ToString());
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
            strSql.Append("select Id,StudentId,ClassId,CourseID,TheDay,CreateTime,CreateId,CreateName,Status,Remark,IsValid,LastModifyTime,LastModifyId ");
            strSql.Append(" FROM StuAttBaseInfo ");
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
            strSql.Append(" Id,StudentId,ClassId,CourseID,TheDay,CreateTime,CreateId,CreateName,Status,Remark,IsValid,LastModifyTime,LastModifyId ");
            strSql.Append(" FROM StuAttBaseInfo ");
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
            strSql.Append("select count(1) FROM StuAttBaseInfo ");
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
            strSql.Append(")AS Row, T.*  from StuAttBaseInfo T ");
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
            parameters[0].Value = "StuAttBaseInfo";
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
