using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

//Please add references
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.DAL
{
	/// <summary>
	/// 数据访问类:SysUser
	/// </summary>
	public partial class SysUser
	{ public SysUser()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid UserId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SysUser");
			strSql.Append(" where UserId=@UserId ");
			SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = UserId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


        public bool ExistsAccount(string userAccount,Guid userGuid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SysUser");
            strSql.Append(" where UserAccount=@UserAccount and  UserId<>@UserId ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserAccount", SqlDbType.VarChar,50),		
					new SqlParameter("@UserId", SqlDbType.UniqueIdentifier,16)		};
            parameters[0].Value = userAccount;
            parameters[1].Value = userGuid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(TrainerEvaluate.Models.SysUser model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SysUser(");
            strSql.Append("UserId,UserRole,UserName,UserPassWord,CreateTime,UserAccount,Dept)");
			strSql.Append(" values (");
            strSql.Append("@UserId,@UserRole,@UserName,@UserPassWord,@CreateTime,@UserAccount,@Dept)");
			SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@UserRole", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@UserPassWord", SqlDbType.VarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UserAccount", SqlDbType.VarChar,50),
					new SqlParameter("@Dept", SqlDbType.VarChar,50)};
			parameters[0].Value = model.UserId;
			parameters[1].Value = model.UserRole;
			parameters[2].Value = model.UserName;
			parameters[3].Value = model.UserPassWord;
			parameters[4].Value = System.DateTime.Now;
			parameters[5].Value = model.UserAccount;
			parameters[6].Value = model.Dept;

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
		public bool Update(TrainerEvaluate.Models.SysUser model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SysUser set ");
			strSql.Append("UserRole=@UserRole,");
			strSql.Append("UserName=@UserName,");
			strSql.Append("UserPassWord=@UserPassWord,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("UserAccount=@UserAccount,");
            strSql.Append("Dept=@Dept");
			strSql.Append(" where UserId=@UserId ");
			SqlParameter[] parameters = {
					new SqlParameter("@UserRole", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@UserPassWord", SqlDbType.VarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UserAccount", SqlDbType.VarChar,50),
					new SqlParameter("@UserId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@Dept", SqlDbType.VarChar,50)};
			parameters[0].Value = model.UserRole;
			parameters[1].Value = model.UserName;
			parameters[2].Value = model.UserPassWord;
			parameters[3].Value = model.CreateTime;
			parameters[4].Value = model.UserAccount;
			parameters[5].Value = model.UserId;
			parameters[6].Value = model.Dept;

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
		public bool Delete(Guid UserId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SysUser ");
			strSql.Append(" where UserId=@UserId ");
			SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = UserId;

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
		public bool DeleteList(string UserIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SysUser ");
			strSql.Append(" where UserId in ("+UserIdlist + ")  ");
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
		public TrainerEvaluate.Models.SysUser GetModel(Guid UserId)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 UserId,UserRole,UserName,UserPassWord,CreateTime,UserAccount,Dept from SysUser ");
			strSql.Append(" where UserId=@UserId ");
			SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = UserId;

			TrainerEvaluate.Models.SysUser model=new TrainerEvaluate.Models.SysUser();
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
		public TrainerEvaluate.Models.SysUser DataRowToModel(DataRow row)
		{
			TrainerEvaluate.Models.SysUser model=new TrainerEvaluate.Models.SysUser();
			if (row != null)
			{
				if(row["UserId"]!=null && row["UserId"].ToString()!="")
				{
					model.UserId= new Guid(row["UserId"].ToString());
				}
				if(row["UserRole"]!=null && row["UserRole"].ToString()!="")
				{
					model.UserRole=int.Parse(row["UserRole"].ToString());
				}
				if(row["UserName"]!=null)
				{
					model.UserName=row["UserName"].ToString();
				}
				if(row["UserPassWord"]!=null)
				{
					model.UserPassWord=row["UserPassWord"].ToString();
				}
				if(row["CreateTime"]!=null && row["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(row["CreateTime"].ToString());
				}
				if(row["UserAccount"]!=null)
				{
					model.UserAccount=row["UserAccount"].ToString();
                } 
                if (row["Dept"] != null)
				{
                    model.Dept = row["Dept"].ToString();
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
            strSql.Append("select UserId,UserRole,case UserRole when 1 then '学员' when 2 then '教师' when 3 then '管理员' when 4 then '课程管理员'  end UserRoleName,UserName,UserPassWord,CreateTime,UserAccount,Dept ");
			strSql.Append(" FROM SysUser ");
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
            strSql.Append(" UserId,UserRole,UserName,UserPassWord,CreateTime,UserAccount,Dept ");
			strSql.Append(" FROM SysUser ");
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
			strSql.Append("select count(1) FROM SysUser ");
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
            strSql.Append("SELECT  UserId,UserRole,case UserRole when 1 then '学员' when 2 then '教师' when 3 then '管理员' when 4 then '课程管理员'  end UserRoleName,UserName,UserPassWord,CreateTime,UserAccount,Dept  FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(sort.Trim()))
			{
				strSql.Append("order by T." + sort +" "+ order);
			}
			else
			{
				strSql.Append("order by T.UserId desc");
			}
			strSql.Append(")AS Row, T.*  from SysUser T ");
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
            parameters[0].Value = "SysUser";
            parameters[1].Value = "UserId";
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

