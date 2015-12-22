using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NPOI.HSSF.Record.Chart;
using TrainerEvaluate.Utility;
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.BLL
{
    /// <summary>
    /// 学校信息表
    /// </summary>
    public partial class SPSchool
    {

        private readonly DAL.SPSchool dal = new DAL.SPSchool();
        public SPSchool()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid TeacherId)
		{
			return dal.Exists(TeacherId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public bool Add(Models.SPSchool model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(Models.SPSchool model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public bool Delete(Guid SchoolId)
		{

            return dal.Delete(SchoolId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string TeacherIdlist )
		{
			return dal.DeleteList(TeacherIdlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public Models.SPSchool GetModel(Guid SchoolId)
		{

            return dal.GetModel(SchoolId);
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
        public List<Models.SPSchool> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<Models.SPSchool> DataTableToList(DataTable dt)
		{
            List<Models.SPSchool> modelList = new List<Models.SPSchool>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
                Models.SPSchool model;
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

		#endregion  BasicMethod

		#region  ExtensionMethod

        public bool SaveChooseForAllofClass(string classId)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return false;
            }
        }

        public DataSet GetDataForExport(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select SchoolName,SchDisName,RunNatureName,SchoolTypeName,AddrNum,ClassNum,StudentNum,TeacherNum,PartyNum,LegalName,LinkTel,Description ");
            strSql.Append(" FROM School ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by SchoolName asc, CreatedDate desc ");
            return DbHelperSQL.Query(strSql.ToString());
        }

		#endregion  ExtensionMethod
	}
}


