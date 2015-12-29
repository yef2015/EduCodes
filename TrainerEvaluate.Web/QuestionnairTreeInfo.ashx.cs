using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json;
using TrainerEvaluate.Utility;

namespace TrainerEvaluate.Web
{
    /// <summary>
    /// QuestionnairTreeInfo 的摘要说明
    /// </summary>
    public class QuestionnairTreeInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            var opType = context.Request["t"];
            switch (opType)
            {
                case "treelist":
                    var str = GetData(context);
                    context.Response.Write(str);
                    //Add(context);
                    break;
                case "edit":
                    //Edit(id, context);
                    break;
                case "del":
                    //Del(id, context);
                    break;
                case "q":
                    //Query(context);
                    break;
                default:
                    break;
            }
        }

        private string GetData(HttpContext context)
        {
            //Expression<Func<SysFunc, bool>> where = PredicateExtensionses.True<SysFunc>();
            //where = where.And(m => m.IsValid == true);
            //var obj = unitOfWork.SysFuncBLL.GetPageList(where, m => m.OrderBy(u => u.ShowOrder), "");

            //var str = JsonConvert.SerializeObject(new { total = obj.TotalCount, rows = obj.LstData });

            //str = str.Replace("ParentId", "_parentId");

            //return str;


            var ds = new DataSet();
            var qustBll = new BLL.QuestionnaireSurvey();
            ds = qustBll.GetQuestionnaireSurveryList();
            var num = ds.Tables[0].Rows.Count;

            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            str = str.Replace("TreeNode", "_parentId");


            str = "{\"total\":7,\"rows\":"+
"[{\"Id\":\"f36d787f-b552-4fe7-845a-2e273084d057\",\"Name\":\"一、您对本次培训课程的总体评价是：\",\"ShowCode\":\"radAll\",\"_parentId\":null},"+
"{\"Id\":\"2e56be46-9de7-4c0a-9c0a-e2b93f5e89ee\",\"Name\":\"二、课程内容\",\"ShowCode\":null,\"_parentId\":null},"+
"{\"Id\":\"280af0f5-ce9c-4382-b646-75fd9fb53b25\",\"Name\":\"1.课程主题清晰明确\",\"ShowCode\":\"radSubject\",\"_parentId\":\"2e56be46-9de7-4c0a-9c0a-e2b93f5e89ee\"}," +
"{\"Id\":\"1b5bdcbe-2689-4d35-84d7-33e83560bed8\",\"Name\":\"5.课程内容有助于个人发展\",\"ShowCode\":\"radCourseDevelop\",\"_parentId\":\"2e56be46-9de7-4c0a-9c0a-e2b93f5e89ee\"}]}";
            return str;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}