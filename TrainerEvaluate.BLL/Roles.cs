using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TrainerEvaluate.Utility;
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.BLL
{
	/// <summary>
	/// 用户角色表
	/// </summary>
	public partial class Roles
	{
		private readonly TrainerEvaluate.DAL.Roles dal=new TrainerEvaluate.DAL.Roles();
		public Roles()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(TrainerEvaluate.Models.Roles model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(TrainerEvaluate.Models.Roles model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(Guid ID)
		{
			
			return dal.Delete(ID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(IDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public TrainerEvaluate.Models.Roles GetModel(Guid ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
        //public TrainerEvaluate.Models.Roles GetModelByCache(Guid ID)
        //{
			
        //    string CacheKey = "RolesModel-" + ID;
        //    object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
        //    if (objModel == null)
        //    {
        //        try
        //        {
        //            objModel = dal.GetModel(ID);
        //            if (objModel != null)
        //            {
        //                int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
        //                Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
        //            }
        //        }
        //        catch{}
        //    }
        //    return (TrainerEvaluate.Models.Roles)objModel;
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
		public List<TrainerEvaluate.Models.Roles> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<TrainerEvaluate.Models.Roles> DataTableToList(DataTable dt)
		{
			List<TrainerEvaluate.Models.Roles> modelList = new List<TrainerEvaluate.Models.Roles>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				TrainerEvaluate.Models.Roles model;
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
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex,string order)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex,order);
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



        public DataSet GetRoleFuncListByPage(string roleId, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby + " desc, " + "T.FuncName  asc ");
            }
            else
            {
                strSql.Append("order by T.FuncName asc");
            }
            strSql.Append(")AS Row, T.*  from   ");
            strSql.Append(" ( select  a.*, case ISNULL(b.ID,'00000000-0000-0000-0000-000000000000') when '00000000-0000-0000-0000-000000000000' then  0  else 1 end ck     ");
            strSql.Append(string.Format("   from SysFunc a left join  SysRoleFunc  b on a.ID=b.FuncId  and b.RoleId='{0}' )  ", roleId));
            strSql.Append("  T ");
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }




        public bool SaveAllFunc(string roleId)
        {
            try
            {
                if (!string.IsNullOrEmpty(roleId))
                {
                    var lstSql = new List<string>();
                    lstSql.Add(string.Format(" delete from  SysRoleFunc  where RoleId='{0}' ", roleId));
                    var sql1 = string.Format(" insert into SysRoleFunc select NEWID(),'{0}',  ID from SysFunc  where IsValid=1 ", roleId);
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


        public bool SaveChooseFunc(string[] funcIds, string roleId)
        {
            try
            {
                if (!string.IsNullOrEmpty(roleId))
                {
                    if (funcIds != null && funcIds.Length > 0)
                    {
                        var lstSql = new List<string>();
                        lstSql.Add(string.Format(" delete from  SysRoleFunc  where RoleId='{0}' ", roleId));
                        foreach (var funcId in funcIds)
                        {
                            if (!string.IsNullOrEmpty(funcId))
                            {
                                var sql1 =
                                    string.Format(
                                        "insert into SysRoleFunc values(NEWID(),'{0}','{1}')  ", roleId,
                                        funcId);
                                lstSql.Add(sql1);
                            }
                        } 
                        DbHelperSQL.ExecuteSqlTran(lstSql);
                    }
                    else
                    {
                        var lstSql = new List<string>();
                        lstSql.Add(string.Format(" delete from  SysRoleFunc  where RoleId='{0}' ", roleId));
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

     


     public bool DeletRoleFuncbyFuncId(string roleId)
        {
            try
            {
                var strs = new List<string>();
                var sql = string.Format(" delete from  SysRoleFunc  where RoleId='{0}' ", roleId);  
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



	    public DataSet GetRoleUserListByPage(string roleId,string roleName, string orderby, int startIndex, int endIndex)
	    {
            int userRole = 1;
            switch (roleName)
            { 
                case "超级管理员":
                    userRole = 3;
                    break;
                case "班主任":
                    userRole = 3;
                    break;
                case "学员":
                    userRole = 1;
                    break;
                case "教师":
                    userRole = 2;
                    break;
                default:
                    userRole = 1;
                    break;
            }

	        StringBuilder strSql = new StringBuilder();
	        strSql.Append("SELECT * FROM ( ");
	        strSql.Append(" SELECT ROW_NUMBER() OVER (");
	        if (!string.IsNullOrEmpty(orderby.Trim()))
	        {
                strSql.Append("order by T." + orderby + " desc, " + "T.UserName  asc ");
	        }
	        else
	        {
	            strSql.Append("order by T.FuncName asc");
	        }
	        strSql.Append(")AS Row, T.*  from   ");
	        strSql.Append(
                " ( select  a.*, case ISNULL(b.ID,'00000000-0000-0000-0000-000000000000') when '00000000-0000-0000-0000-000000000000' then  0  else 1 end ck     ");
	        strSql.Append(
                string.Format("   from SysUser a left join  SysRoleUser  b on a.UserId=b.UserId  and b.RoleId='{0}'   where UserRole ={1}  )  ",
	                roleId,userRole));
	        strSql.Append("  T ");
	        strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE   TT.Row between {0} and {1}", startIndex, endIndex);
	        return DbHelperSQL.Query(strSql.ToString());
	    }


        public bool SaveAllUser(string roleId)
        {
            try
            {
                if (!string.IsNullOrEmpty(roleId))
                {
                    var lstSql = new List<string>();
                    lstSql.Add(string.Format(" delete from  SysRoleUser  where RoleId='{0}' ", roleId));
                    var sql1 = string.Format(" insert into SysRoleUser select NEWID(),'{0}',  UserId from SysUser where Status=1 and  UserRole!=1", roleId);
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



        public bool SaveChooseUser(string[] userIds, string roleId)
        {
            try
            {
                if (!string.IsNullOrEmpty(roleId))
                {
                    if (userIds != null && userIds.Length > 0)
                    {
                        var lstSql = new List<string>();
                        lstSql.Add(string.Format(" delete from  SysRoleUser  where RoleId='{0}' ", roleId));
                        foreach (var userId in userIds)
                        {
                            if (!string.IsNullOrEmpty(userId))
                            {
                                var sql1 =
                                    string.Format(
                                        "insert into SysRoleUser values(NEWID(),'{0}','{1}')  ", roleId,
                                        userId);
                                lstSql.Add(sql1);
                            }
                        }
                        DbHelperSQL.ExecuteSqlTran(lstSql);
                    }
                    else
                    {
                        var lstSql = new List<string>();
                        lstSql.Add(string.Format(" delete from  SysRoleUser  where RoleId='{0}' ", roleId));
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



        public bool DeletRoleUserbyRoleId(string roleId)
        {
            try
            {
                var strs = new List<string>();
                var sql = string.Format(" delete from  SysRoleUser  where RoleId='{0}' ", roleId);
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


        public   DataTable GetDataForCombox()
        {
            var dt = new DataTable();
            try
            {
                var sql = string.Format("  select ID,Name from Roles where Rstatus=1 ");
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



	    public DataTable GetCurrentUserRoleInfo(Guid userid)
	    {
	        var sql =
	            string.Format(
	                "  select b.Name, d.FuncCode,d.FuncName from  SysRoleUser  a, Roles b, SysRoleFunc c,SysFunc d " +
                    " where a.RoleId=b.ID  and c.RoleId=b.ID and c.FuncId=d.ID and a.UserId='{0}' order by d.FuncSort ASC", userid);


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

