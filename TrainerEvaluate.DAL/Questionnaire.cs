using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using TrainerEvaluate.Models;
using TrainerEvaluate.Utility;
using TrainerEvaluate.Utility.DB;

//Please add references
namespace TrainerEvaluate.DAL
{
	/// <summary>
	/// 数据访问类:Questionnaire
	/// </summary>
	public partial class Questionnaire
	{
        public Questionnaire()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(Guid QuestionnaireId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Questionnaire");
            strSql.Append(" where QuestionnaireId=@QuestionnaireId ");
            SqlParameter[] parameters = {
					new SqlParameter("@QuestionnaireId", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = QuestionnaireId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(TrainerEvaluate.Models.Questionnaire model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Questionnaire(");
            strSql.Append("QuestionnaireId,CourseId,TotalEvaluation,CourseSubject,CourseRich,CoursePractical,CourseKey,CourseDevelop,TeacherPrepare,TeacherLanguage,TeacherBearing,TeacherStyle,TeacherCommunication,OrgService,OrgTime,OrgArrange,AppraiserId,AppraiserTime,Suggest,Total,TotalCousre,TotalTeacher,TotalOrg)");
            strSql.Append(" values (");
            strSql.Append("@QuestionnaireId,@CourseId,@TotalEvaluation,@CourseSubject,@CourseRich,@CoursePractical,@CourseKey,@CourseDevelop,@TeacherPrepare,@TeacherLanguage,@TeacherBearing,@TeacherStyle,@TeacherCommunication,@OrgService,@OrgTime,@OrgArrange,@AppraiserId,@AppraiserTime,@Suggest,@Total,@TotalCousre,@TotalTeacher,@TotalOrg)");
            SqlParameter[] parameters = {
					new SqlParameter("@QuestionnaireId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CourseId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@TotalEvaluation", SqlDbType.Int,4),
					new SqlParameter("@CourseSubject", SqlDbType.Int,4),
					new SqlParameter("@CourseRich", SqlDbType.Int,4),
					new SqlParameter("@CoursePractical", SqlDbType.Int,4),
					new SqlParameter("@CourseKey", SqlDbType.Int,4),
					new SqlParameter("@CourseDevelop", SqlDbType.Int,4),
					new SqlParameter("@TeacherPrepare", SqlDbType.Int,4),
					new SqlParameter("@TeacherLanguage", SqlDbType.Int,4),
					new SqlParameter("@TeacherBearing", SqlDbType.Int,4),
					new SqlParameter("@TeacherStyle", SqlDbType.Int,4),
					new SqlParameter("@TeacherCommunication", SqlDbType.Int,4),
					new SqlParameter("@OrgService", SqlDbType.Int,4),
					new SqlParameter("@OrgTime", SqlDbType.Int,4),
					new SqlParameter("@OrgArrange", SqlDbType.Int,4),
					new SqlParameter("@AppraiserId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@AppraiserTime", SqlDbType.DateTime),
					new SqlParameter("@Suggest", SqlDbType.NVarChar,-1),
					new SqlParameter("@Total", SqlDbType.Int,4),
					new SqlParameter("@TotalCousre", SqlDbType.Int,4),
					new SqlParameter("@TotalTeacher", SqlDbType.Int,4),
					new SqlParameter("@TotalOrg", SqlDbType.Int,4)
                                        };
            parameters[0].Value = Guid.NewGuid();
            parameters[1].Value = model.CourseId;
            parameters[2].Value = model.TotalEvaluation;
            parameters[3].Value = model.CourseSubject;
            parameters[4].Value = model.CourseRich;
            parameters[5].Value = model.CoursePractical;
            parameters[6].Value = model.CourseKey;
            parameters[7].Value = model.CourseDevelop;
            parameters[8].Value = model.TeacherPrepare;
            parameters[9].Value = model.TeacherLanguage;
            parameters[10].Value = model.TeacherBearing;
            parameters[11].Value = model.TeacherStyle;
            parameters[12].Value = model.TeacherCommunication;
            parameters[13].Value = model.OrgService;
            parameters[14].Value = model.OrgTime;
            parameters[15].Value = model.OrgArrange;
            parameters[16].Value =model.AppraiserId;
            parameters[17].Value = model.AppraiserTime;
            parameters[18].Value = model.Suggest;
            parameters[19].Value = model.Total;
            parameters[20].Value = model.TotalCousre;
            parameters[21].Value = model.TotalTeacher;
            parameters[22].Value = model.TotalOrg;

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
        public bool Update(TrainerEvaluate.Models.Questionnaire model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Questionnaire set ");
            strSql.Append("CourseId=@CourseId,");
            strSql.Append("TotalEvaluation=@TotalEvaluation,");
            strSql.Append("CourseSubject=@CourseSubject,");
            strSql.Append("CourseRich=@CourseRich,");
            strSql.Append("CoursePractical=@CoursePractical,");
            strSql.Append("CourseKey=@CourseKey,");
            strSql.Append("CourseDevelop=@CourseDevelop,");
            strSql.Append("TeacherPrepare=@TeacherPrepare,");
            strSql.Append("TeacherLanguage=@TeacherLanguage,");
            strSql.Append("TeacherBearing=@TeacherBearing,");
            strSql.Append("TeacherStyle=@TeacherStyle,");
            strSql.Append("TeacherCommunication=@TeacherCommunication,");
            strSql.Append("OrgService=@OrgService,");
            strSql.Append("OrgTime=@OrgTime,");
            strSql.Append("OrgArrange=@OrgArrange,");
            strSql.Append("AppraiserId=@AppraiserId,");
            strSql.Append("AppraiserTime=@AppraiserTime,");
            strSql.Append("Suggest=@Suggest");
            strSql.Append(" where QuestionnaireId=@QuestionnaireId ");
            SqlParameter[] parameters = {
					new SqlParameter("@CourseId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@TotalEvaluation", SqlDbType.Int,4),
					new SqlParameter("@CourseSubject", SqlDbType.Int,4),
					new SqlParameter("@CourseRich", SqlDbType.Int,4),
					new SqlParameter("@CoursePractical", SqlDbType.Int,4),
					new SqlParameter("@CourseKey", SqlDbType.Int,4),
					new SqlParameter("@CourseDevelop", SqlDbType.Int,4),
					new SqlParameter("@TeacherPrepare", SqlDbType.Int,4),
					new SqlParameter("@TeacherLanguage", SqlDbType.Int,4),
					new SqlParameter("@TeacherBearing", SqlDbType.Int,4),
					new SqlParameter("@TeacherStyle", SqlDbType.Int,4),
					new SqlParameter("@TeacherCommunication", SqlDbType.Int,4),
					new SqlParameter("@OrgService", SqlDbType.Int,4),
					new SqlParameter("@OrgTime", SqlDbType.Int,4),
					new SqlParameter("@OrgArrange", SqlDbType.Int,4),
					new SqlParameter("@AppraiserId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@AppraiserTime", SqlDbType.DateTime),
					new SqlParameter("@Suggest", SqlDbType.NVarChar,-1),
					new SqlParameter("@QuestionnaireId", SqlDbType.UniqueIdentifier,16)};
            parameters[0].Value = model.CourseId;
            parameters[1].Value = model.TotalEvaluation;
            parameters[2].Value = model.CourseSubject;
            parameters[3].Value = model.CourseRich;
            parameters[4].Value = model.CoursePractical;
            parameters[5].Value = model.CourseKey;
            parameters[6].Value = model.CourseDevelop;
            parameters[7].Value = model.TeacherPrepare;
            parameters[8].Value = model.TeacherLanguage;
            parameters[9].Value = model.TeacherBearing;
            parameters[10].Value = model.TeacherStyle;
            parameters[11].Value = model.TeacherCommunication;
            parameters[12].Value = model.OrgService;
            parameters[13].Value = model.OrgTime;
            parameters[14].Value = model.OrgArrange;
            parameters[15].Value = model.AppraiserId;
            parameters[16].Value = model.AppraiserTime;
            parameters[17].Value = model.Suggest;
            parameters[18].Value = model.QuestionnaireId;

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
        public bool Delete(Guid QuestionnaireId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Questionnaire ");
            strSql.Append(" where QuestionnaireId=@QuestionnaireId ");
            SqlParameter[] parameters = {
					new SqlParameter("@QuestionnaireId", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = QuestionnaireId;

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
        public bool DeleteList(string QuestionnaireIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Questionnaire ");
            strSql.Append(" where QuestionnaireId in (" + QuestionnaireIdlist + ")  ");
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
        public TrainerEvaluate.Models.Questionnaire GetModel(Guid QuestionnaireId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 QuestionnaireId,CourseId,TotalEvaluation,CourseSubject,CourseRich,CoursePractical,CourseKey,CourseDevelop,TeacherPrepare,TeacherLanguage,TeacherBearing,TeacherStyle,TeacherCommunication,OrgService,OrgTime,OrgArrange,AppraiserId,AppraiserTime,Suggest from Questionnaire ");
            strSql.Append(" where QuestionnaireId=@QuestionnaireId ");
            SqlParameter[] parameters = {
					new SqlParameter("@QuestionnaireId", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = QuestionnaireId;

            TrainerEvaluate.Models.Questionnaire model = new TrainerEvaluate.Models.Questionnaire();
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
        public TrainerEvaluate.Models.Questionnaire DataRowToModel(DataRow row)
        {
            TrainerEvaluate.Models.Questionnaire model = new TrainerEvaluate.Models.Questionnaire();
            if (row != null)
            {
                if (row["QuestionnaireId"] != null && row["QuestionnaireId"].ToString() != "")
                {
                    model.QuestionnaireId = new Guid(row["QuestionnaireId"].ToString());
                }
                if (row["CourseId"] != null && row["CourseId"].ToString() != "")
                {
                    model.CourseId = new Guid(row["CourseId"].ToString());
                }
                if (row["TotalEvaluation"] != null && row["TotalEvaluation"].ToString() != "")
                {
                    model.TotalEvaluation = int.Parse(row["TotalEvaluation"].ToString());
                }
                if (row["CourseSubject"] != null && row["CourseSubject"].ToString() != "")
                {
                    model.CourseSubject = int.Parse(row["CourseSubject"].ToString());
                }
                if (row["CourseRich"] != null && row["CourseRich"].ToString() != "")
                {
                    model.CourseRich = int.Parse(row["CourseRich"].ToString());
                }
                if (row["CoursePractical"] != null && row["CoursePractical"].ToString() != "")
                {
                    model.CoursePractical = int.Parse(row["CoursePractical"].ToString());
                }
                if (row["CourseKey"] != null && row["CourseKey"].ToString() != "")
                {
                    model.CourseKey = int.Parse(row["CourseKey"].ToString());
                }
                if (row["CourseDevelop"] != null && row["CourseDevelop"].ToString() != "")
                {
                    model.CourseDevelop = int.Parse(row["CourseDevelop"].ToString());
                }
                if (row["TeacherPrepare"] != null && row["TeacherPrepare"].ToString() != "")
                {
                    model.TeacherPrepare = int.Parse(row["TeacherPrepare"].ToString());
                }
                if (row["TeacherLanguage"] != null && row["TeacherLanguage"].ToString() != "")
                {
                    model.TeacherLanguage = int.Parse(row["TeacherLanguage"].ToString());
                }
                if (row["TeacherBearing"] != null && row["TeacherBearing"].ToString() != "")
                {
                    model.TeacherBearing = int.Parse(row["TeacherBearing"].ToString());
                }
                if (row["TeacherStyle"] != null && row["TeacherStyle"].ToString() != "")
                {
                    model.TeacherStyle = int.Parse(row["TeacherStyle"].ToString());
                }
                if (row["TeacherCommunication"] != null && row["TeacherCommunication"].ToString() != "")
                {
                    model.TeacherCommunication = int.Parse(row["TeacherCommunication"].ToString());
                }
                if (row["OrgService"] != null && row["OrgService"].ToString() != "")
                {
                    model.OrgService = int.Parse(row["OrgService"].ToString());
                }
                if (row["OrgTime"] != null && row["OrgTime"].ToString() != "")
                {
                    model.OrgTime = int.Parse(row["OrgTime"].ToString());
                }
                if (row["OrgArrange"] != null && row["OrgArrange"].ToString() != "")
                {
                    model.OrgArrange = int.Parse(row["OrgArrange"].ToString());
                }
                if (row["AppraiserId"] != null && row["AppraiserId"].ToString() != "")
                {
                    model.AppraiserId = new Guid(row["AppraiserId"].ToString());
                }
                if (row["AppraiserTime"] != null && row["AppraiserTime"].ToString() != "")
                {
                    model.AppraiserTime = DateTime.Parse(row["AppraiserTime"].ToString());
                }
                if (row["Suggest"] != null)
                {
                    model.Suggest = row["Suggest"].ToString();
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
            strSql.Append("select QuestionnaireId,CourseId,TotalEvaluation,CourseSubject,CourseRich,CoursePractical,CourseKey,CourseDevelop,TeacherPrepare,TeacherLanguage,TeacherBearing,TeacherStyle,TeacherCommunication,OrgService,OrgTime,OrgArrange,AppraiserId,AppraiserTime,Suggest ");
            strSql.Append(" FROM Questionnaire ");
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
            strSql.Append(" QuestionnaireId,CourseId,TotalEvaluation,CourseSubject,CourseRich,CoursePractical,CourseKey,CourseDevelop,TeacherPrepare,TeacherLanguage,TeacherBearing,TeacherStyle,TeacherCommunication,OrgService,OrgTime,OrgArrange,AppraiserId,AppraiserTime,Suggest ");
            strSql.Append(" FROM Questionnaire ");
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
            strSql.Append("select count(1) FROM Questionnaire ");
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
                strSql.Append("order by T.QuestionnaireId desc");
            }
            strSql.Append(")AS Row, T.*  from Questionnaire T ");
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
            parameters[0].Value = "Questionnaire";
            parameters[1].Value = "QuestionnaireId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod



        #region  ExtensionMethod

	    public int GetTotalAvg(string courseName, string teacher, string teahTime, string tPlace)
	    {
	        try
	        {

	            var sql = string.Format("  select  avg(Total) avgTotal  from  Questionnaire a,Course b " +
	                                    "where a.CourseId=b.CourseId and  b.CourseName='{0}' and b.TeacherName='{1}' and b.TeachTime='{2}' and b.TeachPlace='{3}' ",
	                courseName, teacher, teahTime, tPlace);
	            var ds = DbHelperSQL.Query(sql);
	            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
	            {
	                return Convert.ToInt32(ds.Tables[0].Rows[0]["avgTotal"]);
	            }
	            return 0;
	        }
	        catch (Exception ex)
	        {
	            return 0;
	        }
	    }


        public int GetTotalAvg(string cid)
        {
            try
            {

                var sql = string.Format("  select  avg(Total) avgTotal  from  Questionnaire a,Course b " +
                                        "where a.CourseId=b.CourseId and  b.CourseId='{0}'  ",
                    cid);
                var ds = DbHelperSQL.Query(sql);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToInt32(ds.Tables[0].Rows[0]["avgTotal"]);
                }
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        /// <summary>
        /// 获取满意度图表以及未完成数
        /// </summary>
        /// <param name="coursId"></param>
        /// <returns></returns>
        public decimal[] GetSatisfyPercent(Guid coursId)
        {
            try
            {
                var result = new decimal[8];
                var sql1 = string.Format("exec GetPercent '{0}'", coursId);
                var ds = DbHelperSQL.Query(sql1);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var row = ds.Tables[0].Rows[0];
                    result[0] = Convert.ToDecimal(row["top1"]);
                    result[1] = Convert.ToDecimal(row["top2"]);
                    result[2] = Convert.ToDecimal(row["top3"]);
                    result[3] = Convert.ToDecimal(row["top4"]);
                    result[4] = Convert.ToDecimal(row["top5"]);
                    result[5] = Convert.ToDecimal(row["nofinish"]);
                    result[6] = Convert.ToDecimal(row["totalNum"]);
                    result[7] = Convert.ToDecimal(row["totalDoneNum"]); 
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
	    /// 取最前六
	    /// </summary>
	    /// <returns></returns>
        public string[] GetTopSix(Guid coursId)
	    {
	        try
	        {
	            var result = new string[6];
                var sql1 = string.Format("exec GetTopSix '{0}'", coursId);
	            var ds = DbHelperSQL.Query(sql1);
	            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
	            {
	                result[0] = ds.Tables[0].Rows[0]["ItemName"].ToString();
	                result[1] = ds.Tables[0].Rows[1]["ItemName"].ToString();
	                result[2] = ds.Tables[0].Rows[2]["ItemName"].ToString();
	                result[3] = ds.Tables[0].Rows[3]["ItemName"].ToString();
	                result[4] = ds.Tables[0].Rows[4]["ItemName"].ToString();
	                result[5] = ds.Tables[0].Rows[5]["ItemName"].ToString();

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
        /// 取后6
        /// </summary>
        /// <param name="coursId"></param>
        /// <returns></returns>
        public string[] GetBottomSix(Guid coursId)
        {
            try
            { 
                var result = new string[6];
                var sql1 = string.Format("exec GetBottomSix '{0}'", coursId);
                var ds = DbHelperSQL.Query(sql1);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result[0] = ds.Tables[0].Rows[0]["ItemName"].ToString();
                    result[1] = ds.Tables[0].Rows[1]["ItemName"].ToString();
                    result[2] = ds.Tables[0].Rows[2]["ItemName"].ToString();
                    result[3] = ds.Tables[0].Rows[3]["ItemName"].ToString();
                    result[4] = ds.Tables[0].Rows[4]["ItemName"].ToString();
                    result[5] = ds.Tables[0].Rows[5]["ItemName"].ToString();

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

	    #endregion  ExtensionMethod
	}
}

