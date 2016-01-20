using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TrainerEvaluate.Utility;
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.BLL
{
    //问卷信息表
    public partial class QuestionInfo
    {

        private readonly TrainerEvaluate.DAL.QuestionInfo dal = new TrainerEvaluate.DAL.QuestionInfo();
        public QuestionInfo()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(Guid Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(TrainerEvaluate.Models.QuestionInfo model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(TrainerEvaluate.Models.QuestionInfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(Guid Id)
        {

            return dal.Delete(Id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TrainerEvaluate.Models.QuestionInfo GetModel(Guid Id)
        {

            return dal.GetModel(Id);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        //public TrainerEvaluate.Models.QuestionInfo GetModelByCache(Guid Id)
        //{

        //    string CacheKey = "QuestionInfoModel-" + Id;
        //    object objModel = TrainerEvaluate.Common.DataCache.GetCache(CacheKey);
        //    if (objModel == null)
        //    {
        //        try
        //        {
        //            objModel = dal.GetModel(Id);
        //            if (objModel != null)
        //            {
        //                int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
        //                Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
        //            }
        //        }
        //        catch { }
        //    }
        //    return (TrainerEvaluate.Models.QuestionInfo)objModel;
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TrainerEvaluate.Models.QuestionInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TrainerEvaluate.Models.QuestionInfo> DataTableToList(DataTable dt)
        {
            List<TrainerEvaluate.Models.QuestionInfo> modelList = new List<TrainerEvaluate.Models.QuestionInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                TrainerEvaluate.Models.QuestionInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new TrainerEvaluate.Models.QuestionInfo();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = new Guid(dt.Rows[n]["Id"].ToString()); 
                    }
                    model.Name = dt.Rows[n]["Name"].ToString();
                    if (dt.Rows[n]["ClassCourseID"].ToString() != "")
                    {
                        model.ClassCourseID = new Guid(dt.Rows[n]["ClassCourseID"].ToString()); 
                    }
                    if (dt.Rows[n]["Status"].ToString() != "")
                    {
                        model.Status = int.Parse(dt.Rows[n]["Status"].ToString());
                    }
                    if (dt.Rows[n]["CreatedTime"].ToString() != "")
                    {
                        model.CreatedTime = DateTime.Parse(dt.Rows[n]["CreatedTime"].ToString());
                    }
                    if (dt.Rows[n]["StartTime"].ToString() != "")
                    {
                        model.StartTime = DateTime.Parse(dt.Rows[n]["StartTime"].ToString());
                    } if (dt.Rows[n]["EndTime"].ToString() != "")
                    {
                        model.EndTime = DateTime.Parse(dt.Rows[n]["EndTime"].ToString());
                    } 

                    modelList.Add(model);
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
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}



        public int GetRecordCountNew(string  strWhere)
        {
            var sql = "  select * from ( select b.RId,case  ISNULL(a.Status,1) when 1 then '未生成'  when 2 then '已生成' when 3 then '已取消' end   QuestionInfoStatus, " +
                      "  b.TeacherName,b.ClassName,b.CoursName as CourseName,d.TeachPlace " +
                      " from CourseTeacher b " +
                      " left join QuestionInfo a on b.RId = a.ClassCourseID " +
                      " inner join Course d on d.CourseId = b.CourseId ) AA ";
             
            StringBuilder strSql = new StringBuilder();
            strSql.Append(sql);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            int iCount = 0;
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null &&ds.Tables[0].Rows.Count>0)
            {
                iCount = ds.Tables[0].Rows.Count;
            }
            return iCount;
        }


        public DataSet GetListByPageNew(string strWhere, string orderby, int startIndex, int endIndex)
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
            strSql.Append(")AS Row, T.*  from  ");

            //var sql1 = string.Format("(select a.Name,b.ClassId,b.CourseID,b.TeacherID,c.Name,d.CourseName,e.TeacherName,a.Status " +
            //                      "from  QuestionInfo a,ClassCourse b,Class c,Course d,Teacher e" +
            //                      " where  a.ClassCourseID=b.ID and b.ClassId=c.ID and d.CourseId=b.CourseID)"); 

            strSql.Append(" ( select b.RId as ID,a.Name,a.StartTime,a.EndTime,b.ClassId,b.CourseId,b.TeacherId, ");
            strSql.Append(" b.ClassName,b.CoursName as CourseName,b.TeacherName , ");
            strSql.Append(" d.TeachPlace,convert(varchar, b.StartDate, 102) as TeachTime ,convert(varchar, b.FinishDate, 102) as TeachFinishDate,   ");
            strSql.Append(" case  ISNULL(a.Status,1) when 1 then '未生成'  when 2 then '已生成' when 3 then '已取消' end   QuestionInfoStatus, ");
            strSql.Append(" ISNULL(a.Status,1) Status  ");
            strSql.Append(" from CourseTeacher b ");
            strSql.Append(" left join QuestionInfo a on b.RId = a.ClassCourseID ");
            strSql.Append(" inner join Course d on d.CourseId = b.CourseId )   T ");
            strSql.Append(" ) TT");

            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
                strSql.AppendFormat(" and TT.Row between {0} and {1}", startIndex, endIndex);
            }
            else
            {
                strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            }
          
            return DbHelperSQL.Query(strSql.ToString());
        }






        public bool CreateQuesInfo(string name, string classCourseId, string startTime, string endTime, out string msg)
        {
            try
            {
                msg = "";
                var sql = string.Format("  select Status from QuestionInfo  where ClassCourseID='{0}' ", classCourseId);
                var result= DbHelperSQL.GetSingle(sql);
                if (result != null)
                {
                    var re = Convert.ToInt32(result); 
                    if (re == 2)  //已生成
                    {
                        msg = "该问卷已生成！";
                        return false; 
                    }
                    if (re == 3)  //已取消
                    {
                        var ls = new List<string>();
                        var sql1 = string.Format(" update QuestionInfo set  Status=2,CreatedTime=GETDATE() where ClassCourseID='{0}' ", classCourseId);
                        var sql2 = string.Format(" insert into CourseStudents   select NEWID(),a.CourseID ,b.StudentId,0,GETDATE() from ClassCourse a, ClassStudents b where a.ClassId=b.ClassId  and a.ID='{0}'", classCourseId);
                        ls.Add(sql1);
                        ls.Add(sql2); 
                        var re1 = DbHelperSQL.ExecuteSqlTran(ls);
                        return re1 > 0;  
                    }
                    return  true;
                }
                else
                {
                    var id = Guid.NewGuid();
                    var ls = new List<string>();
                    var sTime = string.IsNullOrEmpty(startTime) ? "null" : startTime;
                    var eTime = string.IsNullOrEmpty(endTime) ? "null" : endTime;  
                    var sql1 = string.Format(" insert into  QuestionInfo (Id,Name,ClassCourseID,Status,CreatedTime,StartTime,EndTime) values('{0}','{1}','{2}',2,GETDATE(),'{3}','{4}')", id,name, classCourseId, sTime,eTime); 
                    var sql2 = string.Format(" insert into CourseStudents   select NEWID(),a.CourseID ,b.StudentId,0,GETDATE() from ClassCourse a, ClassStudents b where a.ClassId=b.ClassId  and a.ID='{0}'",id);

                    ls.Add(sql1);
                    ls.Add(sql2);
                    var re= DbHelperSQL.ExecuteSqlTran(ls); 
                    return re > 0;
                } 
         
            }
            catch (Exception ex)
            {
                msg = "";
                LogHelper.WriteLogofExceptioin(ex);
                return false;
            } 
        } 
        
        
        
        public bool EditQuesInfo( string name, string classCourseId, string startTime, string endTime, out string msg)
        {
            try
            {
                msg = "";
                var sql = string.Format("  update QuestionInfo set Name='{0}', StartTime='{1}',EndTime='{2}' where ClassCourseID='{3}' ", name, startTime, endTime, classCourseId);
                var result = DbHelperSQL.ExecuteSql(sql);
                if (result == 0)
                {
                    msg = "更新数据失败！";
                } 
                return result > 0;
            }
            catch (Exception ex)
            {
                msg = "更新数据失败！" + ex.Message;
                LogHelper.WriteLogofExceptioin(ex);
                return false;
            } 
        }



     



        public bool CancelQue(string classCourseId)
        {
            try
            {
                var ls = new List<string>();
                var sql = string.Format("  update QuestionInfo set Status=3 where ClassCourseID='{0}' ", classCourseId);
                var sql1 =
                    string.Format(
                        "  delete from CourseStudents where CourseId=(select e.CourseID from ClassCourse e where e.ID='{0}' ) and StudentId in( select b.StudentId from ClassStudents b,ClassCourse c where b.ClassId=c.ClassId and c.ID='{0}') ",
                        classCourseId);

                ls.Add(sql);
                ls.Add(sql1);
                var re = DbHelperSQL.ExecuteSqlTran(ls);
                return re > 0;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return false;
            }
        }



       



        #endregion

    }
}
