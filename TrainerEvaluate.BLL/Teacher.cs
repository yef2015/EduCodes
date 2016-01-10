using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NPOI.HSSF.Record.Chart;
using TrainerEvaluate.Utility;
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.BLL
{
	/// <summary>
	/// 教师信息表
	/// </summary>
	public partial class Teacher
	{
		private readonly DAL.Teacher dal=new DAL.Teacher();
		public Teacher()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid TeacherId)
		{
			return dal.Exists(TeacherId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public bool Add(Models.Teacher model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(Models.Teacher model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(Guid TeacherId)
		{
			
			return dal.Delete(TeacherId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string TeacherIdlist )
		{
			return dal.DeleteList(TeacherIdlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public Models.Teacher GetModel(Guid TeacherId)
		{
			
			return dal.GetModel(TeacherId);
		}

        ///// <summary>
        ///// 得到一个对象实体，从缓存中
        ///// </summary>
        //public Models.Teacher GetModelByCache(Guid TeacherId)
        //{
			
        //    string CacheKey = "TeacherModel-" + TeacherId;
        //    object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
        //    if (objModel == null)
        //    {
        //        try
        //        {
        //            objModel = dal.GetModel(TeacherId);
        //            if (objModel != null)
        //            {
        //                int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
        //                Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
        //            }
        //        }
        //        catch{}
        //    }
        //    return (TrainerEvaluate.Model.TrainerEvaluate.Teacher)objModel;
        //}

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
        public List<Models.Teacher> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<Models.Teacher> DataTableToList(DataTable dt)
		{
            List<Models.Teacher> modelList = new List<Models.Teacher>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
                Models.Teacher model;
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
			return dal.GetListByPage( strWhere,  sort,  startIndex,  endIndex,order);
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


       
        //取关系数据
        public DataSet GetCourseTeacherStateListByPage(string courseId, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby + " desc, " + "T.TeacherName  asc ");
            }
            else
            {
                strSql.Append("order by T.TeacherName asc");
            }
            strSql.Append(")AS Row, T.*  from   ");
            strSql.Append(" ( select a.TeacherId,a.TeacherName, a.Dept, case ISNULL(b.RId,'00000000-0000-0000-0000-000000000000')  when '00000000-0000-0000-0000-000000000000' then  0  else 1 end ck from Teacher a    ");
            strSql.Append(string.Format("  left join  CourseTeacher b on a.TeacherId=b.TeacherId and b.CourseId='{0}'  )  ", courseId));
            strSql.Append("  T "); 
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
     
        
        public DataSet GetClassTeacherListByPage(string classId, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby + " desc, " + "T.TeacherName  asc ");
            }
            else
            {
                strSql.Append("order by T.TeacherName asc");
            }
            strSql.Append(")AS Row, T.*  from   ");
            strSql.Append(" ( select a.TeacherId,a.TeacherName, a.Dept, case ISNULL(b.RId,'00000000-0000-0000-0000-000000000000')  when '00000000-0000-0000-0000-000000000000' then  0  else 1 end ck from Teacher a    ");
            strSql.Append(string.Format("  left join  ClassTeacher b on a.TeacherId=b.TeacherId and b.ClassId={0}  )  ", classId));
            strSql.Append("  T "); 
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }



        public bool SaveChooseForAll(string courseId)
	    {
            try
            {
                if (!string.IsNullOrEmpty(courseId))
                {
                    var lstSql = new List<string>();
                    lstSql.Add(string.Format(" delete from  CourseTeacher  where CourseId='{0}' ", courseId));
                    var sql1 = string.Format("  insert into CourseTeacher select NEWID(),'{0}',  TeacherId,getdate() from Teacher ", courseId );
                    lstSql.Add(sql1);  
                    DbHelperSQL.ExecuteSqlTran(lstSql);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return false;
            }
	    }



        public bool SaveChooseForAllofClass(string classId)
        {
            try
            {
                //if (!string.IsNullOrEmpty(classId))
                //{
                //    var lstSql = new List<string>(); 
                //    var sql1 = string.Format("  insert into CourseTeacher select NEWID(),'{0}',  TeacherId,getdate() from Teacher ", courseId);
                //    lstSql.Add(sql1);
                //    DbHelperSQL.ExecuteSqlTran(lstSql);
                //}
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return false;
            }
        }



        public bool DeletTeacherofClass(string classId)
        {
            try
            {
                var strs = new List<string>();
                var sql = string.Format("  update Class set Teacher='' where ID='{0}' ", classId);
                strs.Add(sql);
                DbHelperSQL.ExecuteSqlTran(strs);
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return false;
            }
        }



        public bool DeletCourseTeacherbyCourseId(string coursiId)
        {
            try
            {
                var strs = new List<string>();
                var sql = string.Format(" delete from  CourseTeacher  where CourseId='{0}' ", coursiId);  
                strs.Add(sql);  
                DbHelperSQL.ExecuteSqlTran(strs); 
                return true; 
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return false;
            }
        }



        public bool SaveChooseTeacher(string[] teaIds, string courseId)
        {
            try
            {
                if (!string.IsNullOrEmpty(courseId))
                {
                    if (teaIds != null && teaIds.Length > 0)
                    {
                        var lstSql = new List<string>();
                        lstSql.Add(string.Format(" delete from  CourseTeacher  where CourseId='{0}' ", courseId));
                        foreach (var teaId in teaIds)
                        {
                            if (!string.IsNullOrEmpty(teaId))
                            {
                                var sql1 =
                                    string.Format(
                                        "insert into CourseTeacher values(NEWID(),'{0}','{1}',Getdate())  ", courseId,
                                        teaId);  
                                lstSql.Add(sql1); 
                            }
                        } 
                        var teacherNames = GetCourserTeacherNames(teaIds, courseId);
                        var sql2 = string.Format("update Course set TeacherName='{0}' where CourseId='{1}'  ", teacherNames, courseId);
                        lstSql.Add(sql2);
                        DbHelperSQL.ExecuteSqlTran(lstSql);
                    }
                    else
                    {
                        var lstSql = new List<string>();
                        lstSql.Add(string.Format(" delete from  CourseTeacher  where CourseId='{0}' ", courseId));  
                        DbHelperSQL.ExecuteSqlTran(lstSql);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return false;
            }
        }


        private string GetCourserTeacherNames(string[] teaIds, string courseId)
	    {
            var sb = new StringBuilder();
            try
            { 
              
                var sb1 = new StringBuilder();
                foreach (var teaId in teaIds)
                {
                    sb1.Append("'" + teaId+"',"); 
                }
                if (sb1.Length > 0)
                {
                    sb1 = sb1.Remove(sb1.Length - 1, 1);
                }
                var teachers = sb1.ToString();
                var sql = string.Format(" select TeacherName from Teacher where  TeacherId in({0}) ", teachers);
                var dt = DbHelperSQL.Query(sql);

                if (dt != null && dt.Tables.Count > 0 )
                {
                    foreach (DataRow row in dt.Tables[0].Rows)
                    {
                        sb.Append(row["TeacherName"]+","); 
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                } 
                return sb.ToString(); 
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return "";
            }
	    }




	    public bool SaveChooseTeacherofClass(string[] teaids, string classId)
        {
            try
            {
                if (!string.IsNullOrEmpty(classId))
                {
                    if (teaids != null && teaids.Length>0)
                    {
                        var lstSql = new List<string>(); 
                        var sb = new StringBuilder();
                        lstSql.Add(string.Format(" delete from  ClassTeacher  where ClassId={0} ", classId));
                        foreach (var teaId in teaids)
                        {
                            if (!string.IsNullOrEmpty(teaId))
                            {
                                sb.Append("'" + teaId + "',");
                                var sql1 =
                                    string.Format(
                                        "insert into ClassTeacher values(NEWID(),{0},'{1}')  ", classId,
                                        teaId);
                                lstSql.Add(sql1);
                            }
                        }   
                        sb= sb.Remove(sb.Length-1,1);
                        var sql2 = string.Format(" select TeacherName from Teacher  where TeacherId in  ({0})  ",sb);
                        var ds = DbHelperSQL.Query(sql2);
                        if (ds != null && ds.Tables.Count > 0&& ds.Tables[0].Rows.Count>0)
                        {
                            var sb1 = new StringBuilder();
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                sb1.Append(row["TeacherName"]+",");
                            } 
                            sb1 = sb1.Remove(sb1.Length - 1, 1);
                            var sq3 = string.Format(" update Class  set  Teacher='{1}' where ID={0} ", classId, sb1);
                            lstSql.Add(sq3);
                        }  
                        DbHelperSQL.ExecuteSqlTran(lstSql);
                    }
                    else
                    {
                        var lstSql = new List<string>();
                        lstSql.Add(string.Format(" update Class set Teacher='' where ID={0}  ", classId));
                        DbHelperSQL.ExecuteSqlTran(lstSql);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return false;
            }
        }


	    public DataSet GetDataForExport(string strWhere)
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select TeacherName,case Gender when 1 then '男' else '女' end as GenderName,IdentityNo,Dept,Title,Post,ResearchBigName+'  '+Research ,Mobile,Description ");
            strSql.Append(" FROM Teacher ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by TeacherName asc, CreateTime desc ");
            return DbHelperSQL.Query(strSql.ToString());
	    }


        public string GetTeacherAccount()
        {
            try
            {
                var sql = string.Format(" select COUNT(*) num from  SysUser where UserRole={0} and YEAR(CreateTime)=YEAR(GETDATE())", (int)EnumUserRole.Teacher);
                var result = DbHelperSQL.Query(sql);
                var num = 0;
                if (result != null && result.Tables.Count > 0)
                {
                    num = Convert.ToInt32(result.Tables[0].Rows[0]["num"]) + 1;
                }

                var userAccount = "HB" + DateTime.Now.Year + num.ToString().PadLeft(3, '0');


                return userAccount;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return "";
            }
        }

        public string GetPwd()
        {
            var pwd = new Random();
            return pwd.Next(999999).ToString();
        }

        /// <summary>
        /// 获取授课教师信息，为填充下拉框
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataSourceOnTeacher()
        {
            var dt = new DataTable();
            try
            {
                var sql = " select TeacherId as ID,TeacherName as Name from Teacher where Status = 1 order by Name   ";
                var result = DbHelperSQL.Query(sql);
                if (result != null && result.Tables.Count > 0)
                {
                    dt = result.Tables[0];
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
            }
            return dt;
        }

		#endregion  ExtensionMethod
	}
}

