﻿using System;
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
            strSql.Append("select a.Name,Object, Description, StartDate, FinishDate, Students, Point, b.Name as PointTypeName, Teacher, c.Name as  AreaName,d.Name as   LevelName,  e.Name as TypeName ");
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


	    #endregion  ExtensionMethod
	}
}

