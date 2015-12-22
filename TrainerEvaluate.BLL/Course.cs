﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.BLL
{
	/// <summary>
	/// Course
	/// </summary>
	public partial class Course
	{
		private readonly TrainerEvaluate.DAL.Course dal=new TrainerEvaluate.DAL.Course();
		public Course()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid CourseId)
		{
			return dal.Exists(CourseId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public bool Add(Models.Course model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(Models.Course model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(Guid CourseId)
		{
			
			return dal.Delete(CourseId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string CourseIdlist )
		{
			return dal.DeleteList(CourseIdlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public Models.Course GetModel(Guid CourseId)
		{
			
			return dal.GetModel(CourseId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
        //public Models.Course GetModelByCache(Guid CourseId)
        //{
			
        //    string CacheKey = "CourseModel-" + CourseId;
        //    object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
        //    if (objModel == null)
        //    {
        //        try
        //        {
        //            objModel = dal.GetModel(CourseId);
        //            if (objModel != null)
        //            {
        //                int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
        //                Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
        //            }
        //        }
        //        catch{}
        //    }
        //    return (TrainerEvaluate.Model.TrainerEvaluate.Course)objModel;
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
        public List<Models.Course> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<Models.Course> DataTableToList(DataTable dt)
		{
            List<Models.Course> modelList = new List<Models.Course>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
                Models.Course model;
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


        public Guid GetCourseId(string courseName, string teacher, string teachTime, string teachPlace)
	    { 
	        return dal.GetCourseId(courseName,teacher,teachTime,teachPlace);
	    }



	    public Guid GetTop1Guid()
	    {
	        return dal.GetTop1Guid();
	    }




        public DataSet GetDataForExport(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select CourseName,TeacherName,TeachPlace,TeachTime,case  Type  when 1 then '通识' when 2 then '专业' when 3 then '修养' else ''  end as Type,Description ");
            strSql.Append(" FROM Course ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by CourseName asc, TeachTime desc ");
            return DbHelperSQL.Query(strSql.ToString());
        }



        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordOfClassCount(string strWhere,string classId)
        { 
            StringBuilder strSql = new StringBuilder();
            strSql.Append(string.Format("select count(1) FROM  Course   "));
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
        public DataSet GetListByPageOfClass(string strWhere, string sort, int startIndex, int endIndex, string order,string classId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(sort.Trim()))
            {
                strSql.Append("order by T." + sort + "  " + order);
            }
            else
            {
                strSql.Append("order by T.TeachTime desc");
            }
            strSql.Append(")AS Row, T.*  from ");
            strSql.Append("( "); 
            strSql.Append(string.Format(" select b.ClassId,  a.*,case ISNULL(b.ClassId,8) when 8 then 0 else 1 end ck  from Course a  left join ClassCourse b on a.CourseId=b.CourseID  and b.ClassId={0}     ", classId));
            strSql.Append(" )  T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString()); 
        }


	    #endregion  ExtensionMethod
	}
}

