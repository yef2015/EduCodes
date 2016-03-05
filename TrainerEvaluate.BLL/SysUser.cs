using System;
using System.Collections.Generic;
using System.Data;
using System.Security;
using System.Text;
using TrainerEvaluate.Utility;
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.BLL
{
	/// <summary>
	/// 系统用户表
	/// </summary>
	public partial class SysUser
	{
        private readonly TrainerEvaluate.DAL.SysUser dal = new TrainerEvaluate.DAL.SysUser();
        public SysUser()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(Guid UserId)
        {
            return dal.Exists(UserId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(TrainerEvaluate.Models.SysUser model)
        {
            return dal.Add(model);
        }

         /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddComeStudent(TrainerEvaluate.Models.SysUser model)
        {
            return dal.AddComeStudent(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(TrainerEvaluate.Models.SysUser model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(Guid UserId)
        {

            return dal.Delete(UserId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string UserIdlist)
        {
            return dal.DeleteList(UserIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TrainerEvaluate.Models.SysUser GetModel(Guid UserId)
        {

            return dal.GetModel(UserId);
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TrainerEvaluate.Models.SysUser> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TrainerEvaluate.Models.SysUser> DataTableToList(DataTable dt)
        {
            List<TrainerEvaluate.Models.SysUser> modelList = new List<TrainerEvaluate.Models.SysUser>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                TrainerEvaluate.Models.SysUser model;
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
            return dal.GetListByPage(strWhere, sort, startIndex, endIndex,order);
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


	    public Models.SysUser GetSysUserByAccount(string accountId,string pwd)
	    {
            var userList = GetModelList(string.Format("  UserAccount='{0}' and UserPassWord='{1}'", accountId,pwd));
	        if (userList != null && userList.Count == 1)
	        {
	            return userList[0];
	        }
	        else
	        {
                LogHelper.WriteLogofExceptioin(new Exception("用户信息重复或为找到"));
	            return null;
	        }
	    }

        public Models.SysUser GetSysUserByIdentityNo(string identityNo, string pwd)
        {
            var userList = new List<TrainerEvaluate.Models.SysUser>();

            // 先进行学员的登录验证，主要验证身份证号、继教号、手机号
            DataSet ds = GetStudentInfoByCondition(identityNo);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string guid = ds.Tables[0].Rows[0]["StudentId"].ToString();
                    userList = GetModelList(string.Format(" UserId = '{0}' and UserPassWord = '{1}'", guid, pwd));
                }
                else
                {
                    userList = GetModelList(string.Format(" (UserAccount = '{0}' or IdentityNo = '{1}') and UserPassWord = '{2}'", identityNo, identityNo, pwd));
                }
            }
            

            if (userList != null && userList.Count == 1)
            {
                return userList[0];
            }
            else
            {
                LogHelper.WriteLogofExceptioin(new Exception("用户信息重复或未找到"));
                return null;
            }
        }

	    public bool GetAccountExsist(string userAccount,Guid useGuid)
	    {
            return dal.ExistsAccount(userAccount, useGuid);
	    }


        public bool ExistsAccountByAC(string userAccount)
	    {
            return dal.ExistsAccountByAC(userAccount);
	    }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TrainerEvaluate.Models.SysUser GetModelByIdentityNo(string identityNo)
        {

            return dal.GetModelByIdentityNo(identityNo);
        }




        /// <summary>
        /// 取班级负责人信息
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="orderby"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public DataSet GetPrjChargebyClassInfo(string classId, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby + " desc, " + "T.UserName   asc ");
            }
            else
            {
                strSql.Append("order by T.TeacherName asc");
            }
            strSql.Append(")AS Row, T.*  from   ");
            strSql.Append(" ( select a.UserId,a.UserName, a.Dept, case ISNULL(b.RId,'00000000-0000-0000-0000-000000000000')  when '00000000-0000-0000-0000-000000000000' then  0  else 1 end ck    ");
            strSql.Append(string.Format("  from SysUser a left join  ClassTeacher b on a.UserId=b.TeacherId and b.ClassId={0}  where a.Status=1 and a.UserRole=3  "
                + " and a.UserId in(select a.UserId from dbo.SysRoleUser a "
                + " left join Roles b on a.RoleId = b.ID where b.Name = '项目负责人' and b.Rstatus = 1) "
                +"  )  ", classId));
            strSql.Append("  T ");
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }



        /// <summary>
        /// 班级项目负责人信息(全选)
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public bool SaveChooseForAllofClass(string classId)
        {
            try
            {
                if (!string.IsNullOrEmpty(classId))
                {
                    var lstSql = new List<string>();
                    var sql1 = string.Format("    insert into ClassTeacher select NEWID(),'{0}',  UserId  from SysUser a where a.Status=1 and a.UserRole=3  ", classId);
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



        /// <summary>
        ///  班级项目负责人信息(全不选)
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public bool DeletTeacherofClass(string classId)
        {
            try
            {
                var strs = new List<string>();
                var sql = string.Format("  delete from ClassTeacher where ClassId='{0}' ", classId);
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



       /// <summary>
       /// 保存班级的项目负责人信息
       /// </summary>
       /// <param name="teaids"></param>
       /// <param name="classId"></param>
       /// <returns></returns>
        public bool SaveChooseTeacherofClass(string[] teaids, string classId)
        {
            try
            {
                if (!string.IsNullOrEmpty(classId))
                {
                    if (teaids != null && teaids.Length > 0)
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
                        sb = sb.Remove(sb.Length - 1, 1);
                        var sql2 = string.Format(" select UserName from SysUser  where UserId in  ({0})  ", sb);
                        var ds = DbHelperSQL.Query(sql2);
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            var sb1 = new StringBuilder();
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                sb1.Append(row["UserName"] + ",");
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

        public DataSet GetStudentInfoByCondition(string loginname)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from Student  ");
            strSql.Append(string.Format(" where (Mobile = '{0}' or TeachNo = '{0}' or IdentityNo = '{0}') and Status = 1 ",loginname));
            return DbHelperSQL.Query(strSql.ToString());
        }



        public string GetPwd(string idNo)
        {
            if (!string.IsNullOrEmpty(idNo) && idNo.Length > 6)
            {
                return idNo.Substring(idNo.Length - 6, 6);
            }
            else
            {
                return "000000";
            }
        }

	    #endregion  ExtensionMethod
	}
}

