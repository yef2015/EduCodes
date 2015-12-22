using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

//Please add references
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.DAL
{
    /// <summary>
    /// 数据访问类:SchoolDistrict
    /// </summary>
    public partial class SPSchoolDistrict
    {
        public SPSchoolDistrict()
		{}

		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(Guid SchDisId)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select count(1) from SchoolDistrict");
            strSql.Append(" where SchDisId=@SchDisId ");
			SqlParameter[] parameters = {
					new SqlParameter("@SchDisId", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = SchDisId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}   

        /// <summary>
		/// 增加一条数据
		/// </summary>
        public bool Add(Models.SPSchoolDistrict model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into SchoolDistrict(");
            strSql.Append("SchDisId,SchDisName,Status,Description,CreatedDate,LastModifyTime)");
			strSql.Append(" values (");
            strSql.Append("@SchDisId,@SchDisName,@Status,@Description,@CreatedDate,@LastModifyTime)");
			SqlParameter[] parameters = {
					new SqlParameter("@SchDisId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@SchDisName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@LastModifyTime", SqlDbType.DateTime)};
			parameters[0].Value = Guid.NewGuid();
			parameters[1].Value = model.SchDisName;
            parameters[2].Value = model.Status;
            parameters[3].Value = model.Description;
            parameters[4].Value = model.CreatedDate;
            parameters[5].Value = model.LastModifyTime;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
        public bool Update(Models.SPSchoolDistrict model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update SchoolDistrict set ");
            strSql.Append("SchDisName=@SchDisName,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("LastModifyTime=@LastModifyTime,");
            strSql.Append("Status=@Status,");
            strSql.Append("Description=@Description ");
            strSql.Append(" where SchDisId=@SchDisId ");
			SqlParameter[] parameters = {
					new SqlParameter("@SchDisName", SqlDbType.NVarChar,50),
                    new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@LastModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@SchDisId", SqlDbType.UniqueIdentifier,16)};

			parameters[0].Value = model.SchDisName;
			parameters[1].Value = model.CreatedDate;
            parameters[2].Value = model.LastModifyTime;
			parameters[3].Value = model.Status;
			parameters[4].Value = model.Description;
			parameters[5].Value = model.SchDisId;
           
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
        public bool Delete(Guid SchDisId)
		{			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update SchoolDistrict set Status = 0");
            strSql.Append(" where SchDisId=@SchDisId ");
			SqlParameter[] parameters = {
					new SqlParameter("@SchDisId", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = SchDisId;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
        public bool DeleteList(string SchDisIdlist)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update SchoolDistrict set Status = 0 ");
            strSql.Append(" where SchDisId in (" + SchDisIdlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
        public Models.SPSchoolDistrict GetModel(Guid SchDisId)
		{			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 SchDisId, SchDisName, Status, Description,CreatedDate,LastModifyTime from SchoolDistrict ");
            strSql.Append(" where SchDisId=@SchDisId ");
			SqlParameter[] parameters = {
					new SqlParameter("@SchDisId", SqlDbType.UniqueIdentifier,16)};
            parameters[0].Value = SchDisId;

            Models.SPSchoolDistrict model = new Models.SPSchoolDistrict();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
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
        public Models.SPSchoolDistrict DataRowToModel(DataRow row)
		{
            Models.SPSchoolDistrict model = new Models.SPSchoolDistrict();
			if (row != null)
			{
                if (row["SchDisId"] != null && row["SchDisId"].ToString() != "")
				{
                    model.SchDisId = new Guid(row["SchDisId"].ToString());
				}
                if (row["SchDisName"] != null && row["SchDisName"].ToString() != "")
				{
                    model.SchDisName = row["SchDisName"].ToString();
				}
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
				{
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
				}
                if (row["LastModifyTime"] != null && row["LastModifyTime"].ToString() != "")
                {
                    model.LastModifyTime = DateTime.Parse(row["LastModifyTime"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["Description"] != null && row["Description"].ToString() != "")
                {
                    model.Description = row["Description"].ToString();
                }
            }
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select SchDisId,SchDisName,Status,Description, CreatedDate,LastModifyTime ");
            strSql.Append(" FROM SchoolDistrict ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            strSql.Append(" order by SchDisName asc ");
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
            strSql.Append(" SchDisId,SchDisName,Status,Description, CreatedDate,LastModifyTime ");
            strSql.Append(" FROM SchoolDistrict ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select count(1) FROM SchoolDistrict ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
		public DataSet GetListByPage(string strWhere, string sort, int startIndex, int endIndex,string order)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(sort.Trim()))
			{
                strSql.Append("order by T." + sort +" "+order);
			}
			else
			{
                strSql.Append("order by T.SchDisName asc");
			}
			strSql.Append(")AS Row, T.*  from   ");
            strSql.Append(" ( select * from  SchoolDistrict where Status=1  ) ");
			strSql.Append(" T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}
