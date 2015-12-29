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
    /// 问卷题目管理
    /// </summary>
    public partial class QuestionnaireSurvey
    {
        private readonly TrainerEvaluate.DAL.QuestionnaireSurvey dal = new TrainerEvaluate.DAL.QuestionnaireSurvey();
        public QuestionnaireSurvey()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(Guid QuestId)
        {
            return dal.Exists(QuestId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Models.QuestionnaireSurvey model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Models.QuestionnaireSurvey model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(Guid QuestId)
        {

            return dal.Delete(QuestId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string QuestIdlist)
        {
            return dal.DeleteList(QuestIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Models.QuestionnaireSurvey GetModel(Guid QuestId)
        {

            return dal.GetModel(QuestId);
        }

        ///// <summary>
        ///// 得到一个对象实体，从缓存中
        ///// </summary>
        //public Models.QuestionnaireSurvey GetModelByCache(Guid QuestId)
        //{

        //    string CacheKey = "QuestionnaireSurveyModel-" + QuestId;
        //    object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
        //    if (objModel == null)
        //    {
        //        try
        //        {
        //            objModel = dal.GetModel(QuestId);
        //            if (objModel != null)
        //            {
        //                int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
        //                Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
        //            }
        //        }
        //        catch { }
        //    }
        //    return (Models.QuestionnaireSurvey)objModel;
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
        public List<Models.QuestionnaireSurvey> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Models.QuestionnaireSurvey> DataTableToList(DataTable dt)
        {
            List<Models.QuestionnaireSurvey> modelList = new List<Models.QuestionnaireSurvey>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Models.QuestionnaireSurvey model;
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

        
        public DataSet GetQuestionnaireSurveryList()
        {
            try
            {
                var sql = string.Format(@" select upper(QuestId) AS QuestId,
                                              ShowName,ShowType,ShowOrder,ShowCode,ShowId,
                                              OptText1,OptType1,OptValue1,
                                              OptText2,OptType2,OptValue2,OptText3,OptType3,OptValue3,
                                              OptText4,OptType4,OptValue4,CreateTime,
                                              upper(ParentId) AS ParentId from QuestionnaireSurvey 
                                           where IsDelete = 0
                                            order by ShowOrder asc ");

                return DbHelperSQL.Query(sql);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return null;
            }
        }

		#endregion  ExtensionMethod
	}
}