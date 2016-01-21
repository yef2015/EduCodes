using System;
using System.Collections.Generic;
using System.Data;
using System.Security;
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
            var userList = GetModelList(string.Format(" (UserAccount = '{0}' or IdentityNo = '{1}') and UserPassWord = '{2}'", identityNo, identityNo, pwd));
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


	    #endregion  ExtensionMethod
	}
}

