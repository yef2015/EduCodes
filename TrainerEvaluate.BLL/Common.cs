using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TrainerEvaluate.Utility;
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.BLL
{
    public static class Common
    {

        /// <summary>
        /// 提取代码表中的一种代码
        /// </summary>
        /// <param name="dType"></param>
        /// <returns></returns>
        public static DataTable GetDataSourceFromSysCode(string dType)
        {
            var dt = new DataTable();
            try
            {
                var sql = string.Format(" select  ID,  Name from Dictionaries  where Dstatus=1 and DType='{0}' ", dType);
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
            var name="";
            try
            {
                var sql = string.Format(" select  ID from Dictionaries  where Dstatus=1 and Name='{0}' ", dicName);
                 name = DbHelperSQL.GetSingle(sql).ToString(); 
            } 
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
            }
            return string.IsNullOrEmpty(name) ? "null" : name;
        }






    }
}
