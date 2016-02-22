using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using TrainerEvaluate.BLL;
using TrainerEvaluate.Utility;

namespace TrainerEvaluate.Web
{
    /// <summary>
    /// NetForStudentInfo 的摘要说明
    /// </summary>
    public class NetForStudentInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var opType = context.Request["t"];
            var id = context.Request["id"];
            switch (opType)
            {
                case "teve":
                    SaveNetFor(context);
                    break;
                case "tyet":
                    SaveNetCancel(context);
                    break;
                case "qeve":
                    var strEve=  GetForStudentEve(context);
                    context.Response.Write(strEve);
                    break;
                case "qyet":
                  var strYet=  GetForStudentYet(context);
                  context.Response.Write(strYet);
                    break;
                case "qjoin":
                    var stcla = GetClassInfoByStudentId(context);
                    context.Response.Write(stcla);
                    break;
                default:
                    var str = GetData(context);
                    context.Response.Write(str);
                    break;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private string GetClassInfoByStudentId(HttpContext context)
        {
            var str = string.Empty;

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var name = context.Request["name"];
            var desp = context.Request["desp"];

            var ds = new DataSet();
            var classBll = new BLL.Class();
            var studentId = context.Request["studentId"];

            if (!string.IsNullOrEmpty(studentId))
            {
                var num = classBll.GetRecordCountByStudentId(studentId, name, desp);

                ds = classBll.GetClassInfoByStudentId(studentId, name, desp, startIndex, endIndex);

                str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            }

            return str;
        }

        private string GetData(HttpContext context)
        {
            var ds = new DataSet();
            var dsBll = new BLL.SPSchoolDistrict();

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "SchDisName" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "asc" : context.Request["order"];

            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = dsBll.GetRecordCount(" Status = 1 ");
            ds = dsBll.GetListByPage(" Status = 1 ", sort, startIndex, endIndex, order);
            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            return str;
        }

        private string GetForStudentYet(HttpContext context)
        {
            var ds = new DataSet();
            var dsBll = new BLL.NetEnterStudent();

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "LastUpdateTime" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "desc" : context.Request["order"];

            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var name = context.Request["name"];
            var desp = context.Request["desp"];
            var studentId = context.Request["studentId"];

            var num = dsBll.GetNetStudentYetCount(studentId, name, desp);
            ds = dsBll.GetNetStudentYet(studentId, name, desp, startIndex, endIndex);
            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            return str;
        }

        private string GetForStudentEve(HttpContext context)
        {
            var ds = new DataSet();
            var dsBll = new BLL.NetEnterStudent();

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var name = context.Request["name"];
            var desp = context.Request["desp"];
            var studentId = context.Request["studentId"];

            var num = dsBll.GetNetStudentEveCount(studentId, name, desp);
            ds = dsBll.GetNetStudentEve(studentId, name, desp, startIndex, endIndex);
            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            return str;
        }

        /// <summary>
        /// 报名
        /// </summary>
        /// <param name="context"></param>
        private void SaveNetFor(HttpContext context)
        {
            var result = false;
            var msg = "";
            try
            {
                var stuModel = new Models.NetEnterStudent();
                stuModel.StudentId = Guid.NewGuid();

                stuModel.StudentId = Guid.Parse(context.Request["userid"]);
                stuModel.StuName = context.Request["username"];
                stuModel.NetEnteryId = Guid.Parse(context.Request["trainid"]);
                stuModel.NetEnterName = context.Request["trainname"];
                stuModel.CreateId = stuModel.StudentId.ToString();
                stuModel.CreateName = stuModel.StuName;
                stuModel.CreateTime = DateTime.Now;
                stuModel.LastUpdateTime = DateTime.Now;
                stuModel.IsDelete = false;

                var stuBll = new BLL.NetEnterStudent();
                result = stuBll.Add(stuModel);

                // 更新培训班的人数
                var sbll = new BLL.NetEnterFor();
                sbll.EditEnterForNum(stuModel.NetEnteryId);

                if (!result)
                {
                    msg = "保存失败！";
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                result = false;
                msg = ex.Message;

            }
            //  var str = JsonConvert.SerializeObject(new { success = result, errorMsg = msg});
            context.Response.Write(msg);
        }


        /// <summary>
        /// 取消报名
        /// </summary>
        /// <param name="context"></param>
        private void SaveNetCancel(HttpContext context)
        {
            var result = false;
            var msg = "";
            try
            {
                var userid = context.Request["userid"];
                var netid = context.Request["trainid"];
                // 取消报名，删除报名的记录
                var stuBll = new BLL.NetEnterStudent();
                result = stuBll.CancelEnterFor(userid, netid);

                // 取消报名更新培训班的报名人数
                var sbll = new BLL.NetEnterFor();
                sbll.EditCancelNum(netid);

                if (!result)
                {
                    msg = "保存失败！";
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                result = false;
                msg = ex.Message;

            }
            //  var str = JsonConvert.SerializeObject(new { success = result, errorMsg = msg});
            context.Response.Write(msg);
        }
    }
}