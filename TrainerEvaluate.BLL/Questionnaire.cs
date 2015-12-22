﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using NPOI.HSSF.Record.Chart;
using TrainerEvaluate.Utility;
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.BLL
{
	/// <summary>
	/// 问卷管理
	/// </summary>
	public partial class Questionnaire
	{
		private readonly TrainerEvaluate.DAL.Questionnaire dal=new TrainerEvaluate.DAL.Questionnaire();
		public Questionnaire()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid QuestionnaireId)
		{
			return dal.Exists(QuestionnaireId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public bool Add(Models.Questionnaire model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(Models.Questionnaire model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(Guid QuestionnaireId)
		{
			
			return dal.Delete(QuestionnaireId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string QuestionnaireIdlist )
		{
			return dal.DeleteList(QuestionnaireIdlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public Models.Questionnaire GetModel(Guid QuestionnaireId)
		{
			
			return dal.GetModel(QuestionnaireId);
		}

        ///// <summary>
        ///// 得到一个对象实体，从缓存中
        ///// </summary>
        //public Models.Questionnaire GetModelByCache(Guid QuestionnaireId)
        //{
			
        //    string CacheKey = "QuestionnaireModel-" + QuestionnaireId;
        //    object objModel =  Common.DataCache.GetCache(CacheKey);
        //    if (objModel == null)
        //    {
        //        try
        //        {
        //            objModel = dal.GetModel(QuestionnaireId);
        //            if (objModel != null)
        //            {
        //                int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
        //                Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
        //            }
        //        }
        //        catch{}
        //    }
        //    return (TrainerEvaluate.Model.TrainerEvaluate.Questionnaire)objModel;
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
        public List<Models.Questionnaire> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<Models.Questionnaire> DataTableToList(DataTable dt)
		{
            List<Models.Questionnaire> modelList = new List<Models.Questionnaire>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
                Models.Questionnaire model;
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
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
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

        /// <summary>
        /// 获取总平均分
        /// </summary>
        /// <param name="courseName"></param>
        /// <param name="teacher"></param>
        /// <param name="teahTime"></param>
        /// <param name="tPlace"></param>
        /// <returns></returns>
	    public  int GetTotalAvg(string courseName,string teacher,string teahTime,string tPlace)
	    {
            return dal.GetTotalAvg(courseName,teacher,teahTime,tPlace);

	    }


        public int GetTotalAvg(string cid)
        {
            return dal.GetTotalAvg(cid);

        }



        /// <summary>
        /// 获取满意度百分比
        /// </summary>
        /// <param name="coursId"></param>
        /// <returns></returns>
	    public decimal[] GetSatisfyPercent(Guid coursId)
        {
            return dal.GetSatisfyPercent(coursId);
        }




        /// <summary>
        /// 提取总平均分2014--2018趋势
        /// </summary>
        /// <param name="coursId"></param>
        /// <returns></returns>
        public double[] GetTendency(Guid coursId)
        {
            try
            {
                var result = new double[5];
                var sql1 = string.Format("exec GetTendency '{0}'", coursId);
                var ds = DbHelperSQL.Query(sql1);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var row = ds.Tables[0].Rows[0];
                    result[0] = row["2014"] == DBNull.Value ? 0 : Convert.ToDouble(row["2014"]);
                    result[1] = row["2015"] == DBNull.Value ? 0 : Convert.ToDouble(row["2015"]);
                    result[2] = row["2016"] == DBNull.Value ? 0 : Convert.ToDouble(row["2016"]);
                    result[3] = row["2017"] == DBNull.Value ? 0 : Convert.ToDouble(row["2017"]);
                    result[4] = row["2018"] == DBNull.Value ? 0 : Convert.ToDouble(row["2018"]);
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return null;
            }
        }


        /// <summary>
        /// 取满意度柱状图的课程
        /// </summary>
        /// <returns></returns>
	    public DataTable GetSatisfybar(string level)
	    {
            try
            { 
                var beginValue = 0;
                var endValue = 0;
                switch (level)
                {
                    case "1":
                        beginValue = 95;
                        endValue = 100;
                        break;
                    case "2":
                        beginValue = 90;
                        endValue = 95;
                        break;
                    case "3":
                        beginValue = 80;
                        endValue = 90;
                        break;
                    case "4":
                        beginValue = 0;
                        endValue = 80;
                        break;
                    default:
                        break;
                }

                var sql = string.Format("exec GetSatisfybar {0},{1}", beginValue, endValue); 
                return DbHelperSQL.Query(sql).Tables[0]; 
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return null;
            }
	    }




	    public List<string> GetTheSix(Guid couGuid, bool isTop)
	    {
	        var ss = new string[6];
	        if (isTop)
	        {
	            ss = dal.GetTopSix(couGuid);
	        }
	        else
	        {
	            ss = dal.GetBottomSix(couGuid);
	        }

	        var result = new List<string>();
	        for (int i = 0; i < 6; i++)
	        {
	            switch (ss[i])
	            {
	                case "CourseSubject":
	                    result.Add("课程主题清晰明确");
	                    break;
	                case "CourseRich":
	                    result.Add("课程内容丰富、能吸引人");
	                    break;
	                case "CoursePractical":
	                    result.Add("课程内容切合实际，能指导实践");
	                    break;
	                case "CourseKey":
	                    result.Add("课程内容重点突出，易于理解");
	                    break;
	                case "CourseDevelop":
	                    result.Add("课程内容有助于个人发展");
	                    break;
	                case "TeacherPrepare":
	                    result.Add("讲师准备比较充分");
	                    break;
	                case "TeacherLanguage":
	                    result.Add("语言表达清晰，态度端正");
	                    break;
	                case "TeacherBearing":
	                    result.Add("仪表仪容端庄大方，有亲和力");
	                    break;
	                case "TeacherStyle":
	                    result.Add("培训方式多样，生动有趣");
	                    break;
	                case "TeacherCommunication":
	                    result.Add("与学员沟通和互动有效");
	                    break;
	                case "OrgService":
	                    result.Add("培训服务周到细致");
	                    break;
	                case "OrgTime":
	                    result.Add("培训时间安排和控制合理");
	                    break;
	                case "OrgArrange":
	                    result.Add("培训场所、设备安排到位");
	                    break;
	                default:
	                    break;

	            }
	        }
	        return result;
	    }



        /// <summary>
        /// 保存问卷
        /// </summary>
        /// <param name="stuIds"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
	    public bool SaveQuestions(string[] stuIds,string courseId)
	    {
	        try
	        {
	            if (!string.IsNullOrEmpty(courseId))
	            {
	                if (stuIds != null && stuIds.Length > 0)
	                {
	                    var lstSql = new List<string>();
	                    lstSql.Add(string.Format(" delete from  CourseStudents  where CourseId='{0}' ", courseId));
	                    foreach (var stuId in stuIds)
	                    {
	                        if (!string.IsNullOrEmpty(stuId))
	                        {
	                            var sql1 =
	                                string.Format(
	                                    "insert into CourseStudents values(NEWID(),'{0}','{1}',{2},Getdate())  ", courseId,
	                                    stuId, (int) EnumQuestionState.Start);
	                            lstSql.Add(sql1);
	                        }
	                    }
	                    var sql = string.Format("update Course set  IsQuestionCreate='已生成'  where CourseId='{0}' ",
	                        courseId);
	                    lstSql.Add(sql);
	                    DbHelperSQL.ExecuteSqlTran(lstSql);
	                }
	                else
	                {
	                    var lstSql = new List<string>();
	                    lstSql.Add(string.Format(" delete from  CourseStudents  where CourseId='{0}' ", courseId));
	                    var sql = string.Format(" update Course set  IsQuestionCreate=''  where CourseId='{0}' ",
	                        courseId);
	                    lstSql.Add(sql);
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





        public bool SaveQuestionsForAll(string courseId)
        {
            try
            {
                if (!string.IsNullOrEmpty(courseId))
                {
                    var lstSql = new List<string>();
                    lstSql.Add(string.Format(" delete from  CourseStudents  where CourseId='{0}' ", courseId));
                    var sql1 = string.Format("  insert into CourseStudents select NEWID(),'{0}',  StudentId,{1},getdate() from Student ", courseId,
                         (int) EnumQuestionState.Start);
                    lstSql.Add(sql1);
                    var sql = string.Format("update Course set  IsQuestionCreate='已生成'  where CourseId='{0}' ", courseId);
                    lstSql.Add(sql);
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


        public bool EditQuestions(string[] stuIds, string courseId)
	    {
            try
            {
                if (!string.IsNullOrEmpty(courseId))
                {
                    if (stuIds != null && stuIds.Length > 0)
                    {
                        var lstSql = new List<string>();

                        lstSql.Add(string.Format(" delete from  CourseStudents  where CourseId='{0}' ", courseId));
                        foreach (var stuId in stuIds)
                        {
                            if (!string.IsNullOrEmpty(stuId))
                            {
                                var sql1 = string.Format("insert into CourseStudents values(NEWID(),'{0}','{1}')  ", courseId, stuId);
                                lstSql.Add(sql1);
                            }
                        }  
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



	    public bool DeletCourseStudentbyCourseId(string coursiId)
	    {
	        try
	        {
	            var strs = new List<string>();
	            var sql = string.Format(" delete from  CourseStudents  where CourseId='{0}' ", coursiId);
                var sql1 = string.Format(" update Course set IsQuestionCreate='{1}' where CourseId='{0}' ", coursiId,
	                "已删除");

	            strs.Add(sql);
	            strs.Add(sql1);

	            DbHelperSQL.ExecuteSqlTran(strs);

	            return true;

	        }
	        catch (Exception ex)
	        {
	            LogHelper.WriteLogofExceptioin(ex);
	            return false;
	        }
	    }



        /// <summary>
        /// 获取未完成此次调查的学生信息
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public DataSet GetNofinishedStu(string courseId)
        {
            try
            {
                var sql = string.Format(" select b.StuName,b.School,b.TelNo from   CourseStudents a, Student b  where a.CurState=0  and a.CourseId='{0}' and a.StudentId=b.StudentId "  
                                        , courseId);

                return DbHelperSQL.Query(sql);

            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return null;
            }
        }



        public DataSet GetfinishedStu(string courseId)
        {
            try
            {
                var sql = string.Format(" select b.StuName,b.School,b.TelNo from   CourseStudents a, Student b  where a.CurState=1  and a.CourseId='{0}' and a.StudentId=b.StudentId "
                                        , courseId);

                return DbHelperSQL.Query(sql);

            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return null;
            }
        }



        public DataSet GetNofinishedStuListByPage(string courseId, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby + " desc ");
            }
            else
            {
                strSql.Append("order by T.QuestionnaireId desc");
            }
            strSql.Append(")AS Row, T.*  from   ");
            strSql.Append(" ( select b.StudentId,b.StuName,b.School,b.TelNo from   CourseStudents a, Student b      ");
            strSql.Append(string.Format(" where a.CurState=0  and a.CourseId='{0}' and a.StudentId=b.StudentId   )  ", courseId));
            strSql.Append("  T ");
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }





        public int GetNofinishedStuNum(string courseId)
        {
            try
            {
                var sql = string.Format(" select count(b.StudentId) as TotNum  from   CourseStudents a, Student b  where a.CurState=0  and a.CourseId='{0}' and a.StudentId=b.StudentId "
                                        , courseId);

                var ds= DbHelperSQL.Query(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    return Convert.ToInt32(ds.Tables[0].Rows[0]["TotNum"]);
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return 0;
            }
        }




        public DataSet GetfinishedStuListByPage(string courseId, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby + " desc ");
            }
            else
            {
                strSql.Append("order by T.QuestionnaireId desc");
            }
            strSql.Append(")AS Row, T.*  from   ");
            strSql.Append(" ( select b.StudentId,b.StuName,b.School,b.TelNo from   CourseStudents a, Student b      ");
            strSql.Append(string.Format(" where a.CurState=1  and a.CourseId='{0}' and a.StudentId=b.StudentId   )  ", courseId));
            strSql.Append("  T ");
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }





        public int GetfinishedStuNum(string courseId)
        {
            try
            {
                var sql = string.Format(" select count(b.StudentId) as TotNum  from   CourseStudents a, Student b  where a.CurState=1  and a.CourseId='{0}' and a.StudentId=b.StudentId "
                                        , courseId);

                var ds = DbHelperSQL.Query(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    return Convert.ToInt32(ds.Tables[0].Rows[0]["TotNum"]);
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return 0;
            }
        }




	    /// <summary>
        /// 获取问卷状态
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
	    public DataSet GetCourseStudentState(string courseId)
	    {
	        try
	        {
                var sql = string.Format(" select a.StudentId,a.StuName,a.School,case  ISNULL(b.CurState,8)   when 8  then  0  else 1 end ck from Student a  " +
                                        "  left join CourseStudents b on a.StudentId=b.StudentId  and b.CourseId='{0}' order by ck  desc ", courseId);

                return DbHelperSQL.Query(sql);
	             
	        }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return null;
            } 
	    }





        public DataSet GetCourseStudentStateListByPage(string courseId, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby + " desc, " + "T.StuName  asc ");
            }
            else
            {
                strSql.Append("order by T.StuName asc");
            }
            strSql.Append(")AS Row, T.*  from   ");
            strSql.Append(" ( select a.StudentId,a.StuName,a.School,case  ISNULL(b.CurState,8)   when 8  then  0  else 1 end ck from Student a    ");
            strSql.Append(string.Format("  left join CourseStudents b on a.StudentId=b.StudentId  and b.CourseId='{0}'  )  ", courseId));
            strSql.Append("  T "); 
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetCourseClassStateListByPage(string courseId, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby + " desc, " + "T.Name  asc ");
            }
            else
            {
                strSql.Append("order by T.Name asc");
            }
            strSql.Append(")AS Row, T.*  from   ");
            strSql.Append(" ( select a.ID,a.Name,case  ISNULL(b.CurState,8)   when 8  then  0  else 1 end ck from Student a    ");
            strSql.Append(string.Format("  left join CourseClass b on a.ID=b.ClassId  and b.CourseId='{0}'  )  ", courseId));
            strSql.Append("  T ");
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }



        public DataSet GetSuggestion(string courseId)
        {
            try
            {
                if (!string.IsNullOrEmpty(courseId))
                {
                    var sql =
                        string.Format(" select b.StuName, a.Suggest from Questionnaire a, Student b  " +
                                      "where a.CourseId='{0}' and a.AppraiserId=b.StudentId  and  a.Suggest<>'' order by AppraiserTime desc  "
                            , courseId);

                    return DbHelperSQL.Query(sql); 
                }
                else
                {
                    return new DataSet();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return null;
            }
        }



        public DataSet GetSuggestionByPage(string courseId, string orderby, int startIndex, int endIndex)
        {
            try
            {
                if (!string.IsNullOrEmpty(courseId))
                {
                    //var sql =
                    //    string.Format(" select a.AppraiserId,b.StuName, a.Suggest from Questionnaire a, Student b  " +
                    //                  "where a.CourseId='{0}' and a.AppraiserId=b.StudentId  and  a.Suggest<>'' order by AppraiserTime desc  "
                    //        , courseId);

                    //return DbHelperSQL.Query(sql);


                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("SELECT * FROM ( ");
                    strSql.Append(" SELECT ROW_NUMBER() OVER (");
                    if (!string.IsNullOrEmpty(orderby.Trim()))
                    {
                        strSql.Append("order by T." + orderby + " desc ");
                    }
                    else
                    {
                        strSql.Append("order by T.AppraiserId desc");
                    }
                    strSql.Append(")AS Row, T.*  from   ");
                    strSql.Append(" ( select a.AppraiserId,b.StuName, a.Suggest from Questionnaire a, Student b    ");
                    strSql.Append(string.Format("  where a.CourseId='{0}' and a.AppraiserId=b.StudentId  and  a.Suggest<>'' )  ", courseId));
                    strSql.Append("  T ");
                    strSql.Append(" ) TT");
                    strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
                    return DbHelperSQL.Query(strSql.ToString());  
                }
                else
                {
                    return  new DataSet();
                }   
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return null;
            }
        }




        /// <summary>
        /// 提取当前处理人的需要参加调查
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
	    public DataSet GetStudentQuestionnaire(string studentId)
	    {
	        try
	        {
                var sql = string.Format(" select b.* from CourseStudents a, Course b  where a.CourseId=b.CourseId  and a.StudentId='{0}' and  a.CurState={1} ",
                    studentId,(int)EnumQuestionState.Start);

                return DbHelperSQL.Query(sql);
	        }
	        catch (Exception ex)
	        {
                LogHelper.WriteLogofExceptioin(ex);
                return null;
	        }
	    }




	    public bool SubmitQuestionnaireState(Guid stuId,Guid courId)
	    {
            try
            {
                var sql =
                    string.Format(" update CourseStudents  set CurState={2} where StudentId='{0}' and CourseId='{1}'  ",
                        stuId, courId,(int)EnumQuestionState.Submit);

                 DbHelperSQL.ExecuteSql(sql);
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return false;
            }
	    }



        /// <summary>
        /// 生成统计表
        /// </summary>
	    public void GetDataReport()
	    {
            try
            {


            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);  
            }
	    }





        public DataSet GetCourseQuestionnarieInfo(Guid currentUserId)
	    {
            try
            {
                var sql = string.Format(" select b.CourseId,b.CourseName,TeacherName,TeachPlace,TeachTime from CourseStudents a, Course b " +
                                        "where  a.StudentId='{0}'  and a.CurState={1} and  a.CourseId=b.CourseId   ", currentUserId, (int)EnumQuestionState.Start);

                return DbHelperSQL.Query(sql);

            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return null;
            }
	    }




	    public DataSet GetReportTile(Guid courseId)
	    {
            try
            {
                var ds = new DataSet();
                var sql = string.Format(" exec GetReportTitle '{0}' ", courseId); 
                ds = DbHelperSQL.Query(sql); 
                return ds;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return null;
            }
	    }


        public DataSet GetReport(Guid courseId)
        {
            try
            {
                var ds = new DataSet();
                var sql = string.Format(" exec GetReport '{0}' ", courseId);
                ds = DbHelperSQL.Query(sql);
                return ds; 
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return null;
            }
        }



       public string GetLevel(double satisify)
        {
            if (satisify > 0.9)
                return "优秀";
            if (satisify < 0.9 & satisify >= 0.7)
                return "良好";
            if (satisify >= 0.5 & satisify < 0.7)
                return "合格";
            if (satisify < 0.5)
                return "不合格";
            return "";

        }




	    public DataTable GetStuInfoofCourse(string courseId)
	    {
	        var sql = string.Format(" select a.UserName,a.UserAccount,a.UserPassWord,b.School " +
	                                " from SysUser a, Student b, CourseStudents c " +
	                                "where a.UserRole={0} and  a.UserId=b.StudentId and b.StudentId=c.StudentId  and c.CourseId='{1}' ",(int)EnumUserRole.Student,courseId);
	        var ds = DbHelperSQL.Query(sql);

	        if (ds != null && ds.Tables.Count > 0)
	        {
	            return ds.Tables[0];
	        }
	        else
	        {
	            return null;
	        }
	    }



	    public DataTable GetTotalReport()
	    {
            var sql = string.Format("exec GetTotalReport ");
	        var ds = DbHelperSQL.Query(sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            } 
	    }




        public DataTable GetTeacherReport()
        {
            var sql = string.Format("exec GetTeacherReport ");
            var ds = DbHelperSQL.Query(sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }


        public DataTable GetCourseReport()
        {
            var sql = string.Format("exec GetCourseReport ");
            var ds = DbHelperSQL.Query(sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        } 
        
        
        public DataTable GetOrgReport()
        {
            var sql = string.Format("exec GetOrgReport ");
            var ds = DbHelperSQL.Query(sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
         
	    #endregion  ExtensionMethod
	}
}

