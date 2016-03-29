using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TrainerEvaluate.Web
{
    public partial class TeacherManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 是否显示删除按钮，项目负责人的权限要增加，对其负责的项目有以下权限 教师管理：无“删除”权限，其他开放
        /// </summary>
        public string IsDel
        {
            get
            {
                if (Profile.CurrentUser.UserRole == 1 || Profile.CurrentUser.UserRole == 2)
                {
                    return "none";
                }
                else
                {
                    var role = new BLL.Roles();
                     var result= role.GetCurrentUserIsCharge(Profile.CurrentUser.UserId);
                     return result ? "none" : "block";
                }
            }
        }




    }
}