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
            var id = context.Request["id"];

            switch (opType)
            {
                case "treelist":
                    var str = GetData(context);
                    context.Response.Write(str);
                    //Add(context);
                    break;
                case "nq":
                    // 新增问题
                    AddQustion(id,context);
                    break;
                case "eq":
                    // 编辑问题
                    EditQuestion(id, context);
                    break;
                case "dq":
                    // 删除问题
                    DelQuestion(id, context);
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
            var ds = new DataSet();
            var qustBll = new BLL.QuestionnaireSurvey();
            ds = qustBll.GetQuestionnaireSurveryList();
            var num = ds.Tables[0].Rows.Count;

            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            str = str.Replace("ParentId", "_parentId");

            return str;
        }

        /// <summary>
        /// 新增问题
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="context"></param>
        private void AddQustion(string pid, HttpContext context)
        {
            var result = false;
            var msg = "";
            try
            {
                LogHelper.WriteLogofExceptioin(new Exception("begin"));
                var quesModel = new Models.QuestionnaireSurvey();
                quesModel.QuestId = Guid.NewGuid();
                SetModelValue(quesModel, context);
                quesModel.CreateTime = DateTime.Now;
                quesModel.LastModifyTime = DateTime.Now;
                quesModel.ParentId = pid;
                var quesBll = new BLL.QuestionnaireSurvey();

                result = quesBll.Add(quesModel);

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
            context.Response.Write(msg);
        }

        /// <summary>
        /// 编辑问题
        /// </summary>
        /// <param name="id"></param>
        /// <param name="context"></param>
        private void EditQuestion(string id, HttpContext context)
        { 
            var quesBll = new BLL.QuestionnaireSurvey();
            var quesModel = quesBll.GetModel(new Guid(id));
            quesModel.LastModifyTime = DateTime.Now;
            var result = false;
            var msg = "";
            try
            {
                SetModelValue(quesModel, context);

                result = quesBll.Update(quesModel);
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

            context.Response.Write(msg);
        }

        private void SetModelValue(Models.QuestionnaireSurvey quesModel, HttpContext context)
        {
            quesModel.ShowName = context.Request["ShowName"];
            quesModel.ShowOrder = context.Request["ShowOrder"];
        }

        /// <summary>
        /// 删除问题
        /// </summary>
        /// <param name="id"></param>
        /// <param name="context"></param>
        private void DelQuestion(string id,HttpContext context)
        {
            var quesBll = new BLL.QuestionnaireSurvey(); 
            var result = false;
            var msg = "";
            try
            {　   
                result= quesBll.Delete(new Guid(id)); 
               
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}