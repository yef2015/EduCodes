using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

//Please add references
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.DAL
{
	/// <summary>
	/// 数据访问类:Teacher
	/// </summary>
	public partial class Teacher
	{
		public Teacher()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid TeacherId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Teacher");
			strSql.Append(" where TeacherId=@TeacherId ");
			SqlParameter[] parameters = {
					new SqlParameter("@TeacherId", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = TeacherId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
   

        /// <summary>
		/// 增加一条数据
		/// </summary>
        public bool Add(Models.Teacher model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Teacher(");
            strSql.Append("TeacherId,IdentityNo,TeacherName,Gender,Title,Dept,CreateTime,LastModifyTime, Picture, Post, Research, Mobile, Status, Description,ResearchId,ResearchBigName,ResearchBigId)");
			strSql.Append(" values (");
            strSql.Append("@TeacherId,@IdentityNo,@TeacherName,@Gender,@Title,@Dept,@CreateTime,@LastModifyTime, @Picture, @Post, @Research, @Mobile, @Status, @Description,@ResearchId,@ResearchBigName,@ResearchBigId)");
			SqlParameter[] parameters = {
					new SqlParameter("@TeacherId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@IdentityNo", SqlDbType.VarChar,20),
					new SqlParameter("@TeacherName", SqlDbType.NVarChar,50),
					new SqlParameter("@Gender", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.Int,4),
					new SqlParameter("@Dept", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@LastModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@Picture", SqlDbType.VarChar),
                    new SqlParameter("@Post", SqlDbType.VarChar),
                    new SqlParameter("@Research", SqlDbType.VarChar),
                    new SqlParameter("@Mobile", SqlDbType.VarChar),
                    new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@ResearchId", SqlDbType.NVarChar,50),
					new SqlParameter("@ResearchBigName", SqlDbType.NVarChar,50),
					new SqlParameter("@ResearchBigId", SqlDbType.NVarChar,50)};
			parameters[0].Value = Guid.NewGuid();
			parameters[1].Value = model.IdentityNo;
			parameters[2].Value = model.TeacherName;
			parameters[3].Value = model.Gender;
			parameters[4].Value = model.Title;
			parameters[5].Value = model.Dept;
			parameters[6].Value = model.CreateTime;
			parameters[7].Value = model.LastModifyTime;
            parameters[8].Value = model.Picture;
            parameters[9].Value = model.Post;
            parameters[10].Value = model.Research;
            parameters[11].Value = model.Mobile;
            parameters[12].Value = model.Status;
            parameters[13].Value = model.Description;
            parameters[14].Value = model.ResearchId;
            parameters[15].Value = model.ResearchBigName;
            parameters[16].Value = model.ResearchBigId;

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
        public bool Update(Models.Teacher model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Teacher set ");
			strSql.Append("IdentityNo=@IdentityNo,");
			strSql.Append("TeacherName=@TeacherName,");
			strSql.Append("Gender=@Gender,");
			strSql.Append("Title=@Title,");
			strSql.Append("Dept=@Dept,");
			strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("LastModifyTime=@LastModifyTime,");
            strSql.Append("Picture=@Picture,");
            strSql.Append("Post=@Post,");
            strSql.Append("Research=@Research,");
            strSql.Append("Mobile=@Mobile,");
            strSql.Append("Status=@Status,");
            strSql.Append("Description=@Description,");
            strSql.Append("ResearchId=@ResearchId,");
            strSql.Append("ResearchBigName=@ResearchBigName,");
            strSql.Append("ResearchBigId=@ResearchBigId");
            strSql.Append(" where TeacherId=@TeacherId ");
			SqlParameter[] parameters = {
					new SqlParameter("@IdentityNo", SqlDbType.VarChar,20),
					new SqlParameter("@TeacherName", SqlDbType.NVarChar,50),
					new SqlParameter("@Gender", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.Int,4),
					new SqlParameter("@Dept", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@LastModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@Picture", SqlDbType.VarChar),
                    new SqlParameter("@Post", SqlDbType.VarChar),
                    new SqlParameter("@Research", SqlDbType.VarChar),
                    new SqlParameter("@Mobile", SqlDbType.VarChar),
                    new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@ResearchId", SqlDbType.NVarChar,50),
					new SqlParameter("@ResearchBigName", SqlDbType.NVarChar,50),
					new SqlParameter("@ResearchBigId", SqlDbType.NVarChar,50),
					new SqlParameter("@TeacherId", SqlDbType.UniqueIdentifier,16)};

			parameters[0].Value = model.IdentityNo;
			parameters[1].Value = model.TeacherName;
			parameters[2].Value = model.Gender;
			parameters[3].Value = model.Title;
			parameters[4].Value = model.Dept;
			parameters[5].Value = model.CreateTime;
			parameters[6].Value = model.LastModifyTime;
            parameters[7].Value = model.Picture;
            parameters[8].Value = model.Post;
            parameters[9].Value = model.Research;
            parameters[10].Value = model.Mobile;
            parameters[11].Value = model.Status;
            parameters[12].Value = model.Description;
            parameters[13].Value = model.ResearchId;
            parameters[14].Value = model.ResearchBigName;
            parameters[15].Value = model.ResearchBigId;
            parameters[16].Value = model.TeacherId;

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
		public bool Delete(Guid TeacherId)
		{
			
			StringBuilder strSql=new StringBuilder();
			//strSql.Append("delete from Teacher ");
            strSql.Append("update Teacher set Status = 0");
			strSql.Append(" where TeacherId=@TeacherId ");
			SqlParameter[] parameters = {
					new SqlParameter("@TeacherId", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = TeacherId;

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
		public bool DeleteList(string TeacherIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			//strSql.Append("delete from Teacher ");
            strSql.Append("update Teacher set Status = 0 ");
			strSql.Append(" where TeacherId in ("+TeacherIdlist + ")  ");
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
        public Models.Teacher GetModel(Guid TeacherId)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 TeacherId,IdentityNo,TeacherName,Gender,Title,Dept,CreateTime,LastModifyTime, Picture, Post, Research, Mobile, Status, Description,ResearchId,ResearchBigName,ResearchBigId from Teacher ");
			strSql.Append(" where TeacherId=@TeacherId ");
			SqlParameter[] parameters = {
					new SqlParameter("@TeacherId", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = TeacherId;

            Models.Teacher model = new Models.Teacher();
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
        public Models.Teacher DataRowToModel(DataRow row)
		{
            Models.Teacher model = new Models.Teacher();
			if (row != null)
			{
				if(row["TeacherId"]!=null && row["TeacherId"].ToString()!="")
				{
					model.TeacherId= new Guid(row["TeacherId"].ToString());
				}
				if(row["IdentityNo"]!=null)
				{
					model.IdentityNo=row["IdentityNo"].ToString();
				}
				if(row["TeacherName"]!=null)
				{
					model.TeacherName=row["TeacherName"].ToString();
				}
				if(row["Gender"]!=null && row["Gender"].ToString()!="")
				{
					model.Gender=int.Parse(row["Gender"].ToString());
				}
                if (row["Title"] != null && row["Title"].ToString() != "")
				{
                    model.Title = Convert.ToInt32(row["Title"].ToString());
				}
				if(row["Dept"]!=null)
				{
					model.Dept=row["Dept"].ToString();
				}
				if(row["CreateTime"]!=null && row["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(row["CreateTime"].ToString());
				}
				if(row["LastModifyTime"]!=null && row["LastModifyTime"].ToString()!="")
				{
					model.LastModifyTime=DateTime.Parse(row["LastModifyTime"].ToString());
				}
                if (row["Picture"] != null && row["Picture"].ToString() != "")
                {
                    model.Picture = row["Picture"].ToString();
                }
                if (row["Post"] != null && row["Post"].ToString() != "")
                {
                    model.Post = row["Post"].ToString();
                }
                if (row["Research"] != null && row["Research"].ToString() != "")
                {
                    model.Research = row["Research"].ToString();
                }
                if (row["Mobile"] != null && row["Mobile"].ToString() != "")
                {
                    model.Mobile = row["Mobile"].ToString();
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["Description"] != null && row["Description"].ToString() != "")
                {
                    model.Description = row["Description"].ToString();
                }
                if (row["ResearchId"] != null && row["ResearchId"].ToString() != "")
                {
                    model.ResearchId = row["ResearchId"].ToString();
                }
                if (row["ResearchBigName"] != null && row["ResearchBigName"].ToString() != "")
                {
                    model.ResearchBigName = row["ResearchBigName"].ToString();
                }
                if (row["ResearchBigId"] != null && row["ResearchBigId"].ToString() != "")
                {
                    model.ResearchBigId = row["ResearchBigId"].ToString();
                }
                /*
                 , Picture, Post, Research, Mobile, Status, Description*/
            }
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select TeacherId,IdentityNo,TeacherName,Gender,case Gender when 1 then '男' else '女' end as GenderName,Title,Dept,CreateTime,LastModifyTime, Picture, Post, Research, Mobile, Status, Description,ResearchId,ResearchBigName,ResearchBigId ");
			strSql.Append(" FROM Teacher ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            strSql.Append(" order by TeacherName asc ");
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
            strSql.Append(" TeacherId,IdentityNo,TeacherName,Gender,Title,Dept,CreateTime,LastModifyTime, Picture, Post, Research, Mobile, Status, Description,ResearchId,ResearchBigName,ResearchBigId ");
			strSql.Append(" FROM Teacher ");
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
			strSql.Append("select count(1) FROM Teacher ");
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
                strSql.Append("order by T.TeacherName asc");
			}
			strSql.Append(")AS Row, T.*  from   ");
            strSql.Append(" ( select a.*,b.Name as GenderName,c.Name as JobTitleName from  Teacher a left join   Dictionaries b  on a.Gender=b.ID  left join Dictionaries c on a.Title=c.ID where a.Status=1  ) ");
			strSql.Append(" T ");
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
			parameters[0].Value = "Teacher";
			parameters[1].Value = "TeacherId";
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

