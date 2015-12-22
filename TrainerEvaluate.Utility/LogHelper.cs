//using System.Linq;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.Utility
{

    public enum EnumLogType
    {
        [Description("操作日志")]
        OpLog = 1,

        [Description("异常日志")]
        ExLog = 2

    }


    public class LogHelper
    {



        #region  写系统个日志方法（写在数据库日志表里）
        /// <summary>
        /// 写系统个日志方法（写在数据库日志表里）
        /// </summary>
        /// <param name="logTitle">标题</param>
        /// <param name="logContent">内容</param>
        /// <param name="userid">执行此操作的用户id</param>
        /// <param name="userName">执行此操作的用户名</param>
        public static void WriteSyslog(string logTitle, string logContent, Guid userid, string userName)
        {
            try
            {
                var sql = string.Format("Insert into SysLog(ID,MessageTitle,MessageDetail,MessageType,CreatorId,CreatorName,CreateTime) " +
                                        " values(newid(),'{0}','{1}',{2},'{3}','{4}',getdate())  ", logTitle, logContent, (int)EnumLogType.OpLog, userid, userName);
                DbHelperSQL.ExecuteSql(sql);

            }
            catch (Exception ex)
            {
                WriteEntry(ex);
            }
        }




        /// <summary>
        /// 将异常日志记录到数据库中
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteLogofExceptioin(Exception ex)
        {
            try
            {
                var sql = string.Format("Insert into SysLog(ID,MessageTitle,MessageDetail,MessageType,CreateTime) " +
                                        " values(newid(),'{0}','{1}',{2},getdate())  ", ex.Message, ex.StackTrace, (int)EnumLogType.ExLog);
                DbHelperSQL.ExecuteSql(sql);

            }
            catch (Exception ecp)
            {
                WriteEntry(ecp);
            }
        }


        /// <summary>
        /// 写服务器事件日志
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteEntry(Exception ex)
        {
            //EventLog SemensMediaEventLog = new EventLog();
            //if (!EventLog.SourceExists("SMDB"))
            //{
            //    EventLog.CreateEventSource("SMDB", "SMDBLog");
            //}
            //SemensMediaEventLog.Source = "SMDB";
            //SemensMediaEventLog.Log = "SMDBLog";

            //SemensMediaEventLog.WriteEntry(ex.ToString());


            WriteFileLog(ex.Message+"|"+ex.ToString());
        }



        /// <summary>
        /// 写服务器事件日志
        /// </summary>
        /// <param name="massage"></param>
        public static void WriteEntry(string massage)
        {
            //EventLog SemensMediaEventLog = new EventLog();
            //if (!EventLog.SourceExists("SMDB"))
            //{
            //    EventLog.CreateEventSource("SMDB", "SMDBLog");
            //}
            //SemensMediaEventLog.Source = "SMDB";
            //SemensMediaEventLog.Log = "SMDBLog";

            //SemensMediaEventLog.WriteEntry(massage);
            WriteFileLog(massage);
        }



        #endregion


        #region 写日志到文件
        public static void WriteFileLog(string str)
        {

            //if (System.Configuration.ConfigurationManager.AppSettings["isLog"] != "1")
            //{
            //    return;
            //}
            try
            {
                string dirPath = System.Web.HttpContext.Current.Server.MapPath("~/Log/");
                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath);
                StreamWriter sw;
                string path = System.Web.HttpContext.Current.Server.MapPath("~/Log/log.txt");
                if (!File.Exists(path))
                {
                    sw = File.CreateText(path);
                    sw.WriteLine(str + "：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(path);
                    if (fi.Length > 1048576)
                    {
                        fi.Delete();
                        if (!File.Exists(path))
                        {
                            sw = File.CreateText(path);
                            sw.WriteLine(str + "：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        else
                        {
                            sw = File.AppendText(path);
                            sw.WriteLine(str + "：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                    }
                    else
                    {
                        sw = File.AppendText(path);
                        sw.WriteLine(str + "：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                }
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                WriteSyslog("写日志异常", ex.Message, Guid.NewGuid(), "system");
            }
        }
        #endregion
    }






    public class LogEntity
    {


    }



}
