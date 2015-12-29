using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

//Please add references
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.DAL
{
	/// <summary>
	/// 数据访问类:问卷试题
	/// </summary>
	public partial class QuestionnaireSurvey
    {
        public QuestionnaireSurvey()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(Guid Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from QuestionnaireSurvey");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Models.QuestionnaireSurvey model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into QuestionnaireSurvey(");
            strSql.Append("Id,ShowName,ShowType,ShowOrder,ShowCode,ShowId,OptText1,OptType1,OptValue1,OptText2,OptType2,OptValue2,OptText3,OptType3,OptValue3,OptText4,OptType4,OptValue4,TreeNode,CreateTime,IsDelete)");
            strSql.Append(" values (");
            strSql.Append("@Id,@ShowName,@ShowType,@ShowOrder,@ShowCode,@ShowId,@OptText1,@OptType1,@OptValue1,@OptText2,@OptType2,@OptValue2,@OptText3,@OptType3,@OptValue3,@OptText4,@OptType4,@OptValue4,@TreeNode,@CreateTime,@IsDelete)");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ShowName", SqlDbType.NVarChar,50),
					new SqlParameter("@ShowType", SqlDbType.NVarChar,50),
					new SqlParameter("@ShowOrder", SqlDbType.NVarChar,50),
					new SqlParameter("@ShowCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ShowId", SqlDbType.NVarChar,50),
					new SqlParameter("@OptText1", SqlDbType.NVarChar,50),
					new SqlParameter("@OptType1", SqlDbType.NVarChar,50),
					new SqlParameter("@OptValue1", SqlDbType.NVarChar,50),
					new SqlParameter("@OptText2", SqlDbType.NVarChar,50),
					new SqlParameter("@OptType2", SqlDbType.NVarChar,50),
					new SqlParameter("@OptValue2", SqlDbType.NVarChar,50),
					new SqlParameter("@OptText3", SqlDbType.NVarChar,50),
					new SqlParameter("@OptType3", SqlDbType.NVarChar,50),
					new SqlParameter("@OptValue3", SqlDbType.NVarChar,50),
					new SqlParameter("@OptText4", SqlDbType.NVarChar,50),
					new SqlParameter("@OptType4", SqlDbType.NVarChar,50),
					new SqlParameter("@OptValue4", SqlDbType.NVarChar,50),
					new SqlParameter("@TreeNode", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.Bit,1)};
            parameters[0].Value = Guid.NewGuid();
            parameters[1].Value = model.ShowName;
            parameters[2].Value = model.ShowType;
            parameters[3].Value = model.ShowOrder;
            parameters[4].Value = model.ShowCode;
            parameters[5].Value = model.ShowId;
            parameters[6].Value = model.OptText1;
            parameters[7].Value = model.OptType1;
            parameters[8].Value = model.OptValue1;
            parameters[9].Value = model.OptText2;
            parameters[10].Value = model.OptType2;
            parameters[11].Value = model.OptValue2;
            parameters[12].Value = model.OptText3;
            parameters[13].Value = model.OptType3;
            parameters[14].Value = model.OptValue3;
            parameters[15].Value = model.OptText4;
            parameters[16].Value = model.OptType4;
            parameters[17].Value = model.OptValue4;
            parameters[18].Value = model.TreeNode;
            parameters[19].Value = model.CreateTime;
            parameters[20].Value = model.IsDelete;

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
        /// 更新一条数据
        /// </summary>
        public bool Update(Models.QuestionnaireSurvey model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update QuestionnaireSurvey set ");
            strSql.Append("ShowName=@ShowName,");
            strSql.Append("ShowType=@ShowType,");
            strSql.Append("ShowOrder=@ShowOrder,");
            strSql.Append("ShowCode=@ShowCode,");
            strSql.Append("ShowId=@ShowId,");
            strSql.Append("OptText1=@OptText1,");
            strSql.Append("OptType1=@OptType1,");
            strSql.Append("OptValue1=@OptValue1,");
            strSql.Append("OptText2=@OptText2,");
            strSql.Append("OptType2=@OptType2,");
            strSql.Append("OptValue2=@OptValue2,");
            strSql.Append("OptText3=@OptText3,");
            strSql.Append("OptType3=@OptType3,");
            strSql.Append("OptValue3=@OptValue3,");
            strSql.Append("OptText4=@OptText4,");
            strSql.Append("OptType4=@OptType4,");
            strSql.Append("OptValue4=@OptValue4,");
            strSql.Append("TreeNode=@TreeNode,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("IsDelete=@IsDelete");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@ShowName", SqlDbType.NVarChar,50),
					new SqlParameter("@ShowType", SqlDbType.NVarChar,50),
					new SqlParameter("@ShowOrder", SqlDbType.NVarChar,50),
					new SqlParameter("@ShowCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ShowId", SqlDbType.NVarChar,50),
					new SqlParameter("@OptText1", SqlDbType.NVarChar,50),
					new SqlParameter("@OptType1", SqlDbType.NVarChar,50),
					new SqlParameter("@OptValue1", SqlDbType.NVarChar,50),
					new SqlParameter("@OptText2", SqlDbType.NVarChar,50),
					new SqlParameter("@OptType2", SqlDbType.NVarChar,50),
					new SqlParameter("@OptValue2", SqlDbType.NVarChar,50),
					new SqlParameter("@OptText3", SqlDbType.NVarChar,50),
					new SqlParameter("@OptType3", SqlDbType.NVarChar,50),
					new SqlParameter("@OptValue3", SqlDbType.NVarChar,50),
					new SqlParameter("@OptText4", SqlDbType.NVarChar,50),
					new SqlParameter("@OptType4", SqlDbType.NVarChar,50),
					new SqlParameter("@OptValue4", SqlDbType.NVarChar,50),
					new SqlParameter("@TreeNode", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.Bit,1),
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16)};
            parameters[0].Value = model.ShowName;
            parameters[1].Value = model.ShowType;
            parameters[2].Value = model.ShowOrder;
            parameters[3].Value = model.ShowCode;
            parameters[4].Value = model.ShowId;
            parameters[5].Value = model.OptText1;
            parameters[6].Value = model.OptType1;
            parameters[7].Value = model.OptValue1;
            parameters[8].Value = model.OptText2;
            parameters[9].Value = model.OptType2;
            parameters[10].Value = model.OptValue2;
            parameters[11].Value = model.OptText3;
            parameters[12].Value = model.OptType3;
            parameters[13].Value = model.OptValue3;
            parameters[14].Value = model.OptText4;
            parameters[15].Value = model.OptType4;
            parameters[16].Value = model.OptValue4;
            parameters[17].Value = model.TreeNode;
            parameters[18].Value = model.CreateTime;
            parameters[19].Value = model.IsDelete;
            parameters[20].Value = model.Id;

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
            strSql.Append("delete from QuestionnaireSurvey ");
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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from QuestionnaireSurvey ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        public Models.QuestionnaireSurvey GetModel(Guid Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,ShowName,ShowType,ShowOrder,ShowCode,ShowId,OptText1,OptType1,OptValue1,OptText2,OptType2,OptValue2,OptText3,OptType3,OptValue3,OptText4,OptType4,OptValue4,TreeNode,CreateTime,IsDelete from QuestionnaireSurvey ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = Id;

            Models.QuestionnaireSurvey model = new Models.QuestionnaireSurvey();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Models.QuestionnaireSurvey DataRowToModel(DataRow row)
        {
            Models.QuestionnaireSurvey model = new Models.QuestionnaireSurvey();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = new Guid(row["Id"].ToString());
                }
                if (row["ShowName"] != null)
                {
                    model.ShowName = row["ShowName"].ToString();
                }
                if (row["ShowType"] != null)
                {
                    model.ShowType = row["ShowType"].ToString();
                }
                if (row["ShowOrder"] != null)
                {
                    model.ShowOrder = row["ShowOrder"].ToString();
                }
                if (row["ShowCode"] != null)
                {
                    model.ShowCode = row["ShowCode"].ToString();
                }
                if (row["ShowId"] != null)
                {
                    model.ShowId = row["ShowId"].ToString();
                }
                if (row["OptText1"] != null)
                {
                    model.OptText1 = row["OptText1"].ToString();
                }
                if (row["OptType1"] != null)
                {
                    model.OptType1 = row["OptType1"].ToString();
                }
                if (row["OptValue1"] != null)
                {
                    model.OptValue1 = row["OptValue1"].ToString();
                }
                if (row["OptText2"] != null)
                {
                    model.OptText2 = row["OptText2"].ToString();
                }
                if (row["OptType2"] != null)
                {
                    model.OptType2 = row["OptType2"].ToString();
                }
                if (row["OptValue2"] != null)
                {
                    model.OptValue2 = row["OptValue2"].ToString();
                }
                if (row["OptText3"] != null)
                {
                    model.OptText3 = row["OptText3"].ToString();
                }
                if (row["OptType3"] != null)
                {
                    model.OptType3 = row["OptType3"].ToString();
                }
                if (row["OptValue3"] != null)
                {
                    model.OptValue3 = row["OptValue3"].ToString();
                }
                if (row["OptText4"] != null)
                {
                    model.OptText4 = row["OptText4"].ToString();
                }
                if (row["OptType4"] != null)
                {
                    model.OptType4 = row["OptType4"].ToString();
                }
                if (row["OptValue4"] != null)
                {
                    model.OptValue4 = row["OptValue4"].ToString();
                }
                if (row["TreeNode"] != null)
                {
                    model.TreeNode = row["TreeNode"].ToString();
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }
                if (row["IsDelete"] != null && row["IsDelete"].ToString() != "")
                {
                    if ((row["IsDelete"].ToString() == "1") || (row["IsDelete"].ToString().ToLower() == "true"))
                    {
                        model.IsDelete = true;
                    }
                    else
                    {
                        model.IsDelete = false;
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ShowName,ShowType,ShowOrder,ShowCode,ShowId,OptText1,OptType1,OptValue1,OptText2,OptType2,OptValue2,OptText3,OptType3,OptValue3,OptText4,OptType4,OptValue4,TreeNode,CreateTime,IsDelete ");
            strSql.Append(" FROM QuestionnaireSurvey ");
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
            strSql.Append(" Id,ShowName,ShowType,ShowOrder,ShowCode,ShowId,OptText1,OptType1,OptValue1,OptText2,OptType2,OptValue2,OptText3,OptType3,OptValue3,OptText4,OptType4,OptValue4,TreeNode,CreateTime,IsDelete ");
            strSql.Append(" FROM QuestionnaireSurvey ");
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
            strSql.Append("select count(1) FROM QuestionnaireSurvey ");
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
            strSql.Append(")AS Row, T.*  from QuestionnaireSurvey T ");
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
            parameters[0].Value = "QuestionnaireSurvey";
            parameters[1].Value = "Id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

