using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.Management;
using TrainerEvaluate.Utility;
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.BLL
{
    /// <summary>
    /// 问卷管理
    /// </summary>
    public partial class Student
    {
        private readonly DAL.Student dal = new DAL.Student();
        public Student()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(Guid StudentId)
        {
            return dal.Exists(StudentId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Models.Student model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Models.Student model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(Guid StudentId)
        {

            return dal.Delete(StudentId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string StudentIdlist)
        {
            return dal.DeleteList(StudentIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Models.Student GetModel(Guid StudentId)
        {

            return dal.GetModel(StudentId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        //public Models.Student GetModelByCache(Guid StudentId)
        //{

        //    string CacheKey = "StudentModel-" + StudentId;
        //    object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
        //    if (objModel == null)
        //    {
        //        try
        //        {
        //            objModel = dal.GetModel(StudentId);
        //            if (objModel != null)
        //            {
        //                int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
        //                Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
        //            }
        //        }
        //        catch{}
        //    }
        //    return (TrainerEvaluate.Model.TrainerEvaluate.Student)objModel;
        //}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Models.Student> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Models.Student> DataTableToList(DataTable dt)
        {
            List<Models.Student> modelList = new List<Models.Student>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Models.Student model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string sort, int startIndex, int endIndex, string order)
        {
            return dal.GetListByPage(strWhere, sort, startIndex, endIndex, order);
        }

        public DataSet GetStudentInfo(string strWhere, string sort, int startIndex, int endIndex, string order)
        {
            return dal.GetStudentInfo(strWhere, sort, startIndex, endIndex, order);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod

        #region  ExtensionMethod


        /// <summary>
        /// 取消报名
        /// </summary>
        /// <returns></returns>
        public string CancelReport(string userId,string classId)
        {
            var cobj = new BLL.Class();
            var obj = cobj.GetModel(Convert.ToInt32(classId));
            if (obj != null)
            {
                if (obj.CloseDate < DateTime.Now) //已经结束的不允许取消
                {
                    return "报名已经结束,不允许取消!";
                }
                else
                {
                    var sqllist = new List<string>();
                    var sql1 = string.Format("delete from  ClassStudents where ClassId='{0}' and StudentId='{1}'  ",classId,userId);
                    var sql2 = string.Format("update Class set HasReportNum=HasReportNum-1 where  ID='{0}'  ", classId);
 
                    sqllist.Add(sql1);
                    sqllist.Add(sql2);

                    DbHelperSQL.ExecuteSqlTran(sqllist);
                    return ""; 
                }  
            }
            else
            {
                return "参数错误！";
            } 
        }





        public string GetStuAccount()
        {
            try
            {
                var sql = string.Format(" select COUNT(*) num from  SysUser where UserRole=1 and YEAR(CreateTime)=YEAR(GETDATE())   ");
                var result = DbHelperSQL.Query(sql);
                var num = 0;
                if (result != null && result.Tables.Count > 0)
                {
                    num = Convert.ToInt32(result.Tables[0].Rows[0]["num"]) + 1;
                }

                var userAccount = "HB" + DateTime.Now.Year + num.ToString().PadLeft(3, '0');


                return userAccount;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return "";
            }
        }

        public string GetPwd()
        {
            var pwd = new Random();
            return pwd.Next(999999).ToString();
        }

        public DataSet GetDataForExport(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select StudentId,IdentityNo,StuName,Gender,School,Title,TelNo,CreateTime,LastModifyTime ");
            //strSql.Append("select StuName,case Gender when 1 then '男' else '女' end as GenderName,IdentityNo,School,JobTitle,TelNo, Birthday, Nation, FirstRecord, FirstSchool, LastRecord,LastSchool,PoliticsStatus, Rank, RankTime, Post, PostTime, Mobile, TeachNo, Status, Description ");
            //strSql.Append(" FROM Student ");

            strSql.Append(" select StuName,b.Name as GenderName,IdentityNo,School,c.Name as JobTitleName,TelNo, Birthday, d.Name as NationName, FirstRecord, FirstSchool, LastRecord,LastSchool,e.Name as PoliticsStatusName, Rank, RankTime, Post+'  '+PostOptName, PostTime, Mobile, TeachNo, ManageWork, Description    ");
            strSql.Append(" from Student a left join Dictionaries b  on  a.Gender=b.ID left join Dictionaries c on  a.JobTitle=c.ID  left join Dictionaries d on a.Nation=d.ID  left join Dictionaries e on  a.PoliticsStatus=e.ID  ");
            strSql.Append(" where a.Status=1   ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            strSql.Append(" order by a.StuName asc, a.CreateTime desc ");
            return DbHelperSQL.Query(strSql.ToString());
        }


        public DataSet GetDataForExportByClassId(string classId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" select StuName,b.Name as GenderName,IdentityNo,School,c.Name as JobTitleName,TelNo, Birthday, d.Name as NationName, FirstRecord, FirstSchool, LastRecord,LastSchool,e.Name as PoliticsStatusName, Rank, RankTime, Post+'  '+PostOptName, PostTime, Mobile, TeachNo,  Description,ManageWork    ");
            strSql.Append(" from Student a left join Dictionaries b  on  a.Gender=b.ID left join Dictionaries c on  a.JobTitle=c.ID  left join Dictionaries d on a.Nation=d.ID  left join Dictionaries e on  a.PoliticsStatus=e.ID  ");
            strSql.Append(" left join ClassStudents f on a.StudentId = f.StudentId  ");
            strSql.Append(" where a.Status=1 and f.ClassId = '"+classId+"'  ");
            strSql.Append(" order by a.StuName asc, a.CreateTime desc ");
            return DbHelperSQL.Query(strSql.ToString());
        }



        public DataSet GetClassStuListByPage(string classId, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby + " desc, " + "T.StuName  asc ");
            }
            else
            {
                strSql.Append("order by T.StuName asc");
            }
            strSql.Append(")AS Row, T.*  from   ");
            strSql.Append(" ( select a.*,case ISNULL(b.RId,'00000000-0000-0000-0000-000000000000')  when '00000000-0000-0000-0000-000000000000' then  0  else 1 end ck ");
            strSql.Append(string.Format("  from Student a left join  ClassStudents b on a.StudentId=b.StudentId and b.ClassId={0}  where  a.Status=1  )  ", classId));
            strSql.Append("  T ");
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetClassStuListByPageCondition(string classId,string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby + " desc, " + "T.StuName  asc ");
            }
            else
            {
                strSql.Append("order by T.StuName asc");
            }
            strSql.Append(")AS Row, T.*  from   ");
            strSql.Append(" ( select a.*,case ISNULL(b.RId,'00000000-0000-0000-0000-000000000000')  when '00000000-0000-0000-0000-000000000000' then  0  else 1 end ck ");
            strSql.Append(string.Format("  from Student a left join  ClassStudents b on a.StudentId=b.StudentId and b.ClassId={0}  where {1}  )  ", classId, strWhere));
            strSql.Append("  T ");
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }


        public bool SaveClassStuForAll(string classId)
        {
            try
            {
                if (!string.IsNullOrEmpty(classId))
                {
                    var lstSql = new List<string>();
                    lstSql.Add(string.Format(" delete from  ClassStudents  where ClassId={0} ", classId));
                    var sql1 = string.Format("  insert into ClassStudents select NEWID(),  StudentId,{0} from Student  where Status=1 ", classId);
                    lstSql.Add(sql1);
                    lstSql.Add(string.Format(" UPDATE Class set Students=(select count(*) from Student  where Status=1 ) where ID={1} ", 0, classId));
                    DbHelperSQL.ExecuteSqlTran(lstSql);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return false;
            }
        }




        public bool DeletClassStuForAll(string classId)
        {
            try
            {
                var strs = new List<string>();
                var sql = string.Format(" delete from  ClassStudents  where ClassId={0}  ", classId);
                strs.Add(sql);
                strs.Add(string.Format(" UPDATE Class set Students={0} where ID={1} ", 0, classId));
                DbHelperSQL.ExecuteSqlTran(strs);
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return false;
            }
        }








        public bool SaveClassStu(string[] stuIds, string classId)
        {
            try
            {
                if (!string.IsNullOrEmpty(classId))
                {
                    if (stuIds != null && stuIds.Length > 0)
                    {
                        var lstSql = new List<string>();
                        lstSql.Add(string.Format(" delete from  ClassStudents  where ClassId='{0}' ", classId));
                        foreach (var stuId in stuIds)
                        {
                            if (!string.IsNullOrEmpty(stuId))
                            {
                                var sql1 =
                                    string.Format(
                                        "insert into ClassStudents values(NEWID(),'{0}',{1})  ", stuId, classId
                                        );
                                lstSql.Add(sql1);
                            }
                        }
                        lstSql.Add(string.Format(" UPDATE Class set Students={0} where ID={1} ", stuIds.Length, classId));
                        DbHelperSQL.ExecuteSqlTran(lstSql);
                    }
                    else
                    {
                        var lstSql = new List<string>();
                        lstSql.Add(string.Format(" delete from  ClassStudents  where ClassId='{0}' ", classId));
                        lstSql.Add(string.Format(" UPDATE Class set Students={0} where ID={1} ", 0, classId));
                        DbHelperSQL.ExecuteSqlTran(lstSql);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
                return false;
            }
        }

        /// <summary>
        /// 通过身份证号，获取studentid号
        /// </summary>
        /// <param name="identityNo"></param>
        /// <returns></returns>
        public static string GetStudentIdByIdentityNo(string identityNo)
        {
            var studentId = "";
            try
            {
                var sql = string.Format(" select StudentId from Student  where Status=1 and IdentityNo='{0}' ", identityNo);
                studentId = DbHelperSQL.GetSingle(sql).ToString();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogofExceptioin(ex);
            }
            return string.IsNullOrEmpty(studentId) ? "null" : studentId;
        }


        public DataSet GetStudentTree()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" select * from( ");
            strSql.Append(" select SchDisId as id ,SchDisName as name,'open' as state, ");
            strSql.Append(" '00000000-0000-0000-0000-000000000000' as parentid ,CreatedDate as time ");
            strSql.Append("  from dbo.SchoolDistrict where Status = 1 ");
            strSql.Append(" union all ");
            strSql.Append(" select SchoolId as id,SchoolName as name,'open' as state,  ");
            strSql.Append(" SchDisId as parentid ,CreatedDate as time ");
            strSql.Append(" from School where Status = 1 and SchDisId <> ''  ");
            strSql.Append(" ) TT  ");
            strSql.Append(" order by time asc ");

            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  ExtensionMethod
    }

     public class EasyUITree
     {
         public List<JsonTree> initTree(DataTable dt)
         {
             DataRow[] drList = dt.Select("parentid='00000000-0000-0000-0000-000000000000'");
             List<JsonTree> rootNode = new List<JsonTree>();
             foreach (DataRow dr in drList)
             {
                 JsonTree jt = new JsonTree();
                 jt.id = dr["id"].ToString();
                 jt.text = dr["name"].ToString();
                 jt.state = dr["state"].ToString();
                 //jt.attributes = CreateUrl(dt, jt);
                 jt.children = CreateChildTree(dt, jt);
                 rootNode.Add(jt);
             }
             return rootNode;
         }
 
         private List<JsonTree> CreateChildTree(DataTable dt, JsonTree jt)
         {
             string keyid = jt.id;                                        //根节点ID
             List<JsonTree> nodeList = new List<JsonTree>();
             DataRow[] children = dt.Select("Parentid='" + keyid + "'");
             foreach (DataRow dr in children)
             {
                 JsonTree node = new JsonTree();
                 node.id = dr["id"].ToString();
                 node.text = dr["name"].ToString();
                 node.state = dr["state"].ToString();
                 //node.attributes = CreateUrl(dt, node);
                 node.children = CreateChildTree(dt, node);
                 nodeList.Add(node);
             }
             return nodeList;
         }
 
 　　　　
         private Dictionary<string, string> CreateUrl(DataTable dt, JsonTree jt)    //把Url属性添加到attribute中，如果需要别的属性，也可以在这里添加
         {
             Dictionary<string, string> dic = new Dictionary<string, string>();
             string keyid = jt.id;
             DataRow[] urlList = dt.Select("id='" + keyid + "'");
             string url = urlList[0]["Url"].ToString();
             dic.Add("url", url);
             return dic;
         }
     }

    public class JsonTree
     {
         private string _id;
         private string _text;
         private string _state="open";
         private Dictionary<string, string> _attributes=new Dictionary<string, string>();
         private object _children;        
         
         public string id
         {
             get { return _id; }
             set { _id = value; }
         }
         public string text
         {
             get { return _text; }
             set { _text = value; }
         }
         public string state
         {
             get { return _state; }
             set { _state = value; }
         }
         public Dictionary<string, string> attributes
         {
             get { return _attributes; }
             set { _attributes = value; }
         }
         public object children
         {
             get { return _children; }
             set { _children = value; }
         }
     }


}

