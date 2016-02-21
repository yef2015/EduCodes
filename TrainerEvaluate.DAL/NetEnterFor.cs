using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.DAL
{
    /// <summary>
    /// 数据访问类:NetEnterFor
    /// </summary>
    public partial class NetEnterFor
    {
        public NetEnterFor()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(Guid Guid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from NetEnterFor");
            strSql.Append(" where Guid=@Guid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Guid", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = Guid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Models.NetEnterFor model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into NetEnterFor(");
            strSql.Append("Guid,ClassId,ClassName,TrainName,explain,BeginTime,EndTime,PersonMax,EnterForNum,CreateId,CreateName,CreateTime,LastUpdateTime,IsDelete)");
            strSql.Append(" values (");
            strSql.Append("@Guid,@ClassId,@ClassName,@TrainName,@explain,@BeginTime,@EndTime,@PersonMax,@EnterForNum,@CreateId,@CreateName,@CreateTime,@LastUpdateTime,@IsDelete)");
            SqlParameter[] parameters = {
					new SqlParameter("@Guid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ClassId", SqlDbType.NVarChar,50),
					new SqlParameter("@ClassName", SqlDbType.NVarChar,50),
					new SqlParameter("@TrainName", SqlDbType.NVarChar,200),
					new SqlParameter("@explain", SqlDbType.NVarChar,500),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@PersonMax", SqlDbType.Int,4),
					new SqlParameter("@EnterForNum", SqlDbType.Int,4),
					new SqlParameter("@CreateId", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateTime", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.Bit,1)};
            parameters[0].Value = Guid.NewGuid();
            parameters[1].Value = model.ClassId;
            parameters[2].Value = model.ClassName;
            parameters[3].Value = model.TrainName;
            parameters[4].Value = model.explain;
            parameters[5].Value = model.BeginTime;
            parameters[6].Value = model.EndTime;
            parameters[7].Value = model.PersonMax;
            parameters[8].Value = model.EnterForNum;
            parameters[9].Value = model.CreateId;
            parameters[10].Value = model.CreateName;
            parameters[11].Value = model.CreateTime;
            parameters[12].Value = model.LastUpdateTime;
            parameters[13].Value = model.IsDelete;

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
        public bool Update(Models.NetEnterFor model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update NetEnterFor set ");
            strSql.Append("ClassId=@ClassId,");
            strSql.Append("ClassName=@ClassName,");
            strSql.Append("TrainName=@TrainName,");
            strSql.Append("explain=@explain,");
            strSql.Append("BeginTime=@BeginTime,");
            strSql.Append("EndTime=@EndTime,");
            strSql.Append("PersonMax=@PersonMax,");
            strSql.Append("EnterForNum=@EnterForNum,");
            strSql.Append("CreateId=@CreateId,");
            strSql.Append("CreateName=@CreateName,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("LastUpdateTime=@LastUpdateTime,");
            strSql.Append("IsDelete=@IsDelete");
            strSql.Append(" where Guid=@Guid ");
            SqlParameter[] parameters = {
					new SqlParameter("@ClassId", SqlDbType.NVarChar,50),
					new SqlParameter("@ClassName", SqlDbType.NVarChar,50),
					new SqlParameter("@TrainName", SqlDbType.NVarChar,200),
					new SqlParameter("@explain", SqlDbType.NVarChar,500),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@PersonMax", SqlDbType.Int,4),
					new SqlParameter("@EnterForNum", SqlDbType.Int,4),
					new SqlParameter("@CreateId", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateTime", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.Bit,1),
					new SqlParameter("@Guid", SqlDbType.UniqueIdentifier,16)};
            parameters[0].Value = model.ClassId;
            parameters[1].Value = model.ClassName;
            parameters[2].Value = model.TrainName;
            parameters[3].Value = model.explain;
            parameters[4].Value = model.BeginTime;
            parameters[5].Value = model.EndTime;
            parameters[6].Value = model.PersonMax;
            parameters[7].Value = model.EnterForNum;
            parameters[8].Value = model.CreateId;
            parameters[9].Value = model.CreateName;
            parameters[10].Value = model.CreateTime;
            parameters[11].Value = model.LastUpdateTime;
            parameters[12].Value = model.IsDelete;
            parameters[13].Value = model.Guid;

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
        public bool Delete(Guid Guid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update NetEnterFor ");
            strSql.Append(" set IsDelete = 1 ");
            strSql.Append(" where Guid=@Guid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Guid", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = Guid;

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
        public bool DeleteList(string Guidlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from NetEnterFor ");
            strSql.Append(" where Guid in (" + Guidlist + ")  ");
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
        public Models.NetEnterFor GetModel(Guid Guid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Guid,ClassId,ClassName,TrainName,explain,BeginTime,EndTime,PersonMax,EnterForNum,CreateId,CreateName,CreateTime,LastUpdateTime,IsDelete from NetEnterFor ");
            strSql.Append(" where Guid=@Guid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Guid", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = Guid;

            Models.NetEnterFor model = new Models.NetEnterFor();
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
        public Models.NetEnterFor DataRowToModel(DataRow row)
        {
            Models.NetEnterFor model = new Models.NetEnterFor();
            if (row != null)
            {
                if (row["Guid"] != null && row["Guid"].ToString() != "")
                {
                    model.Guid = new Guid(row["Guid"].ToString());
                }
                if (row["ClassId"] != null)
                {
                    model.ClassId = row["ClassId"].ToString();
                }
                if (row["ClassName"] != null)
                {
                    model.ClassName = row["ClassName"].ToString();
                }
                if (row["TrainName"] != null)
                {
                    model.TrainName = row["TrainName"].ToString();
                }
                if (row["explain"] != null)
                {
                    model.explain = row["explain"].ToString();
                }
                if (row["BeginTime"] != null && row["BeginTime"].ToString() != "")
                {
                    model.BeginTime = DateTime.Parse(row["BeginTime"].ToString());
                }
                if (row["EndTime"] != null && row["EndTime"].ToString() != "")
                {
                    model.EndTime = DateTime.Parse(row["EndTime"].ToString());
                }
                if (row["PersonMax"] != null && row["PersonMax"].ToString() != "")
                {
                    model.PersonMax = int.Parse(row["PersonMax"].ToString());
                }
                if (row["EnterForNum"] != null && row["EnterForNum"].ToString() != "")
                {
                    model.EnterForNum = int.Parse(row["EnterForNum"].ToString());
                }
                if (row["CreateId"] != null)
                {
                    model.CreateId = row["CreateId"].ToString();
                }
                if (row["CreateName"] != null)
                {
                    model.CreateName = row["CreateName"].ToString();
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }
                if (row["LastUpdateTime"] != null && row["LastUpdateTime"].ToString() != "")
                {
                    model.LastUpdateTime = DateTime.Parse(row["LastUpdateTime"].ToString());
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
            strSql.Append("select Guid,ClassId,ClassName,TrainName,explain,BeginTime,EndTime,PersonMax,EnterForNum,CreateId,CreateName,CreateTime,LastUpdateTime,IsDelete ");
            strSql.Append(" FROM NetEnterFor ");
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
            strSql.Append(" Guid,ClassId,ClassName,TrainName,explain,BeginTime,EndTime,PersonMax,EnterForNum,CreateId,CreateName,CreateTime,LastUpdateTime,IsDelete ");
            strSql.Append(" FROM NetEnterFor ");
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
            strSql.Append("select count(1) FROM NetEnterFor ");
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
        public DataSet GetListByPage(string strWhere, string sort, int startIndex, int endIndex, string order)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(sort.Trim()))
            {
                strSql.Append("order by T." + sort + " " + order);
            }
            else
            {
                strSql.Append("order by T.TrainName asc");
            }
            strSql.Append(")AS Row, T.*  from NetEnterFor T ");
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
            parameters[0].Value = "NetEnterFor";
            parameters[1].Value = "Guid";
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