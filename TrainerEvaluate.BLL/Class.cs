using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using TrainerEvaluate.Utility;
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.BLL
{
	/// <summary>
	/// 问卷管理
	/// </summary>
	public partial class Class
	{
		private readonly DAL.Class dal=new DAL.Class();
		public Class()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ClassId)
		{
			return dal.Exists(ClassId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public bool Add(Models.Class model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(Models.Class model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ClassId)
		{
			
			return dal.Delete(ClassId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string ClassIdlist )
		{
			return dal.DeleteList(ClassIdlist);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public Models.Class GetModel(int ClassId)
		{
			
			return dal.GetModel(ClassId);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<Models.Class> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<Models.Class> DataTableToList(DataTable dt)
		{
            List<Models.Class> modelList = new List<Models.Class>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
                Models.Class model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
        public DataSet GetListByPage(string strWhere, string sort, int startIndex, int endIndex,string order)
		{
			return dal.GetListByPage( strWhere,sort,  startIndex,  endIndex,order);
		}

	    /// <summary>
	    /// 分页获取数据列表
	    /// </summary>
	    //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
	    //{
	    //return dal.GetList(PageSize,PageIndex,strWhere);
	    //}

	    #endregion  BasicMethod

	    #region  ExtensionMethod



	    public int GetId()
	    { 
	        var result = DbHelperSQL.GetMaxID("ID", "Class");
	        return result;
	    }




	    public DataSet GetDataForExport(string strWhere)
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.Name,ObjectName,Description, convert(varchar, a.StartDate, 102) as StartDate, convert(varchar, a.FinishDate, 102) as FinishDate, Students, Point, b.Name as PointTypeName, Teacher, c.Name as  AreaName,d.Name as   LevelName,  e.Name as TypeName ");
            strSql.Append(" FROM Class a left join Dictionaries b on a.PointType=b.ID left join Dictionaries c on a.Area=c.ID  left join Dictionaries d on a.Level=d.ID  left join Dictionaries e on a.Type=e.ID ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by Name asc, CreatedTime desc ");
            return DbHelperSQL.Query(strSql.ToString());
	    }



	    /// <summary>
	    /// 保存班级所选课程(全选课程)
	    /// </summary>
	    /// <returns></returns>
	    public bool SaveClassAllCourse(string classid)
	    {
	        try
	        {
	            var lsSql = new List<string>();
	            var sql1 = string.Format("delete from  ClassCourse  where  ClassId={0} ", classid);
	            lsSql.Add(sql1);
	            var sql =
	                string.Format(
	                    "insert into ClassCourse select  NEWID(), {0}, CourseId,'00000000-0000-0000-0000-000000000000' from Course  where Status=1 ",
	                    classid);
	            lsSql.Add(sql);
                return DbHelperSQL.ExecuteSqlTran(lsSql) > 0;
	        }
	        catch (Exception ex)
	        {
	            LogHelper.WriteLogofExceptioin(ex);
	            return false;
	        }
	    }


        /// <summary>
        /// 删除指定班级所有课程
        /// </summary>
        /// <param name="classid"></param>
        /// <returns></returns>
        public bool DeletAllCourseofClass(string classid)
	    {
	        try
	        {
                var sql = string.Format("delete from  ClassCourse  where  ClassId={0} ", classid);
                return  DbHelperSQL.ExecuteSql(sql)>=0;  
	        }
	        catch (Exception ex)
	        {
                LogHelper.WriteLogofExceptioin(ex);
	            return false;
	        }
	    }


        /// <summary>
        /// 保存班级所选课程
        /// </summary>
        /// <param name="classid"></param>
        /// <param name="courseid"></param>
        /// <returns></returns>
        public bool SaveChoseClass(string classid,string[] courseid)
	    {
            try
            {
                var lstSql = new List<string>();
                var sql = string.Format("delete from  ClassCourse  where  ClassId={0} ", classid);
                lstSql.Add(sql);
                foreach (var s in courseid)
                {
                    var sql1 = string.Format("insert into ClassCourse (ID,ClassId,CourseID) values(NEWID(),{0},'{1}')",classid,s);
                    lstSql.Add(sql1);
                }
               return DbHelperSQL.ExecuteSqlTran(lstSql)>=0;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return false;
            }
	    }


        public bool SaveUnChoseClass(string classid, string[] courseid)
        {
            try
            {
                var lstSql = new List<string>();
                foreach (var s in courseid)
                {
                    var sql1 = string.Format("delete from ClassCourse where ClassId={0} and  CourseID='{1}')", classid, s);
                    lstSql.Add(sql1);
                }
                return DbHelperSQL.ExecuteSqlTran(lstSql) >= 0;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return false;
            }
        }


        /// <summary>
        /// 根据班级id，查找所属班级，最后确定课程所属年份
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public string GetClassInfoByClassId(string classId)
        {
            try
            {
                string classYear = System.DateTime.Now.Year.ToString();

                var sql = string.Format("select a.Name,a.YearLevel from Class a where a.Status = 1 and a.ID = '{0}'", classId);
                DataSet ds = DbHelperSQL.Query(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    var row = ds.Tables[0].Rows[0];
                  //  classYear = row["YearLevel"].ToString() + row["Name"].ToString();
                    classYear =  row["Name"].ToString();
                }
                return classYear;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return string.Empty;
            }
        }

        public DataSet GetClassInfoByStudentId(string studentId)
        {
            try
            {
                var sql = string.Format("select a.* from Class a left join ClassStudents b on a.ID = b.ClassId "
                    + " where b.StudentId = '{0}' and a.Status = 1 order by a.StartDate desc ", studentId);
                DataSet ds = DbHelperSQL.Query(sql);
                
                return ds;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return new DataSet();
            }
        }


        public DataSet GetClassInfoByStudentId(string studentId, string name, string desp, int startIndex, int endIndex)
        {
            try
            {
                var sql = string.Format("select a.* from Class a left join ClassStudents b on a.ID = b.ClassId "
                   + " where b.StudentId = '{0}' and a.Status = 1 and CloseDate < GETDATE()", studentId);

                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT * FROM ( ");
                strSql.Append(" SELECT ROW_NUMBER() OVER (");
                strSql.Append("order by StartDate desc");
                strSql.Append(")AS Row, T.*  from   ");
                strSql.Append(" ( " + sql + "  ) ");
                strSql.Append(" T ");

                string strWhere = string.Empty;
                if (!string.IsNullOrEmpty(name))
                {
                    strWhere += " and  Name like '%" + name + "%'";
                }
                if (!string.IsNullOrEmpty(desp))
                {
                    strWhere += " and  Description like '%" + desp + "%'";
                }

                if (!string.IsNullOrEmpty(strWhere.Trim()))
                {
                    strSql.Append(" WHERE 1 = 1 " + strWhere);
                }

                strSql.Append(" ) TT");
                strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
                DataSet ds = DbHelperSQL.Query(strSql.ToString());
                return ds;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return new DataSet();
            }
        }

        public int GetRecordCountByStudentId(string studentId, string name, string desp)
        {
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(name)) 
            {
                strWhere += " and  a.Name like '%" + name + "%'";
            }
            if (!string.IsNullOrEmpty(desp))
            {
                strWhere += " and  a.Description like '%" + desp + "%'";
            }

            var sql = string.Format("select count(1) from Class a left join ClassStudents b on a.ID = b.ClassId "
                    + " where b.StudentId = '{0}' {1} and a.Status = 1 and CloseDate < GETDATE() ", studentId, strWhere);

            object obj = DbHelperSQL.GetSingle(sql);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }


        public DataSet GetProfessExperByTeacherId(string teacherId)
        {
            try
            {
                var sql = string.Format(" select b.RId as Id, a.CourseName,a.TeachPlace,a.TeachTime, c.Name as ClassName,c.Object,c.Description, "
                    + " c.Students as StudentCount,c.Point,c.Teacher,c.StartDate,c.FinishDate "
                    + " from Course a "
                    + " left join CourseTeacher b on a.CourseId=b.CourseId "
                    + " left join Class c on c.ID = b.ClassId "
                    + " where b.TeacherId = '{0}' and a.Status = 1 and c.Status = 1 "
                    + " order by a.TeachTime desc ", teacherId);

                DataSet ds = DbHelperSQL.Query(sql);
                
                return ds;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return new DataSet();
            }
        }

        /// <summary>
        /// 获得数据列表，通过班级Id
        /// </summary>
        public DataSet GetDataByClassId(string classId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select ClassId,ClassName,CourseID from CourseTeacher  ");
            strSql.Append(" where ClassId = '" + classId + "' ");
            return DbHelperSQL.Query(strSql.ToString());
        }

                /// <summary>
        /// 获得导出的班级信息，通过班级Id
        /// </summary>
        public DataSet GetExportByClassId(string classId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select a.Name as ClassName,a.Object,a.ObjectName, a.Description,a.StartDate,a.FinishDate,a.Students, ");
            strSql.Append(" a.Point,b.Name as PointTypeName,c.Name as TypeName,d.Name as AreaName from Class a  ");
            strSql.Append(" left join Dictionaries b on a.PointType = b.ID and a.ID = '" + classId + "' ");
            strSql.Append(" left join Dictionaries c on a.Type = c.ID and a.ID = '" + classId + "' ");
            strSql.Append(" left join Dictionaries d on a.Area = d.ID and a.ID = '" + classId + "'  ");
            strSql.Append(" where a.Status = 1 and a.ID = '"+classId+"'  ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 通过班级id，获取项目负责人
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public string GetProjectPersonClassId(string classId)
        {
            string str = string.Empty;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select a.UserId,a.UserName from SysUser a ");
                strSql.Append(" left join ClassTeacher b on a.UserId = b.TeacherId ");
                strSql.Append(" where b.ClassId = '" + classId + "' and a.Status = 1 ");
                DataSet ds = DbHelperSQL.Query(strSql.ToString());
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        str += dr["UserName"].ToString() + ",";
                    }
                    if (str.Length > 0)
                    {
                        str = str.Substring(0, str.Length - 1);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
            }
            return str;
        }


        public DataSet GetNetStudentGoing(string name, int startIndex, int endIndex)
        {
            try
            {
                string strWhere = string.Empty;
                if (!string.IsNullOrEmpty(name))
                {
                    strWhere += " and levelname like '%" + name + "%'";
                }

                var sql = string.Format("select distinct(levelname) as train from Class "
                        + " where Status = 1 and IsReport = 1 and CloseDate>GETDATE() and levelname is not null   {0} ", strWhere);

                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT * FROM ( ");
                strSql.Append(" SELECT ROW_NUMBER() OVER (");
                strSql.Append("order by train asc");
                strSql.Append(")AS Row, T.*  from   ");
                strSql.Append(" ( " + sql + "  ) ");
                strSql.Append(" T ");
                strSql.Append(" ) TT");
                strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
                DataSet ds = DbHelperSQL.Query(strSql.ToString());
                return ds;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return new DataSet();
            }
        }

        public int GetNetStudentGoingCount(string name,string userId)
        {
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(name))
            {
                strWhere += " and levelname like '%" + name + "%'";
            }

            var sql = string.Format("select COUNT(1) from( select distinct(levelname) as train from Class "
                    + " where  Status = 1 and  IsReport = 1 and CloseDate>GETDATE() and levelname is not null   {0} ) A ", strWhere, userId);

            object obj = DbHelperSQL.GetSingle(sql);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public DataSet GetNetStudentClass(string name, int startIndex, int endIndex, string userId)
        {
            try
            {
                string strWhere = string.Empty;
                if (!string.IsNullOrEmpty(name))
                {
                    strWhere += " and levelname like '%" + name + "%'";
                }

                var sql = string.Format("select *,(ReportMax-HasReportNum) LeftNum from Class "
                        + " where Status = 1 and IsReport = 1 and  ID not in ( select ClassId  from ClassStudents where   StudentId='{1}')  and   (ReportMax-HasReportNum)>0 and CloseDate > GETDATE() {0} ", strWhere, userId);

                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT * FROM ( ");
                strSql.Append(" SELECT ROW_NUMBER() OVER (");
                strSql.Append("order by Name asc");
                strSql.Append(")AS Row, T.*  from   ");
                strSql.Append(" ( " + sql + "  ) ");
                strSql.Append(" T ");
                strSql.Append(" ) TT");
                strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
                DataSet ds = DbHelperSQL.Query(strSql.ToString());
                return ds;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return new DataSet();
            }
        }

        public int GetNetStudentClassCount(string name, string userId)
        {
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(name))
            {
                strWhere += " and levelname like '%" + name + "%'";
            }

            var sql = string.Format("select count(1) from Class "
                    + " where Status = 1 and IsReport = 1 and  ID not in ( select ClassId  from ClassStudents where   StudentId='{1}') and (ReportMax-HasReportNum)>0  and CloseDate > GETDATE()  {0} ", strWhere, userId);

            object obj = DbHelperSQL.GetSingle(sql);
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
        /// 保存学生报名信息
        /// </summary>
        /// <param name="classid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
	    public bool SaveStudentSignupInfo(string classid, string userid)
        {
            

            var ls = new List<string>();
	        var sql = string.Format("update Class set HasReportNum=HasReportNum+1 where  ID='{0}'  ", classid);
	        var sql1 = string.Format(" insert into  ClassStudents values(NEWID(),'{0}','{1}') ", userid, classid);
	        ls.Add(sql);
	        ls.Add(sql1);
	        var result = DbHelperSQL.ExecuteSqlTran(ls);

	        return result == 2;
	    }

	    public bool IfStudentSignup(string classid, string userid)
	    {

	        var sqlstr = string.Format(" select COUNT(*) from ClassStudents where StudentId='{0}' and ClassId='{1}' ",
	            userid, classid);
	        var re = DbHelperSQL.GetSingle(sqlstr);

	        if (Convert.ToInt32(re) > 0)
	        {
	            return false;
	        }
	        else
	        {
	            return true;
	        }
	    }


        /// <summary>
        /// 提取班级课件
        /// </summary>
        /// <param name="classid"></param>
        /// <returns></returns>
        public DataSet GetClasspptByClassId(string classid)
        {
            try
            {
                var sql = string.Format(" select * from ClassAttach where IsValid=1 and ClassId= {0} ", classid);

                DataSet ds = DbHelperSQL.Query(sql);

                return ds;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return new DataSet();
            }
        }

	    #endregion  ExtensionMethod
	}
}

