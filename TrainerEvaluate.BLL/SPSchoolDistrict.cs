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
    /// 学区信息表
    /// </summary>
    public partial class SPSchoolDistrict
    {
        private readonly DAL.SPSchoolDistrict dal = new DAL.SPSchoolDistrict();
        public SPSchoolDistrict()
		{}

        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(Guid SchDisId)
        {
            return dal.Exists(SchDisId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Models.SPSchoolDistrict model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Models.SPSchoolDistrict model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(Guid SchDisId)
        {

            return dal.Delete(SchDisId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string SchDisIdlist)
        {
            return dal.DeleteList(SchDisIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Models.SPSchoolDistrict GetModel(Guid SchDisId)
        {

            return dal.GetModel(SchDisId);
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
        public List<Models.SPSchoolDistrict> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Models.SPSchoolDistrict> DataTableToList(DataTable dt)
        {
            List<Models.SPSchoolDistrict> modelList = new List<Models.SPSchoolDistrict>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Models.SPSchoolDistrict model;
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
        public DataSet GetListByPage(string strWhere, string sort, int startIndex, int endIndex, string order)
        {
            return dal.GetListByPage(strWhere, sort, startIndex, endIndex, order);
        }

        #endregion  BasicMethod

        #region  ExtensionMethod

        public DataSet GetDataForExport(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select SchDisName,AddressInfo,PostCode,Description ");
            strSql.Append(" FROM SchoolDistrict ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by SchDisName asc, CreatedDate desc ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public static DataTable GetDataSourceOnSchDistrict()
        {
            var dt = new DataTable();
            try
            {
                var sql = " select  SchDisId AS Id,  SchDisName AS Name from SchoolDistrict  where Status=1 ";
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

        public static string GetDicValuefromName(string dicName)
        {
            var name = "";
            try
            {
                var sql = string.Format(" select  SchDisId AS Id from SchoolDistrict  where Status=1 and SchDisName='{0}' ", dicName);
                name = DbHelperSQL.GetSingle(sql).ToString();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
            }
            return string.IsNullOrEmpty(name) ? "null" : name;
        }

        #endregion  ExtensionMethod
    }
}