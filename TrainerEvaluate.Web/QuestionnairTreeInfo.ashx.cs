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
                case "era":
                    // 编辑答案,单选
                    EditRadioAnswer(id, context);
                    break;
                case "etxt":
                    // 编辑答案,文本框
                    EditTextAnswer(id, context);
                    break;
                case "da":
                    // 删除答案
                    DelAnswer(id, context);
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

                switch (quesModel.ShowType)
                {
                    case "AnswerRadio":  // 答案是单选
                        quesModel.OptText1 = "满意";
                        quesModel.OptType1 = "radio";
                        quesModel.OptValue1 = "4";

                        quesModel.OptText2 = "比较满意";
                        quesModel.OptType2 = "radio";
                        quesModel.OptValue2 = "3";

                        quesModel.OptText3 = "一般";
                        quesModel.OptType3 = "radio";
                        quesModel.OptValue3 = "2";

                        quesModel.OptText4 = "不满意";
                        quesModel.OptType4 = "radio";
                        quesModel.OptValue4 = "1";
                        break;
                    default:
                        break;
                }

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

                // 重置单选内容
                switch (quesModel.ShowType)
                {
                    case "AnswerRadio": // 答案是单选
                        quesModel.OptText1 = "满意";
                        quesModel.OptType1 = "radio";
                        quesModel.OptValue1 = "4";

                        quesModel.OptText2 = "比较满意";
                        quesModel.OptType2 = "radio";
                        quesModel.OptValue2 = "3";

                        quesModel.OptText3 = "一般";
                        quesModel.OptType3 = "radio";
                        quesModel.OptValue3 = "2";

                        quesModel.OptText4 = "不满意";
                        quesModel.OptType4 = "radio";
                        quesModel.OptValue4 = "1";
                        break;
                    case "NoAnswer":    // 不带答案
                    case "AnswerText":  // 大标题，带答案，文本框
                        quesModel.OptText1 = "";
                        quesModel.OptType1 = "";
                        quesModel.OptValue1 = "";

                        quesModel.OptText2 = "";
                        quesModel.OptType2 = "";
                        quesModel.OptValue2 = "";

                        quesModel.OptText3 = "";
                        quesModel.OptType3 = "";
                        quesModel.OptValue3 = "";

                        quesModel.OptText4 = "";
                        quesModel.OptType4 = "";
                        quesModel.OptValue4 = "";
                        break;
                    default:
                        break;
                }

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
            quesModel.ShowType = context.Request["ShowType"];
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
            context.Response.Write(msg); 
        }
        
        /// <summary>
        /// 编辑答案，单选
        /// </summary>
        /// <param name="id"></param>
        /// <param name="context"></param>
        private void EditRadioAnswer(string id, HttpContext context)
        { 
            var quesBll = new BLL.QuestionnaireSurvey();
            var quesModel = quesBll.GetModel(new Guid(id));
            quesModel.LastModifyTime = DateTime.Now;

            var result = false;
            var isFlag = false;
            var msg = "";

            if (quesModel.ShowType == EnumQuestionType.AnswerRadio.ToString())
            {
                isFlag = true;
            }
            else
            {
                msg = "编辑答案，不是单选。";
            }

            if (isFlag)
            { 
                try
                {
                    SetAnswerValue(quesModel, context);

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
            }

            context.Response.Write(msg);
        }

        /// <summary>
        /// 编辑答案，文本框
        /// </summary>
        /// <param name="id"></param>
        /// <param name="context"></param>
        private void EditTextAnswer(string id, HttpContext context)
        { 
            var quesBll = new BLL.QuestionnaireSurvey();
            var quesModel = quesBll.GetModel(new Guid(id));
            quesModel.LastModifyTime = DateTime.Now;

            var result = false;
            var isFlag = false;
            var msg = "";

            if (quesModel.ShowType == EnumQuestionType.AnswerText.ToString())
            {
                isFlag = true;
            }
            else
            {
                msg = "编辑答案，不是文本框。";
            }

            if (isFlag)
            { 
                try
                {
                    quesModel.ShowCode = context.Request["ShowCodeText"];
                    quesModel.ShowId = context.Request["ShowIdText"];

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
            }
            context.Response.Write(msg);
        }  

        private void SetAnswerValue(Models.QuestionnaireSurvey quesModel, HttpContext context)
        {
            quesModel.ShowCode = context.Request["ShowCode"];
            quesModel.ShowId = context.Request["ShowId"];

            quesModel.OptText1 = context.Request["OptText1"];
            quesModel.OptType1 = context.Request["OptType1"];
            quesModel.OptValue1 = context.Request["OptValue1"];

            quesModel.OptText2 = context.Request["OptText2"];
            quesModel.OptType2 = context.Request["OptType2"];
            quesModel.OptValue2 = context.Request["OptValue2"];

            quesModel.OptText3 = context.Request["OptText3"];
            quesModel.OptType3 = context.Request["OptType3"];
            quesModel.OptValue3 = context.Request["OptValue3"];

            quesModel.OptText4 = context.Request["OptText4"];
            quesModel.OptType4 = context.Request["OptType4"];
            quesModel.OptValue4 = context.Request["OptValue4"];
        }

        /// <summary>
        /// 删除答案
        /// </summary>
        /// <param name="id"></param>
        /// <param name="context"></param>
        private void DelAnswer(string id, HttpContext context)
        {
            var quesBll = new BLL.QuestionnaireSurvey();
            var quesModel = quesBll.GetModel(new Guid(id));
            quesModel.LastModifyTime = DateTime.Now;

            var result = false;
            var msg = "";

            try
            {
                quesModel.ShowCode = "";
                quesModel.ShowId = "";
                quesModel.OptText1 = "";
                quesModel.OptType1 = "";
                quesModel.OptValue1 = "";

                quesModel.OptText2 = "";
                quesModel.OptType2 = "";
                quesModel.OptValue2 = "";

                quesModel.OptText3 = "";
                quesModel.OptType3 = "";
                quesModel.OptValue3 = "";

                quesModel.OptText4 = "";
                quesModel.OptType4 = "";
                quesModel.OptValue4 = "";

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
        

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}