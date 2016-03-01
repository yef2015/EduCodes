using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NPOI.HSSF.Record.Chart;
using TrainerEvaluate.Utility;
using TrainerEvaluate.Utility.DB;
using System.Data.SqlClient;

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

        /// <summary>
        /// 根据学区，判断该学区下是否存在学校
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public static bool IsExistSchoolByShdistId(string shdId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from School");
            strSql.Append(" where Status = 1 and SchDisId=@SchDisId ");
            SqlParameter[] parameters = {
					new SqlParameter("@SchDisId", SqlDbType.NVarChar,50)};
            parameters[0].Value = shdId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取学校信息，为填充下拉框
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataSourceOnSchool()
        {
            var dt = new DataTable();
            try
            {
                var sql = " select  SchoolId AS Id,  SchoolName AS Name from School  where Status=1 ";
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

         /// <summary>
        /// 是否存在该记录,根据学校名称
        /// </summary>
        public bool ExistsBySchoolName(string SchoolName)
        {
            return dal.ExistsBySchoolName(SchoolName);
        }

		#endregion  ExtensionMethod
	}
}


