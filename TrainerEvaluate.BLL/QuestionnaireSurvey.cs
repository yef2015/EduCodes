using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using NPOI.HSSF.Record.Chart;
using TrainerEvaluate.Utility;
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.BLL
{
    /// <summary>
    /// 问卷题目管理
    /// </summary>
    public partial class QuestionnaireSurvey
    {

        #region  ExtensionMethod

        public DataSet GetQuestionnaireSurveryList()
        {
            try
            {
                var sql = string.Format(" select * from dbo.QuestionnaireSurvey order by ShowOrder asc ");

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
