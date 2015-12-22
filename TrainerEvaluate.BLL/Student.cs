using System;
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
	public partial class Student
	{
		private readonly DAL.Student dal=new DAL.Student();
		public Student()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid StudentId)
		{
			return dal.Exists(StudentId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public bool Add(Models.Student model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(Models.Student model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(Guid StudentId)
		{
			
			return dal.Delete(StudentId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string StudentIdlist )
		{
			return dal.DeleteList(StudentIdlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public Models.Student GetModel(Guid StudentId)
		{
			
			return dal.GetModel(StudentId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
        //public Models.Student GetModelByCache(Guid StudentId)
        //{
			
        //    string CacheKey = "StudentModel-" + StudentId;
        //    object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
        //    if (objModel == null)
        //    {
        //        try
        //        {
        //            objModel = dal.GetModel(StudentId);
        //            if (objModel != null)
        //            {
        //                int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
        //                Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
        //            }
        //        }
        //        catch{}
        //    }
        //    return (TrainerEvaluate.Model.TrainerEvaluate.Student)objModel;
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
        public List<Models.Student> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<Models.Student> DataTableToList(DataTable dt)
		{
            List<Models.Student> modelList = new List<Models.Student>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
                Models.Student model;
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


	    public string GetStuAccount()
	    {
	        try
	        {
                var sql = string.Format(" select COUNT(*) num from  SysUser where UserRole=1 and YEAR(CreateTime)=YEAR(GETDATE())   ");
	            var result = DbHelperSQL.Query(sql);
                var num = 0;
	            if (result != null && result.Tables.Count > 0)
	            {
                     num = Convert.ToInt32(result.Tables[0].Rows[0]["num"])+1;
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

	    public DataSet GetDataForExport(string strWhere)
	    {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select StudentId,IdentityNo,StuName,Gender,School,Title,TelNo,CreateTime,LastModifyTime ");
            //strSql.Append("select StuName,case Gender when 1 then '男' else '女' end as GenderName,IdentityNo,School,JobTitle,TelNo, Birthday, Nation, FirstRecord, FirstSchool, LastRecord,LastSchool,PoliticsStatus, Rank, RankTime, Post, PostTime, Mobile, TeachNo, Status, Description ");
            //strSql.Append(" FROM Student ");

            strSql.Append(" select StuName,b.Name as GenderName,IdentityNo,School,c.Name as JobTitleName,TelNo, Birthday, d.Name as NationName, FirstRecord, FirstSchool, LastRecord,LastSchool,e.Name as PoliticsStatusName, Rank, RankTime, Post, PostTime, Mobile, TeachNo,  Description   ");
             strSql.Append(" from Student a left join Dictionaries b  on  a.Gender=b.ID left join Dictionaries c on  a.JobTitle=c.ID  left join Dictionaries d on a.Nation=d.ID  left join Dictionaries e on  a.PoliticsStatus=e.ID  ");
            strSql.Append(" where a.Status=1   "); 
             
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            strSql.Append(" order by a.StuName asc, a.CreateTime desc ");
            return DbHelperSQL.Query(strSql.ToString());
	    }



        public DataSet GetClassStuListByPage(string classId, string orderby, int startIndex, int endIndex)
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
            strSql.Append(" ( select a.*,case ISNULL(b.RId,'00000000-0000-0000-0000-000000000000')  when '00000000-0000-0000-0000-000000000000' then  0  else 1 end ck ");
            strSql.Append(string.Format("  from Student a left join  ClassStudents b on a.StudentId=b.StudentId and b.ClassId={0}  where  a.Status=1  )  ", classId));
            strSql.Append("  T ");
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }




        public bool SaveClassStuForAll(string classId)
        {
            try
            {
                if (!string.IsNullOrEmpty(classId))
                {
                    var lstSql = new List<string>();
                    lstSql.Add(string.Format(" delete from  ClassStudents  where ClassId={0} ", classId));
                    var sql1 = string.Format("  insert into ClassStudents select NEWID(),{0},  StudentId,getdate() from Student ", classId);
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




        public bool DeletClassStuForAll(string classId)
        {
            try
            {
                var strs = new List<string>();
                var sql = string.Format(" delete from  ClassStudents  where ClassId={0}  ", classId);
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








        public bool SaveClassStu(string[] stuIds, string classId)
        {
            try
            {
                if (!string.IsNullOrEmpty(classId))
                {
                    if (stuIds != null && stuIds.Length > 0)
                    {
                        var lstSql = new List<string>();
                        lstSql.Add(string.Format(" delete from  ClassStudents  where ClassId='{0}' ", classId));
                        foreach (var stuId in stuIds)
                        {
                            if (!string.IsNullOrEmpty(stuId))
                            {
                                var sql1 =
                                    string.Format(
                                        "insert into ClassStudents values(NEWID(),'{0}',{1})  ",stuId, classId
                                        );
                                lstSql.Add(sql1);
                            }
                        }  
                        DbHelperSQL.ExecuteSqlTran(lstSql);
                    }
                    else
                    {
                        var lstSql = new List<string>();
                        lstSql.Add(string.Format(" delete from  ClassStudents  where ClassId='{0}' ", classId));  
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



	    #endregion  ExtensionMethod
	}
}

