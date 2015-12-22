using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using TrainerEvaluate.Utility;

namespace TrainerEvaluate.Web 
{
    public class BasePage : Page
    {
        private const string NoAuthUrl = "~/Login.aspx";
        private const string ErrorUrl = "~/ErrorForm.aspx";


        protected override void OnInit(EventArgs e)
        {
            Authentication();
            base.OnInit(e);
        }


        protected override void OnError(EventArgs e)
        {
            base.OnError(e);
            if (e.ToString().Equals("请重新登录"))
            {
                Response.Redirect(NoAuthUrl);
            }
            else
            {   
                Response.Redirect(ErrorUrl);
            }
        }



        private void Authentication()
        {
            if (!Profile.IsLoad)  //无用户认证信息。
            {
                Response.Redirect(NoAuthUrl);
            }

        }

    }






    public static class Profile
    {
        private const string SESSION_NAME = "ProfileInfo";


        public static bool IsLoad
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_NAME] != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }




        public static void LogOut()
        {
            HttpContext.Current.Session.Clear();
        }



        /// <summary>
        /// 当前用户信息
        /// </summary> 
        public static Models.SysUser CurrentUser
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_NAME] != null)
                {
                    return (Models.SysUser)HttpContext.Current.Session[SESSION_NAME];
                }
                else
                {
                    throw new Exception("请重新登录");

                }
            }
        }


        /// <summary>
        /// 根据用户Id获取用户信息，登录测试页面用。
        /// </summary>
        /// <param name="userAccount"></param>
        /// <param name="pwd"></param>
        public static bool Load(string userAccount, string pwd)
        {
            try
            {
                if ((!string.IsNullOrEmpty(userAccount)) && (!string.IsNullOrEmpty(pwd)))
                {
                    var sysUserBll = new BLL.SysUser();
                    var sysUser = sysUserBll.GetSysUserByAccount(userAccount, pwd);
                    if (sysUser != null)
                    {

                        HttpContext.Current.Session.Add(SESSION_NAME, sysUser);
                        //   HttpContext.Current.Session[SESSION_NAME] = sysUser;
                        return true;
                    }
                    else // 该用户在本系统中不存在
                    {
                        throw new Exception("不存在该用户信息，请核对！");
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return false;
            }
        }  
    }
}