using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
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

        public static string GetDicIDfromName(string dicName)
        {
            var name = "";
            try
            {
                var sql = string.Format(" select  ID from Dictionaries  where Dstatus=1 and Name='{0}' ", dicName);
                var res = DbHelperSQL.GetSingle(sql);
                if (res != null)
                {
                    name = DbHelperSQL.GetSingle(sql).ToString();
                } 
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
            }
            return string.IsNullOrEmpty(name) ? "0" : name;
        }



        public static string ExecSql(string sql)
        {
            object result1;
            if (!string.IsNullOrEmpty(sql))
            {
                sql= sql.Trim().ToLower();
                
            }
            try
            {
                if (sql.StartsWith("select"))
                {
                    var dt = DbHelperSQL.Query(sql);
                    if (dt != null && dt.Tables.Count > 0)
                    {
                        result1 = dt.Tables[0];
                    }
                    else
                    {
                        result1 = "";
                    }
                }
                else if (sql.StartsWith("update"))
                {
                  result1= DbHelperSQL.ExecuteSql(sql);
                }
                else if (sql.StartsWith("delete"))
                {
                    result1 = DbHelperSQL.ExecuteSql(sql);
                }  
                else if (sql.StartsWith("alter"))
                {
                    result1 = DbHelperSQL.ExecuteSql(sql);
                }
                else if (sql.StartsWith("insert"))
                {
                    result1 = DbHelperSQL.ExecuteSql(sql);
                }
                else
                {
                    result1 = "";
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                result1 = "";
            } 
            var str = JsonConvert.SerializeObject(new { result = result1 });
            return str;
        }


    }
}
