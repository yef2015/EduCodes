using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TrainerEvaluate.Utility;

namespace TrainerEvaluate.Web
{
    /// <summary>
    /// GetUserInfo 的摘要说明
    /// </summary>
    public class GetUserInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var result = "";
            if (context.Request["t"] == "n")
            {
                result = GetUser(context.Request["uid"]);
            }
            else if (context.Request["t"] == "e")
            {
                result = ModifyUser(context);
            }
            context.Response.Write(result);
        }


        private string ModifyUser(HttpContext context)
        {
            try
            {
                var result = "";
                var userId = new Guid(context.Request["uid"]);
                var userBll = new BLL.SysUser();

                var beforeUserAccount = string.Empty;
                var UserAccount = context.Request["UserAccount"];
                bool isFlag = false;

                if (!string.IsNullOrEmpty(context.Request["BeforeUserAccount"]))
                {
                    beforeUserAccount = context.Request["BeforeUserAccount"];

                    if (beforeUserAccount != UserAccount)
                    {
                        isFlag = userBll.ExistsAccountByAC(UserAccount);
                    }
                }

                if (isFlag)
                {
                    return "该账号已存在！";
                }

                //if (userBll.GetAccountExsist(context.Request["UserAccount"], userId))  //登录账户不能重复
                //{
                //    return "该账号已存在！";
                //}

                var userModel = userBll.GetModel(userId);

                if (userModel.UserRole == (int)EnumUserRole.Student)
                {
                    var stubll = new BLL.Student();
                    var stuModel = stubll.GetModel(userModel.UserId);

                    if (!string.IsNullOrEmpty(context.Request["Gender"]))
                    {
                        stuModel.Gender = Convert.ToInt32(context.Request["Gender"]);
                    }
                    stuModel.TelNo = context.Request["TelNo"];
                    if (!string.IsNullOrEmpty(context.Request["Title"]))
                    {
                        stuModel.JobTitle = Convert.ToInt32(context.Request["Title"]);
                    } 
                    stuModel.StuName = context.Request["UserName"];
                    stuModel.School = context.Request["School"];
                    stuModel.LastModifyTime = DateTime.Now;
                    if (!string.IsNullOrEmpty(context.Request["IdentityNo"]))
                    {
                        stuModel.IdentityNo = context.Request["IdentityNo"];
                    }
                    stubll.Update(stuModel);


                    userModel.UserAccount = context.Request["UserAccount"];
                    userModel.UserName = context.Request["UserName"];
                    userModel.UserPassWord = context.Request["UserPassWord"];
                    userModel.CreateTime = DateTime.Now;
                    userBll.Update(userModel);
                }
                else
                {
                    userModel.UserAccount = context.Request["UserAccount"];
                    userModel.UserName = context.Request["UserName"];  
                    userModel.UserPassWord = context.Request["UserPassWord"];
                    userModel.Dept = context.Request["Gender"];
                    userModel.CreateTime = DateTime.Now;
                    userBll.Update(userModel);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return ex.Message;
            }
        }





        private string GetUser(string userId)
        {
            try
            {
                var result = new StringBuilder("");
                var userBll = new BLL.SysUser();
                var userInfo = userBll.GetModel(new Guid(userId));

                var stuBll = new BLL.Student();
                var stuInfo = stuBll.GetModel(new Guid(userId));

                if (userInfo != null)
                {
                    result.Append("UserName:" + userInfo.UserName);
                    result.Append(",UserAccount:" + userInfo.UserAccount);
                    result.Append(",UserPassWord:" + userInfo.UserPassWord);
                    result.Append(",UserRole:" + userInfo.UserRole);
                    result.Append(",Gender:" + userInfo.Dept);

                }
                if (stuInfo != null)
                {
                    // result.Append(",UserName:" + stuInfo.StuName);
                    result.Append(",Gender:" + stuInfo.Gender);
                    result.Append(",IdentityNo:" + stuInfo.IdentityNo);
                    result.Append(",School:" + stuInfo.School);
                    result.Append(",TelNo:" + stuInfo.TelNo);
                    result.Append(",Title:" + stuInfo.JobTitle);
                }
                result.Append("");
                return result.ToString();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return string.Empty;
            }
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