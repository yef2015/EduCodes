using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

//Please add references
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.DAL
{
	/// <summary>
	/// 数据访问类:Course
	/// </summary>
	public partial class Course
	{
		public Course()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid CourseId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Course");
			strSql.Append(" where CourseId=@CourseId ");
			SqlParameter[] parameters = {
					new SqlParameter("@CourseId", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = CourseId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Models.Course model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Course(");
            strSql.Append("CourseId,CourseName,TeacherId,TeacherName,TeachTime,TeachPlace,CreatTime,LastModifyTime,Type,Status,Description,TypeName,TypeSmallId,TypeSmallName)");
            strSql.Append(" values (");
            strSql.Append("@CourseId,@CourseName,@TeacherId,@TeacherName,@TeachTime,@TeachPlace,@CreatTime,@LastModifyTime,@Type,@Status,@Description,@TypeName,@TypeSmallId,@TypeSmallName)");
            SqlParameter[] parameters = {
					new SqlParameter("@CourseId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CourseName", SqlDbType.NVarChar,50),
					new SqlParameter("@TeacherId", SqlDbType.VarChar,-1),
					new SqlParameter("@TeacherName", SqlDbType.NVarChar,-1),
					new SqlParameter("@TeachTime", SqlDbType.NVarChar,50),
					new SqlParameter("@TeachPlace", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatTime", SqlDbType.DateTime),
					new SqlParameter("@LastModifyTime", SqlDbType.DateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@TypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@TypeSmallId", SqlDbType.NVarChar,50),
					new SqlParameter("@TypeSmallName", SqlDbType.NVarChar,50)};
            parameters[0].Value = Guid.NewGuid();
            parameters[1].Value = model.CourseName;
            parameters[2].Value = model.TeacherId;
            parameters[3].Value = model.TeacherName;
            parameters[4].Value = model.TeachTime;
            parameters[5].Value = model.TeachPlace;
            parameters[6].Value = model.CreatTime;
            parameters[7].Value = model.LastModifyTime;
            parameters[8].Value = model.Type;
            parameters[9].Value = model.Status;
            parameters[10].Value = model.Description;
            parameters[11].Value = model.TypeName;
            parameters[12].Value = model.TypeSmallId;
            parameters[13].Value = model.TypeSmallName;

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
		public bool Update(Models.Course model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Course set ");
            strSql.Append("CourseName=@CourseName,");
            strSql.Append("TeacherId=@TeacherId,");
            strSql.Append("TeacherName=@TeacherName,");
            strSql.Append("TeachTime=@TeachTime,");
            strSql.Append("TeachPlace=@TeachPlace,");
            strSql.Append("CreatTime=@CreatTime,");
            strSql.Append("LastModifyTime=@LastModifyTime,");
            strSql.Append("Type=@Type,");
            strSql.Append("Status=@Status,");
            strSql.Append("Description=@Description,");
            strSql.Append("TypeName=@TypeName,");
            strSql.Append("TypeSmallId=@TypeSmallId,");
            strSql.Append("TypeSmallName=@TypeSmallName");
            strSql.Append(" where CourseId=@CourseId ");
            SqlParameter[] parameters = {
					new SqlParameter("@CourseName", SqlDbType.NVarChar,50),
					new SqlParameter("@TeacherId", SqlDbType.VarChar,-1),
					new SqlParameter("@TeacherName", SqlDbType.NVarChar,-1),
					new SqlParameter("@TeachTime", SqlDbType.NVarChar,50),
					new SqlParameter("@TeachPlace", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatTime", SqlDbType.DateTime),
					new SqlParameter("@LastModifyTime", SqlDbType.DateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@TypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@TypeSmallId", SqlDbType.NVarChar,50),
					new SqlParameter("@TypeSmallName", SqlDbType.NVarChar,50),
					new SqlParameter("@CourseId", SqlDbType.UniqueIdentifier,16)};
            parameters[0].Value = model.CourseName;
            parameters[1].Value = model.TeacherId;
            parameters[2].Value = model.TeacherName;
            parameters[3].Value = model.TeachTime;
            parameters[4].Value = model.TeachPlace;
            parameters[5].Value = model.CreatTime;
            parameters[6].Value = model.LastModifyTime;
            parameters[7].Value = model.Type;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.Description;
            parameters[10].Value = model.TypeName;
            parameters[11].Value = model.TypeSmallId;
            parameters[12].Value = model.TypeSmallName;
            parameters[13].Value = model.CourseId;

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
		public bool Delete(Guid CourseId)
		{
			
			StringBuilder strSql=new StringBuilder();
			//strSql.Append("delete from Course ");
            strSql.Append("update Course set Status = 0 ");
			strSql.Append(" where CourseId=@CourseId ");
			SqlParameter[] parameters = {
					new SqlParameter("@CourseId", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = CourseId;

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
		public bool DeleteList(string CourseIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			//strSql.Append("delete from Course ");
            strSql.Append("update Course set Status = 0 ");
			strSql.Append(" where CourseId in ("+CourseIdlist + ")  ");
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
		public Models.Course GetModel(Guid CourseId)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 CourseId,CourseName,TeacherId,TeacherName,TeachTime,TeachPlace,CreatTime,LastModifyTime, Type, Status, Description,TypeName,TypeSmallId,TypeSmallName  from Course ");
			strSql.Append(" where CourseId=@CourseId ");
			SqlParameter[] parameters = {
					new SqlParameter("@CourseId", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = CourseId;

            Models.Course model = new Models.Course();
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
		public  Models.Course DataRowToModel(DataRow row)
		{
			Models.Course model=new  Models.Course();
			if (row != null)
			{
				if(row["CourseId"]!=null && row["CourseId"].ToString()!="")
				{
					model.CourseId= new Guid(row["CourseId"].ToString());
				}
				if(row["CourseName"]!=null)
				{
					model.CourseName=row["CourseName"].ToString();
				}
				if(row["TeacherId"]!=null && row["TeacherId"].ToString()!="")
				{
					model.TeacherId= row["TeacherId"].ToString();
				}
				if(row["TeacherName"]!=null)
				{
					model.TeacherName=row["TeacherName"].ToString();
				}
				if(row["TeachTime"]!=null)
				{
					model.TeachTime=row["TeachTime"].ToString();
				}
				if(row["TeachPlace"]!=null)
				{
					model.TeachPlace=row["TeachPlace"].ToString();
				}
				if(row["CreatTime"]!=null && row["CreatTime"].ToString()!="")
				{
					model.CreatTime=DateTime.Parse(row["CreatTime"].ToString());
				}
				if(row["LastModifyTime"]!=null && row["LastModifyTime"].ToString()!="")
				{
					model.LastModifyTime=DateTime.Parse(row["LastModifyTime"].ToString());
				}

                if (row["Type"] != null && row["Type"].ToString() != "")
                {
                    model.Type = int.Parse(row["Type"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["Description"] != null && row["Description"].ToString() != "")
                {
                    model.Description = row["Description"].ToString();
                }
                if (row["TypeName"] != null && row["TypeName"].ToString() != "")
                {
                    model.TypeName = row["TypeName"].ToString();
                }
                if (row["TypeSmallId"] != null && row["TypeSmallId"].ToString() != "")
                {
                    model.TypeSmallId = row["TypeSmallId"].ToString();
                }
                if (row["TypeSmallName"] != null && row["TypeSmallName"].ToString() != "")
                {
                    model.TypeSmallName = row["TypeSmallName"].ToString();
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
            strSql.Append("select CourseId,CourseName,TeacherId,TeacherName,TeachTime,TeachPlace,CreatTime,LastModifyTime, Type, Status, Description,TypeName,TypeSmallId,TypeSmallName ");
			strSql.Append(" FROM Course ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            strSql.Append(" order by  TeachTime desc ");
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
            strSql.Append(" CourseId,CourseName,TeacherId,TeacherName,TeachTime,TeachPlace,CreatTime,LastModifyTime, Type, Status, Description,TypeName,TypeSmallId,TypeSmallName ");
			strSql.Append(" FROM Course ");
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
			strSql.Append("select count(1) FROM Course ");
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
                strSql.Append("order by T." + sort + "  "+order);
			}
			else
			{
                strSql.Append("order by T.TeachTime desc");
			}
			strSql.Append(")AS Row, T.*  from Course T ");
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
			parameters[0].Value = "Course";
			parameters[1].Value = "CourseId";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod



	    public Guid GetCourseId(string courseName, string teacher, string teachTime, string teachPlace)
	    {
	        var sql = string.Format("   select b.CourseId   from  Course b " +
	                                "where    b.CourseName='{0}' and b.TeacherName='{1}' and b.TeachTime='{2}' and b.TeachPlace='{3}'",
	            courseName, teacher, teachTime, teachPlace);

	        var ds = DbHelperSQL.Query(sql);
	        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
	        {
	            return new Guid(ds.Tables[0].Rows[0]["CourseId"].ToString());
	        }
	        return Guid.Empty;
	    }




        public Guid GetTop1Guid()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select top 1 CourseId from  Course order by CreatTime desc  ");
            
            var res=  DbHelperSQL.Query(strSql.ToString());
            if (res != null && res.Tables.Count > 0&&res.Tables[0].Rows.Count>0)
            {
                return  new Guid(res.Tables[0].Rows[0]["CourseId"].ToString());
            }
            return Guid.Empty;
        }



	    #endregion  ExtensionMethod
	}
}

