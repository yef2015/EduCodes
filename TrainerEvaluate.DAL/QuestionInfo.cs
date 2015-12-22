using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.DAL
{
    public partial class QuestionInfo
    {

        public bool Exists(Guid Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from QuestionInfo");
            strSql.Append(" where ");
            strSql.Append(" Id = @Id  ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(TrainerEvaluate.Models.QuestionInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into QuestionInfo(");
            strSql.Append("Id,Name,ClassCourseID,Status,CreatedTime");
            strSql.Append(") values (");
            strSql.Append("@Id,@Name,@ClassCourseID,@Status,@CreatedTime");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16) ,            
                        new SqlParameter("@Name", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ClassCourseID", SqlDbType.UniqueIdentifier,16) ,            
                        new SqlParameter("@Status", SqlDbType.Int,4) ,            
                        new SqlParameter("@CreatedTime", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = Guid.NewGuid();
            parameters[1].Value = model.Name;
            parameters[2].Value = Guid.NewGuid();
            parameters[3].Value = model.Status;
            parameters[4].Value = model.CreatedTime;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(TrainerEvaluate.Models.QuestionInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update QuestionInfo set ");

            strSql.Append(" Id = @Id , ");
            strSql.Append(" Name = @Name , ");
            strSql.Append(" ClassCourseID = @ClassCourseID , ");
            strSql.Append(" Status = @Status , ");
            strSql.Append(" CreatedTime = @CreatedTime  ");
            strSql.Append(" where Id=@Id  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16) ,            
                        new SqlParameter("@Name", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ClassCourseID", SqlDbType.UniqueIdentifier,16) ,            
                        new SqlParameter("@Status", SqlDbType.Int,4) ,            
                        new SqlParameter("@CreatedTime", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.ClassCourseID;
            parameters[3].Value = model.Status;
            parameters[4].Value = model.CreatedTime;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(Guid Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from QuestionInfo ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = Id;


            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TrainerEvaluate.Models.QuestionInfo GetModel(Guid Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, Name, ClassCourseID, Status, CreatedTime  ");
            strSql.Append("  from QuestionInfo ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = Id;


            TrainerEvaluate.Models.QuestionInfo model = new TrainerEvaluate.Models.QuestionInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = new Guid(ds.Tables[0].Rows[0]["Id"].ToString()); 
                }
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                if (ds.Tables[0].Rows[0]["ClassCourseID"].ToString() != "")
                {
                    model.ClassCourseID = new Guid(ds.Tables[0].Rows[0]["ClassCourseID"].ToString()); 
                }
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedTime"].ToString() != "")
                {
                    model.CreatedTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedTime"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM QuestionInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM QuestionInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }




        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM QuestionInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.Id desc");
            }
            strSql.Append(")AS Row, T.*  from QuestionInfo T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "QuestionInfo";
            parameters[1].Value = "Id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/


    }
}
