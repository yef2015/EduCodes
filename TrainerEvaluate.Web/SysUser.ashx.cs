using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using TrainerEvaluate.Utility;

namespace TrainerEvaluate.Web
{
    /// <summary>
    /// SysUser 的摘要说明
    /// </summary>
    public class SysUser : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var opType = context.Request["t"];
            var id = context.Request["id"];
            switch (opType)
            {
                case "n":
                    AddData(context);
                    break;
                case "e":
                    EditData(id, context);
                    break;
                case "d":
                    DelData(id, context);
                    break;
                case "q":
                    Query(context);
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


        private string GetData(HttpContext context)
        {
            var ds = new DataSet();
            var sysUserBll = new BLL.SysUser();

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "UserName" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "asc" : context.Request["order"];
             
            var name = context.Request["Name"];
            var dept = context.Request["Dept"];

            var strWhere = "";
            if (!string.IsNullOrEmpty(name))
            {
                strWhere = string.Format(" UserName like '%" + name + "%' ");
            }
            if (!string.IsNullOrEmpty(dept))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  Dept like '%" + dept + "%' ");
                }
                else
                {
                    strWhere = string.Format(" Dept like '%" + dept + "%' ");
                }
            } 
            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = sysUserBll.GetRecordCount(strWhere);
            ds = sysUserBll.GetListByPage(strWhere, sort, startIndex, endIndex, order); 

            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            return str;
        }


        private void Query(HttpContext context)
        {
            var stuName = context.Request["Name"];
            var dept = context.Request["Dept"];
            var ds = new DataSet();
            var sysUserBll = new BLL.SysUser();
            var strWhere = "";
            if (!string.IsNullOrEmpty(stuName))
            {
                strWhere = string.Format(" UserName like '%" + stuName + "%' ");
            }
            if (!string.IsNullOrEmpty(dept))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  Dept like '%" + dept + "%' ");
                }
                else
                {
                    strWhere = string.Format(" Dept like '%" + dept + "%' ");
                }
            } 
            //  ds = sysUserBll.GetList(strWhere);


            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "UserName" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "asc" : context.Request["order"];
             

            var num = sysUserBll.GetRecordCount(strWhere);
            ds = sysUserBll.GetListByPage(strWhere, sort, startIndex, endIndex, order);


            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            context.Response.Write(str);
        }




        private void AddData(HttpContext context)
        {
            var sysUserModel = new Models.SysUser();
            sysUserModel.UserId = Guid.NewGuid();
            SetModelValue(sysUserModel, context);
            sysUserModel.CreateTime = DateTime.Now;

            var sysUserBll = new BLL.SysUser();
            var result = false;
            var msg = "";
            try
            {
                if (sysUserBll.GetAccountExsist(sysUserModel.UserAccount, sysUserModel.UserId))
                {
                    msg = "该账号已存在，请修改！";
                }
                else
                {
                    result = sysUserBll.Add(sysUserModel);

                    if (!result)
                    {
                        msg = "保存失败！";
                    }
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




        private void EditData(string id, HttpContext context)
        {
            var sysUserBll = new BLL.SysUser();
            var sysUserModel = sysUserBll.GetModel(new Guid(id));
            //   sysUserModel.LastModifyTime = DateTime.Now;
            var result = false;
            var msg = "";
            try
            {
                SetModelValue(sysUserModel, context);
                if (sysUserBll.GetAccountExsist(sysUserModel.UserAccount, sysUserModel.UserId))
                {
                    msg = "该账号已存在，请修改！";
                }
                else
                {
                    result = sysUserBll.Update(sysUserModel);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
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




        private void SetModelValue(Models.SysUser sysUserModel, HttpContext context)
        {
            sysUserModel.UserName = context.Request["UserName"];
            sysUserModel.UserAccount = context.Request["UserAccount"];
            sysUserModel.UserRole = 0;
            sysUserModel.Dept = context.Request["Dept"];
            //courModel.TeacherId = new Guid(context.Request["TeacherId"]);
            sysUserModel.UserPassWord = context.Request["UserPassWord"];
        }



        private void DelData(string id, HttpContext context)
        {
            var sysUserBll = new BLL.SysUser();
            var result = false;
            var msg = "";
            try
            {
                result = sysUserBll.Delete(new Guid(id));

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