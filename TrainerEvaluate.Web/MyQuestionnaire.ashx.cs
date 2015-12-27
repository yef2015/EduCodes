using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using TrainerEvaluate.Utility;

namespace TrainerEvaluate.Web
{
    /// <summary>
    /// MyQuestionnaire 的摘要说明
    /// </summary>
    public class MyQuestionnaire : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            GetDataForCombobox(context);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }



        private void GetDataForCombobox(HttpContext context)
        {
            try
            {
                var uid = HttpContext.Current.Request["uid"];
                var questionbll = new BLL.Questionnaire();
                var ds = questionbll.GetStudentQuestionnaireNew(uid);

                //  [{"SUBITEM_VALUE":"1","SUBITEM_NAME":"男"},{"SUBITEM_VALUE":"2","SUBITEM_NAME":"女"}]  

                if (ds != null && ds.Tables.Count > 0)
                {
                    var str = new StringBuilder("[");
                    var i = 0;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        i++;
                        str.Append("{\"CourseId\": \"" + row["CourseId"] + "\",");
                        str.Append("\"CourseName\": \"" + i + ". " + row["CourseName"] + "\"},");
                    }
                    str.Remove(str.Length - 1, 1);
                    str.Append("]");

                    context.Response.Write(str.ToString());
                }
            }
            catch (Exception exception)
            {
                LogHelper.WriteLogofExceptioin(exception);
            }
        }

    }
}