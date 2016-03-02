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
    /// NetEnterStudent
    /// </summary>
    public partial class NetEnterStudent
    {
        private readonly TrainerEvaluate.DAL.NetEnterStudent dal = new TrainerEvaluate.DAL.NetEnterStudent();
        public NetEnterStudent()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(Guid Guid)
        {
            return dal.Exists(Guid);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Models.NetEnterStudent model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Models.NetEnterStudent model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(Guid Guid)
        {

            return dal.Delete(Guid);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string Guidlist)
        {
            return dal.DeleteList(Guidlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Models.NetEnterStudent GetModel(Guid Guid)
        {

            return dal.GetModel(Guid);
        }

        ///// <summary>
        ///// 得到一个对象实体，从缓存中
        ///// </summary>
        //public Models.NetEnterStudent GetModelByCache(Guid Guid)
        //{

        //    string CacheKey = "NetEnterStudentModel-" + Guid;
        //    object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
        //    if (objModel == null)
        //    {
        //        try
        //        {
        //            objModel = dal.GetModel(Guid);
        //            if (objModel != null)
        //            {
        //                int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
        //                Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
        //            }
        //        }
        //        catch { }
        //    }
        //    return (Models.NetEnterStudent)objModel;
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Models.NetEnterStudent> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Models.NetEnterStudent> DataTableToList(DataTable dt)
        {
            List<Models.NetEnterStudent> modelList = new List<Models.NetEnterStudent>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Models.NetEnterStudent model;
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
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
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

        /// <summary>
        /// 取消报名，删除报名记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CancelEnterFor(string userid,string netid)
        {
            return dal.CancelEnterFor(userid, netid);
        }

        public DataSet GetNetStudentYet(string studentId, string name, string desp, int startIndex, int endIndex)
        {
            try
            {
                //var sql = string.Format("select * from NetEnterFor where IsDelete = 0 "
                //   + " and Guid in( select NetEnteryId from NetEnterStudent where StudentId = '{0}' and IsDelete = 0)", studentId);

                //StringBuilder strSql = new StringBuilder();
                //strSql.Append("SELECT * FROM ( ");
                //strSql.Append(" SELECT ROW_NUMBER() OVER (");
                //strSql.Append("order by TrainName asc");
                //strSql.Append(")AS Row, T.*  from   ");
                //strSql.Append(" ( " + sql + "  ) ");
                //strSql.Append(" T ");

                //string strWhere = string.Empty;
                //if (!string.IsNullOrEmpty(name))
                //{
                //    strWhere += " and  TrainName like '%" + name + "%'";
                //}
                //if (!string.IsNullOrEmpty(desp))
                //{
                //    strWhere += " and  explain like '%" + desp + "%'";
                //}

                //if (!string.IsNullOrEmpty(strWhere.Trim()))
                //{
                //    strSql.Append(" WHERE 1 = 1 " + strWhere);
                //}

                //strSql.Append(" ) TT");
                //strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
                //DataSet ds = DbHelperSQL.Query(strSql.ToString());
                //return ds;



                var sql = string.Format("select a.*  from  Class a ,ClassStudents b  where a.ID=b.ClassId and b.StudentId='{0}'", studentId);

                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT * FROM ( ");
                strSql.Append(" SELECT ROW_NUMBER() OVER (");
                strSql.Append("order by Name asc");
                strSql.Append(")AS Row, T.*  from   ");
                strSql.Append(" ( " + sql + "  ) ");
                strSql.Append(" T ");

                string strWhere = string.Empty;
                if (!string.IsNullOrEmpty(name))
                {
                    strWhere += " and  Name like '%" + name + "%'";
                }
                //if (!string.IsNullOrEmpty(desp))
                //{
                //    strWhere += " and  explain like '%" + desp + "%'";
                //}

                if (!string.IsNullOrEmpty(strWhere.Trim()))
                {
                    strSql.Append(" WHERE 1 = 1 " + strWhere);
                }

                strSql.Append(" ) TT");
                strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
                DataSet ds = DbHelperSQL.Query(strSql.ToString());
                return ds;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return new DataSet();
            }
        }

        public int GetNetStudentYetCount(string studentId, string name, string desp)
        {
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(name))
            {
                strWhere += " and TrainName like '%" + name + "%'";
            }
            if (!string.IsNullOrEmpty(desp))
            {
                strWhere += " and explain like '%" + desp + "%'";
            }

            var sql = string.Format("select COUNT(1) from NetEnterFor where IsDelete = 0 "
                    + " and Guid in( select NetEnteryId from NetEnterStudent "
                    + " where StudentId = '{0}' and IsDelete = 0) {1} ", studentId, strWhere);

            object obj = DbHelperSQL.GetSingle(sql);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }


        public DataSet GetNetStudentEve(string studentId, string name, string desp, int startIndex, int endIndex)
        {
            try
            {
                var sql = string.Format("select * from NetEnterFor where IsDelete = 0 "
                   + " and Guid not in( select NetEnteryId from NetEnterStudent where StudentId = '{0}' and IsDelete = 0)", studentId);

                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT * FROM ( ");
                strSql.Append(" SELECT ROW_NUMBER() OVER (");
                strSql.Append("order by TrainName asc");
                strSql.Append(")AS Row, T.*  from   ");
                strSql.Append(" ( " + sql + "  ) ");
                strSql.Append(" T ");

                string strWhere = string.Empty;
                if (!string.IsNullOrEmpty(name))
                {
                    strWhere += " and  TrainName like '%" + name + "%'";
                }
                if (!string.IsNullOrEmpty(desp))
                {
                    strWhere += " and  explain like '%" + desp + "%'";
                }

                if (!string.IsNullOrEmpty(strWhere.Trim()))
                {
                    strSql.Append(" WHERE 1 = 1 " + strWhere);
                }

                strSql.Append(" ) TT");
                strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
                DataSet ds = DbHelperSQL.Query(strSql.ToString());
                return ds;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return new DataSet();
            }
        }

        public int GetNetStudentEveCount(string studentId, string name, string desp)
        {
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(name))
            {
                strWhere += " and TrainName like '%" + name + "%'";
            }
            if (!string.IsNullOrEmpty(desp))
            {
                strWhere += " and explain like '%" + desp + "%'";
            }

            var sql = string.Format("select COUNT(1) from NetEnterFor where IsDelete = 0 "
                    + " and Guid not in( select NetEnteryId from NetEnterStudent "
                    + " where StudentId = '{0}' and IsDelete = 0) {1} ", studentId, strWhere);

            object obj = DbHelperSQL.GetSingle(sql);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }


        #endregion  ExtensionMethod
    }
}
