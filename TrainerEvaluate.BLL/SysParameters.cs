using System;
using System.Collections.Generic;
using System.Data;
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.BLL
{
	/// <summary>
	/// 系统参数表
	///设置系统默认的一些参数
	/// </summary>
	public partial class SysParameters
	{
		private readonly TrainerEvaluate.DAL.SysParameters dal=new TrainerEvaluate.DAL.SysParameters();
		public SysParameters()
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
		public bool Add(TrainerEvaluate.Models.SysParameters model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(TrainerEvaluate.Models.SysParameters model)
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
		public TrainerEvaluate.Models.SysParameters GetModel(Guid ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
        //public TrainerEvaluate.Models.SysParameters GetModelByCache(Guid ID)
        //{
			
        //    string CacheKey = "SysParametersModel-" + ID;
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
        //    return (TrainerEvaluate.Models.SysParameters)objModel;
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
		public List<TrainerEvaluate.Models.SysParameters> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<TrainerEvaluate.Models.SysParameters> DataTableToList(DataTable dt)
		{
			List<TrainerEvaluate.Models.SysParameters> modelList = new List<TrainerEvaluate.Models.SysParameters>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				TrainerEvaluate.Models.SysParameters model;
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

		#endregion  ExtensionMethod
	}
}

