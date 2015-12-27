using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using TrainerEvaluate.Utility;
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.Web
{
    /// <summary>
    /// Roles1 的摘要说明
    /// </summary>
    public class Roles1 : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var opType = context.Request["t"];
            var id = context.Request["id"];
            switch (opType)
            {
                case "add":
                    Add(context);
                    break;
                case "edit":
                    Edit(id, context);
                    break;
                case "del":
                    Del(id, context);
                    break;
                case "q":
                    Query(context);
                    break;  
                case "gr":
                    GetFuncbyRole(context);
                    break;
                case "gu":
                    GetUserbyRole(context);
                    break;  
                case "sf":
                    SaveFuncRole(context);
                    break;   
                case "su":
                    SaveUserRole(context);
                    break;   
                case "g":
                    GetdataForCombobox(context);
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
            var rolesBll = new BLL.Roles();
            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "Name" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "asc" : context.Request["order"];


            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = rolesBll.GetRecordCount(" Rstatus !=0 ");
            ds = rolesBll.GetListByPage(" Rstatus = 1  ", sort, startIndex, endIndex, order);
            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            return str;
        }

        private string GetdataForCombobox(HttpContext context)
        {
            var rolesBll = new BLL.Roles();
            var dt = rolesBll.GetDataForCombox();
            var str = JsonConvert.SerializeObject(dt);
            return str;
        }



        private void SetModelValue(Models.Roles roleModel, HttpContext context)
        {
            roleModel.Name = context.Request["Name"];
            roleModel.Rstatus = Convert.ToInt32(context.Request["Rstatus"]);
            roleModel.Description = context.Request["Description"];
        }

        private void Add(HttpContext context)
        {
            var roleModel = new Models.Roles();
            roleModel.ID = Guid.NewGuid();
            //roleModel.CreateId = Profile.CurrentUser.UserId;
            //roleModel.CreateName = Profile.CurrentUser.UserName;
            roleModel.CreateTime = System.DateTime.Now;
            SetModelValue(roleModel, context);

            var roleBll = new BLL.Roles();
            var result = false;
            var msg = "";
            try
            {
                result = roleBll.Add(roleModel);
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


        private void Edit(string id, HttpContext context)
        {
            var roleBll = new BLL.Roles();
            var roleModel = roleBll.GetModel(new Guid(id));

            //roleModel.LastModifyId = Profile.CurrentUser.UserId;
            //roleModel.LastModifyName = Profile.CurrentUser.UserName;
            roleModel.LastModifyTime = System.DateTime.Now;
            SetModelValue(roleModel, context);

            var result = false;
            var msg = "";
            try
            {
                result = roleBll.Update(roleModel);
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



        private void Del(string id, HttpContext context)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var result = false;
                var msg = "";
                var roleBll = new BLL.Roles();
                try
                {
                    var roleModel = roleBll.GetModel(new Guid(id));

                    roleModel.LastModifyTime = System.DateTime.Now;
                    roleModel.Rstatus = 2;
                    result = roleBll.Update(roleModel);
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
        }


        private void GetFuncbyRole(HttpContext context)
        { 
            var ds = new DataSet();
            var roleId = context.Request["rId"];
            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var roleBll = new BLL.Roles(); 
            ds = roleBll.GetRoleFuncListByPage(roleId, "ck", startIndex, endIndex);
            var num = 0;
            if (ds != null && ds.Tables.Count > 0)
            {
                num = ds.Tables[0].Rows.Count;
            }  
            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            context.Response.Write(str);
        }

       
        private void GetUserbyRole(HttpContext context)
        { 
            var ds = new DataSet();
            var roleId = context.Request["rId"];
            var roleName = context.Request["rName"];
            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var roleBll = new BLL.Roles();
            ds = roleBll.GetRoleUserListByPage(roleId,roleName, "ck", startIndex, endIndex);
            var num = 0;
            if (ds != null && ds.Tables.Count > 0)
            {
                num = ds.Tables[0].Rows.Count;
            }  
            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            context.Response.Write(str);
        }
        


        private void Query(HttpContext context)
        {
            var name = string.IsNullOrEmpty(context.Request["Name"]) ? "" : context.Request["Name"].Trim();
            var rstatus = string.IsNullOrEmpty(context.Request["Rstatus"]) ? "" : context.Request["Rstatus"].Trim();
            var description = string.IsNullOrEmpty(context.Request["Description"]) ? "" : context.Request["Description"].Trim();
            var createTime = string.IsNullOrEmpty(context.Request["CreateTime"]) ? "" : context.Request["CreateTime"].Trim();
            var endTime = string.IsNullOrEmpty(context.Request["EndTime"]) ? "" : context.Request["EndTime"].Trim();

            var ds = new DataSet();
            var roleBll = new BLL.Roles();
            var strWhere = "";
            if (!string.IsNullOrEmpty(name))
            {
                strWhere = string.Format(" Name like '%" + name + "%' ");
            }
            if (!string.IsNullOrEmpty(rstatus))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  Rstatus like '%" + rstatus + "%' ");
                }
                else
                {
                    strWhere = string.Format(" Rstatus  like '%" + rstatus + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(description))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  Description  like '%" + description + "%' ");
                }
                else
                {
                    strWhere = string.Format(" Description  like '%" + description + "%' ");
                }
            }
            if (!string.IsNullOrEmpty(createTime))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  CreateTime  >= '" + createTime + "' ");
                }
                else
                {
                    strWhere = string.Format(" CreateTime  >= '" + createTime + "' ");
                }
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strWhere += string.Format(" and  CreateTime  <= '" + endTime + "' ");
                }
                else
                {
                    strWhere = string.Format(" CreateTime  <= '" + endTime + "' ");
                }
            }   

            var page = Convert.ToInt32(context.Request["page"]);
            var rows = Convert.ToInt32(context.Request["rows"]);
            var sort = string.IsNullOrEmpty(context.Request["sort"]) ? "Name" : context.Request["sort"];
            var order = string.IsNullOrEmpty(context.Request["order"]) ? "asc" : context.Request["order"];

            var startIndex = (page - 1) * rows + 1;
            var endIndex = startIndex + rows - 1;

            var num = roleBll.GetRecordCount(strWhere);
            ds = roleBll.GetListByPage(strWhere, sort, startIndex, endIndex, order);

            var str = JsonConvert.SerializeObject(new { total = num, rows = ds.Tables[0] });
            context.Response.Write(str);

        }




        private void SaveFuncRole(HttpContext context)
        { 
            var msg = "";
            var roleId = context.Request["RoleId"];
            var isAll = context.Request["IsAll"];
            if (!string.IsNullOrEmpty(isAll)) //全选
            {
                if (isAll == "1")
                {
                    var roleBll = new BLL.Roles();
                    var result = roleBll.SaveAllFunc(roleId);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
                else //全不选
                {
                    var roleBll = new BLL.Roles();
                    var result = roleBll.DeletRoleFuncbyFuncId(roleId);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
            }
            else
            {
                var funcids = context.Request["FuncIds"];
                var unfuncids = context.Request["UnFuncIds"];
                if (!string.IsNullOrEmpty(funcids))
                {
                    if (!string.IsNullOrEmpty(unfuncids))
                    {
                        var unid = unfuncids.Split('|');
                        if (unid.Length > 0)
                        {
                            foreach (var uid in unid)
                            {
                                if (!string.IsNullOrEmpty(uid))
                                {
                                    funcids = funcids.Replace(uid, "");
                                }
                            }
                        }
                    }
                    var func = funcids.Split('|');
                    var funcList = new List<string>();
                    foreach (var sid in func)
                    {
                        if (!string.IsNullOrEmpty(sid))
                        {
                            if (!funcList.Contains(sid))
                            {
                                funcList.Add(sid);
                            }
                        }
                    }
                    var roleBll = new BLL.Roles();
                    var result = roleBll.SaveChooseFunc(funcList.ToArray(), roleId);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
                else
                {
                    var roleBll = new BLL.Roles();
                    var result = roleBll.SaveChooseFunc(null, roleId);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
            }
            context.Response.Write(msg); 
        }





        private void SaveUserRole(HttpContext context)
        {
            var msg = "";
            var roleId = context.Request["RoleId"];
            var isAll = context.Request["IsAll"];
            if (!string.IsNullOrEmpty(isAll)) //全选
            {
                if (isAll == "1")
                {
                    var roleBll = new BLL.Roles();
                    var result = roleBll.SaveAllUser(roleId);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
                else //全不选
                {
                    var roleBll = new BLL.Roles();
                    var result = roleBll.DeletRoleUserbyRoleId(roleId);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
            }
            else
            {  
                var userids = context.Request["UserIds"];
                var unuserids = context.Request["UnUserIds"];
                if (!string.IsNullOrEmpty(userids))
                {
                    if (!string.IsNullOrEmpty(unuserids))
                    {
                        var unid = unuserids.Split('|');
                        if (unid.Length > 0)
                        {
                            foreach (var uid in unid)
                            {
                                if (!string.IsNullOrEmpty(uid))
                                {
                                    userids = userids.Replace(uid, "");
                                }
                            }
                        }
                    }
                    var user = userids.Split('|');
                    var userList = new List<string>();
                    foreach (var sid in user)
                    {
                        if (!string.IsNullOrEmpty(sid))
                        {
                            if (!userList.Contains(sid))
                            {
                                userList.Add(sid);
                            }
                        }
                    }
                    var roleBll = new BLL.Roles();
                    var result = roleBll.SaveChooseUser(userList.ToArray(), roleId);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
                else
                {
                    var roleBll = new BLL.Roles();
                    var result = roleBll.SaveChooseUser(null, roleId);
                    if (!result)
                    {
                        msg = "保存失败！";
                    }
                }
            }
            context.Response.Write(msg);
        }


    }
}