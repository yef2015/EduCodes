using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

//Please add references
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.DAL
{
    /// <summary>
    /// 数据访问类:CourseTeacher
    /// </summary>
    public partial class CourseTeacher
    {
        public CourseTeacher()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(Guid RId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CourseTeacher");
            strSql.Append(" where RId=@RId ");
            SqlParameter[] parameters = {
					new SqlParameter("@RId", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = RId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Models.CourseTeacher model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CourseTeacher(");
            strSql.Append("RId,CourseId,CoursName,TeacherId,TeacherName,ClassId,ClassName,StartDate,FinishDate,CreateTime,teacherplace)");
            strSql.Append(" values (");
            strSql.Append("@RId,@CourseId,@CoursName,@TeacherId,@TeacherName,@ClassId,@ClassName,@StartDate,@FinishDate,@CreateTime,@teacherplace)");
            SqlParameter[] parameters = {
					new SqlParameter("@RId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CourseId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CoursName", SqlDbType.NVarChar,100),
					new SqlParameter("@TeacherId", SqlDbType.NVarChar,200),
					new SqlParameter("@TeacherName", SqlDbType.NVarChar,200),
					new SqlParameter("@ClassId", SqlDbType.NVarChar,100),
					new SqlParameter("@ClassName", SqlDbType.NVarChar,200),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@FinishDate", SqlDbType.DateTime),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@teacherplace", SqlDbType.NVarChar),
                                        };
            parameters[0].Value = Guid.NewGuid();
            parameters[1].Value = model.CourseId;
            parameters[2].Value = model.CoursName;
            parameters[3].Value = model.TeacherId;
            parameters[4].Value = model.TeacherName;
            parameters[5].Value = model.ClassId;
            parameters[6].Value = model.ClassName;
            parameters[7].Value = model.StartDate;
            parameters[8].Value = model.FinishDate;
            parameters[9].Value = model.CreateTime;
            parameters[10].Value = model.teacherplace;

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
        public bool Delete(Guid RId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CourseTeacher ");
            strSql.Append(" where RId=@RId ");
            SqlParameter[] parameters = {
					new SqlParameter("@RId", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = RId;

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
        public bool DeleteByClassId(string classId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CourseTeacher ");
            strSql.Append(" where ClassId=@ClassId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ClassId", SqlDbType.NVarChar,100)			};
            parameters[0].Value = classId;

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
        public bool DeleteList(string RIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CourseTeacher ");
            strSql.Append(" where RId in (" + RIdlist + ")  ");
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
        public Models.CourseTeacher GetModel(Guid RId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 RId,CourseId,CoursName,TeacherId,TeacherName,ClassId,ClassName,StartDate,FinishDate,CreateTime,teacherplace from CourseTeacher ");
            strSql.Append(" where RId=@RId ");
            SqlParameter[] parameters = {
					new SqlParameter("@RId", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = RId;

            Models.CourseTeacher model = new Models.CourseTeacher();
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
        public Models.CourseTeacher DataRowToModel(DataRow row)
        {
            Models.CourseTeacher model = new Models.CourseTeacher();
            if (row != null)
            {
                if (row["RId"] != null && row["RId"].ToString() != "")
                {
                    model.RId = new Guid(row["RId"].ToString());
                }
                if (row["CourseId"] != null && row["CourseId"].ToString() != "")
                {
                    model.CourseId = new Guid(row["CourseId"].ToString());
                }
                if (row["CoursName"] != null)
                {
                    model.CoursName = row["CoursName"].ToString();
                }
                if (row["TeacherId"] != null)
                {
                    model.TeacherId = row["TeacherId"].ToString();
                }
                if (row["TeacherName"] != null)
                {
                    model.TeacherName = row["TeacherName"].ToString();
                }
                if (row["teacherplace"] != null)
                {
                    model.teacherplace = row["teacherplace"].ToString();
                }
                if (row["ClassId"] != null)
                {
                    model.ClassId = row["ClassId"].ToString();
                }
                if (row["ClassName"] != null)
                {
                    model.ClassName = row["ClassName"].ToString();
                }
                if (row["StartDate"] != null && row["StartDate"].ToString() != "")
                {
                    model.StartDate = DateTime.Parse(row["StartDate"].ToString());
                }
                if (row["FinishDate"] != null && row["FinishDate"].ToString() != "")
                {
                    model.FinishDate = DateTime.Parse(row["FinishDate"].ToString());
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
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
            strSql.Append("select RId,CourseId,CoursName,TeacherId,TeacherName,ClassId,ClassName,StartDate,FinishDate,CreateTime,teacherplace ");
            strSql.Append(" FROM CourseTeacher ");
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
            strSql.Append(" RId,CourseId,CoursName,TeacherId,TeacherName,ClassId,ClassName,StartDate,FinishDate,CreateTime,teacherplace ");
            strSql.Append(" FROM CourseTeacher ");
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
            strSql.Append("select count(1) FROM CourseTeacher ");
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
                strSql.Append("order by T.RId desc");
            }
            strSql.Append(")AS Row, T.*  from CourseTeacher T ");
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
            parameters[0].Value = "CourseTeacher";
            parameters[1].Value = "RId";
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

