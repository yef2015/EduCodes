using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TrainerEvaluate.Utility.DB;

//Please add references
namespace TrainerEvaluate.DAL
{
	/// <summary>
	/// 数据访问类:Roles
	/// </summary>
	public partial class Roles
	{
		public Roles()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Roles");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(TrainerEvaluate.Models.Roles model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Roles(");
			strSql.Append("ID,Name,Rstatus,Description,CreateTime,CreateId,CreateName,LastModifyTime,LastModifyId,LastModifyName)");
			strSql.Append(" values (");
			strSql.Append("@ID,@Name,@Rstatus,@Description,@CreateTime,@CreateId,@CreateName,@LastModifyTime,@LastModifyId,@LastModifyName)");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Rstatus", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CreateName", SqlDbType.NVarChar,50),
					new SqlParameter("@LastModifyTime", SqlDbType.DateTime),
					new SqlParameter("@LastModifyId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@LastModifyName", SqlDbType.BigInt,8)};
			parameters[0].Value = Guid.NewGuid();
			parameters[1].Value = model.Name;
			parameters[2].Value = model.Rstatus;
			parameters[3].Value = model.Description;
			parameters[4].Value = model.CreateTime;
			parameters[5].Value = Guid.NewGuid();
			parameters[6].Value = model.CreateName;
			parameters[7].Value = model.LastModifyTime;
			parameters[8].Value = Guid.NewGuid();
			parameters[9].Value = model.LastModifyName;

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
		public bool Update(TrainerEvaluate.Models.Roles model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Roles set ");
			strSql.Append("Name=@Name,");
			strSql.Append("Rstatus=@Rstatus,");
			strSql.Append("Description=@Description,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("CreateId=@CreateId,");
			strSql.Append("CreateName=@CreateName,");
			strSql.Append("LastModifyTime=@LastModifyTime,");
			strSql.Append("LastModifyId=@LastModifyId,");
			strSql.Append("LastModifyName=@LastModifyName");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Rstatus", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CreateName", SqlDbType.NVarChar,50),
					new SqlParameter("@LastModifyTime", SqlDbType.DateTime),
					new SqlParameter("@LastModifyId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@LastModifyName", SqlDbType.BigInt,8),
					new SqlParameter("@ID", SqlDbType.UniqueIdentifier,16)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.Rstatus;
			parameters[2].Value = model.Description;
			parameters[3].Value = model.CreateTime;
			parameters[4].Value = model.CreateId;
			parameters[5].Value = model.CreateName;
			parameters[6].Value = model.LastModifyTime;
			parameters[7].Value = model.LastModifyId;
			parameters[8].Value = model.LastModifyName;
			parameters[9].Value = model.ID;

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
		public bool Delete(Guid ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Roles ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = ID;

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
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Roles ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
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
		public TrainerEvaluate.Models.Roles GetModel(Guid ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,Name,Rstatus,Description,CreateTime,CreateId,CreateName,LastModifyTime,LastModifyId,LastModifyName from Roles ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = ID;

			TrainerEvaluate.Models.Roles model=new TrainerEvaluate.Models.Roles();
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
		public TrainerEvaluate.Models.Roles DataRowToModel(DataRow row)
		{
			TrainerEvaluate.Models.Roles model=new TrainerEvaluate.Models.Roles();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID= new Guid(row["ID"].ToString());
				}
				if(row["Name"]!=null)
				{
					model.Name=row["Name"].ToString();
				}
				if(row["Rstatus"]!=null && row["Rstatus"].ToString()!="")
				{
					model.Rstatus=int.Parse(row["Rstatus"].ToString());
				}
				if(row["Description"]!=null)
				{
					model.Description=row["Description"].ToString();
				}
				if(row["CreateTime"]!=null && row["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(row["CreateTime"].ToString());
				}
				if(row["CreateId"]!=null && row["CreateId"].ToString()!="")
				{
					model.CreateId= new Guid(row["CreateId"].ToString());
				}
				if(row["CreateName"]!=null)
				{
					model.CreateName=row["CreateName"].ToString();
				}
				if(row["LastModifyTime"]!=null && row["LastModifyTime"].ToString()!="")
				{
					model.LastModifyTime=DateTime.Parse(row["LastModifyTime"].ToString());
				}
				if(row["LastModifyId"]!=null && row["LastModifyId"].ToString()!="")
				{
					model.LastModifyId= new Guid(row["LastModifyId"].ToString());
				}
				if(row["LastModifyName"]!=null && row["LastModifyName"].ToString()!="")
				{
					model.LastModifyName=long.Parse(row["LastModifyName"].ToString());
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
			strSql.Append("select ID,Name,Rstatus,Description,CreateTime,CreateId,CreateName,LastModifyTime,LastModifyId,LastModifyName ");
			strSql.Append(" FROM Roles ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
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
			strSql.Append(" ID,Name,Rstatus,Description,CreateTime,CreateId,CreateName,LastModifyTime,LastModifyId,LastModifyName ");
			strSql.Append(" FROM Roles ");
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
			strSql.Append("select count(1) FROM Roles ");
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
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex,string order )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby +" "+ order);
			}
			else
			{
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from Roles T ");
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
			parameters[0].Value = "Roles";
			parameters[1].Value = "ID";
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

