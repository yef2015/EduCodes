using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

//Please add references
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.DAL
{
	/// <summary>
	/// 数据访问类:Student
	/// </summary>
	public partial class Student
	{
		public Student()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid StudentId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Student");
			strSql.Append(" where StudentId=@StudentId ");
			SqlParameter[] parameters = {new SqlParameter("@StudentId", SqlDbType.UniqueIdentifier,16)};
			parameters[0].Value = StudentId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Models.Student model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Student(");
            strSql.Append("StudentId,IdentityNo,StuName,Gender,School,JobTitle,TelNo,CreateTime,LastModifyTime,Picture,Birthday,Nation,FirstRecord,FirstSchool,LastRecord,LastSchool,PoliticsStatus,Rank,RankTime,Post,PostTime,Mobile,TeachNo,Status,Description,PostOptName,PostOptId)");
            strSql.Append(" values (");
            strSql.Append("@StudentId,@IdentityNo,@StuName,@Gender,@School,@JobTitle,@TelNo,@CreateTime,@LastModifyTime,@Picture,@Birthday,@Nation,@FirstRecord,@FirstSchool,@LastRecord,@LastSchool,@PoliticsStatus,@Rank,@RankTime,@Post,@PostTime,@Mobile,@TeachNo,@Status,@Description,@PostOptName,@PostOptId)");
            SqlParameter[] parameters = {
					new SqlParameter("@StudentId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@IdentityNo", SqlDbType.VarChar,20),
					new SqlParameter("@StuName", SqlDbType.NVarChar,50),
					new SqlParameter("@Gender", SqlDbType.Int,4),
					new SqlParameter("@School", SqlDbType.NVarChar,50),
					new SqlParameter("@JobTitle", SqlDbType.Int,4),
					new SqlParameter("@TelNo", SqlDbType.VarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@LastModifyTime", SqlDbType.DateTime),
					new SqlParameter("@Picture", SqlDbType.VarChar,50),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@Nation", SqlDbType.Int,4),
					new SqlParameter("@FirstRecord", SqlDbType.VarChar,50),
					new SqlParameter("@FirstSchool", SqlDbType.VarChar,50),
					new SqlParameter("@LastRecord", SqlDbType.VarChar,50),
					new SqlParameter("@LastSchool", SqlDbType.VarChar,50),
					new SqlParameter("@PoliticsStatus", SqlDbType.Int,4),
					new SqlParameter("@Rank", SqlDbType.VarChar,50),
					new SqlParameter("@RankTime", SqlDbType.DateTime),
					new SqlParameter("@Post", SqlDbType.VarChar,50),
					new SqlParameter("@PostTime", SqlDbType.DateTime),
					new SqlParameter("@Mobile", SqlDbType.VarChar,11),
					new SqlParameter("@TeachNo", SqlDbType.VarChar,50),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@PostOptName", SqlDbType.NVarChar,50),
					new SqlParameter("@PostOptId", SqlDbType.NVarChar,50)};
            parameters[0].Value = Guid.NewGuid();
            parameters[1].Value = model.IdentityNo;
            parameters[2].Value = model.StuName;
            parameters[3].Value = model.Gender;
            parameters[4].Value = model.School;
            parameters[5].Value = model.JobTitle;
            parameters[6].Value = model.TelNo;
            parameters[7].Value = model.CreateTime;
            parameters[8].Value = model.LastModifyTime;
            //parameters[9].Value = model.Picture;
            parameters[9].Value = "";
            parameters[10].Value = model.Birthday;
            parameters[11].Value = model.Nation;
            parameters[12].Value = model.FirstRecord;
            parameters[13].Value = model.FirstSchool;
            parameters[14].Value = model.LastRecord;
            parameters[15].Value = model.LastSchool;
            parameters[16].Value = model.PoliticsStatus;
            parameters[17].Value = model.Rank;
            parameters[18].Value = model.RankTime;
            parameters[19].Value = model.Post;
            parameters[20].Value = model.PostTime;
            parameters[21].Value = model.Mobile;
            parameters[22].Value = model.TeachNo;
            parameters[23].Value = 1;
            parameters[24].Value = model.Description;
            parameters[25].Value = model.PostOptName;
            parameters[26].Value = model.PostOptId;

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
        public bool Update(Models.Student model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Student set ");
            strSql.Append("IdentityNo=@IdentityNo,");
            strSql.Append("StuName=@StuName,");
            strSql.Append("Gender=@Gender,");
            strSql.Append("School=@School,");
            strSql.Append("JobTitle=@JobTitle,");
            strSql.Append("TelNo=@TelNo,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("LastModifyTime=@LastModifyTime,");
            strSql.Append("Picture=@Picture,");
            strSql.Append("Birthday=@Birthday,");
            strSql.Append("Nation=@Nation,");
            strSql.Append("FirstRecord=@FirstRecord,");
            strSql.Append("FirstSchool=@FirstSchool,");
            strSql.Append("LastRecord=@LastRecord,");
            strSql.Append("LastSchool=@LastSchool,");
            strSql.Append("PoliticsStatus=@PoliticsStatus,");
            strSql.Append("Rank=@Rank,");
            strSql.Append("RankTime=@RankTime,");
            strSql.Append("Post=@Post,");
            strSql.Append("PostTime=@PostTime,");
            strSql.Append("Mobile=@Mobile,");
            strSql.Append("TeachNo=@TeachNo,");
            strSql.Append("Status=@Status,");
            strSql.Append("Description=@Description,");
            strSql.Append("PostOptName=@PostOptName,");
            strSql.Append("PostOptId=@PostOptId");
            strSql.Append(" where StudentId=@StudentId ");
            SqlParameter[] parameters = {
					new SqlParameter("@IdentityNo", SqlDbType.VarChar,20),
					new SqlParameter("@StuName", SqlDbType.NVarChar,50),
					new SqlParameter("@Gender", SqlDbType.Int,4),
					new SqlParameter("@School", SqlDbType.NVarChar,50),
					new SqlParameter("@JobTitle", SqlDbType.Int,4),
					new SqlParameter("@TelNo", SqlDbType.VarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@LastModifyTime", SqlDbType.DateTime),
					new SqlParameter("@Picture", SqlDbType.VarChar,50),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@Nation", SqlDbType.Int,4),
					new SqlParameter("@FirstRecord", SqlDbType.VarChar,50),
					new SqlParameter("@FirstSchool", SqlDbType.VarChar,50),
					new SqlParameter("@LastRecord", SqlDbType.VarChar,50),
					new SqlParameter("@LastSchool", SqlDbType.VarChar,50),
					new SqlParameter("@PoliticsStatus", SqlDbType.Int,4),
					new SqlParameter("@Rank", SqlDbType.VarChar,50),
					new SqlParameter("@RankTime", SqlDbType.DateTime),
					new SqlParameter("@Post", SqlDbType.VarChar,50),
					new SqlParameter("@PostTime", SqlDbType.DateTime),
					new SqlParameter("@Mobile", SqlDbType.VarChar,11),
					new SqlParameter("@TeachNo", SqlDbType.VarChar,50),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@PostOptName", SqlDbType.NVarChar,50),
					new SqlParameter("@PostOptId", SqlDbType.NVarChar,50),
					new SqlParameter("@StudentId", SqlDbType.UniqueIdentifier,16)};
            parameters[0].Value = model.IdentityNo;
            parameters[1].Value = model.StuName;
            parameters[2].Value = model.Gender;
            parameters[3].Value = model.School;
            parameters[4].Value = model.JobTitle;
            parameters[5].Value = model.TelNo;
            parameters[6].Value = model.CreateTime;
            parameters[7].Value = model.LastModifyTime;
            parameters[8].Value = model.Picture;
            parameters[9].Value = model.Birthday;
            parameters[10].Value = model.Nation;
            parameters[11].Value = model.FirstRecord;
            parameters[12].Value = model.FirstSchool;
            parameters[13].Value = model.LastRecord;
            parameters[14].Value = model.LastSchool;
            parameters[15].Value = model.PoliticsStatus;
            parameters[16].Value = model.Rank;
            parameters[17].Value = model.RankTime;
            parameters[18].Value = model.Post;
            parameters[19].Value = model.PostTime;
            parameters[20].Value = model.Mobile;
            parameters[21].Value = model.TeachNo;
            parameters[22].Value = model.Status;
            parameters[23].Value = model.Description;
            parameters[24].Value = model.PostOptName;
            parameters[25].Value = model.PostOptId;
            parameters[26].Value = model.StudentId;

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
		public bool Delete(Guid StudentId)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update Student set Status = 0 ");
			strSql.Append(" where StudentId=@StudentId ");
			SqlParameter[] parameters = {
					new SqlParameter("@StudentId", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = StudentId;

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
		public bool DeleteList(string StudentIdlist )
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update Student set Status = 0 ");
			strSql.Append(" where StudentId in ("+StudentIdlist + ")  ");
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
        public Models.Student GetModel(Guid StudentId)
		{

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 StudentId,IdentityNo,StuName,Gender,School,JobTitle,TelNo,CreateTime,LastModifyTime,Picture,Birthday,Nation,FirstRecord,FirstSchool,LastRecord,LastSchool,PoliticsStatus,Rank,RankTime,Post,PostTime,Mobile,TeachNo,Status,Description,PostOptName,PostOptId from Student ");
            strSql.Append(" where StudentId=@StudentId ");
            SqlParameter[] parameters = {
					new SqlParameter("@StudentId", SqlDbType.UniqueIdentifier)			};
            parameters[0].Value = StudentId;

            Models.Student model = new Models.Student();
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
        public Models.Student DataRowToModel(DataRow row)
		{
            Models.Student model = new Models.Student();
			if (row != null)
			{
				if(row["StudentId"]!=null && row["StudentId"].ToString()!="")
				{
					model.StudentId= new Guid(row["StudentId"].ToString());
				}
				if(row["IdentityNo"]!=null)
				{
					model.IdentityNo=row["IdentityNo"].ToString();
				}
				if(row["StuName"]!=null)
				{
					model.StuName=row["StuName"].ToString();
				}
				if(row["Gender"]!=null && row["Gender"].ToString()!="")
				{
					model.Gender=int.Parse(row["Gender"].ToString());
				}
				if(row["School"]!=null)
				{
					model.School=row["School"].ToString();
				}
                if (row["JobTitle"] != null && row["JobTitle"].ToString() != "")
				{
                    model.JobTitle =int.Parse( row["JobTitle"].ToString());
				}
				if(row["TelNo"]!=null)
				{
					model.TelNo=row["TelNo"].ToString();
				}
				if(row["CreateTime"]!=null && row["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(row["CreateTime"].ToString());
				}
				if(row["LastModifyTime"]!=null && row["LastModifyTime"].ToString()!="")
				{
					model.LastModifyTime=DateTime.Parse(row["LastModifyTime"].ToString());
				}
                /*Picture,Birthday, Nation, FirstRecord, FirstSchool, LastRecord,LastSchool,PoliticsStatus, Rank, RankTime, Post, PostTime, Mobile, TeachNo, Status, Description from Student */
                if (row["Picture"] != null && row["Picture"].ToString() != "")
                {
                    model.Picture = row["Picture"].ToString();
                }
                if (row["Birthday"] != null && row["Birthday"].ToString() != "")
                {
                    model.Birthday = DateTime.Parse(row["Birthday"].ToString());
                }

                if (row["Nation"] != null && row["Nation"].ToString() != "")
                {
                    model.Nation = int.Parse(row["Nation"].ToString());
                }
                if (row["FirstRecord"] != null && row["FirstRecord"].ToString() != "")
                {
                    model.FirstRecord =row["FirstRecord"].ToString();
                }
                if (row["FirstSchool"] != null && row["FirstSchool"].ToString() != "")
                {
                    model.FirstSchool = row["FirstSchool"].ToString();
                }
                if (row["LastRecord"] != null && row["LastRecord"].ToString() != "")
                {
                    model.LastRecord = row["LastRecord"].ToString();
                }
                if (row["LastSchool"] != null && row["LastSchool"].ToString() != "")
                {
                    model.LastSchool = row["LastSchool"].ToString();
                }
                if (row["PoliticsStatus"] != null && row["PoliticsStatus"].ToString() != "")
                {
                    model.PoliticsStatus = int.Parse(row["PoliticsStatus"].ToString());
                }
                if (row["Rank"] != null && row["Rank"].ToString() != "")
                {
                    model.Rank = row["Rank"].ToString();
                }
                if (row["RankTime"] != null && row["RankTime"].ToString() != "")
                {
                    model.RankTime = DateTime.Parse(row["RankTime"].ToString());
                }
                /*Post, PostTime, Mobile, TeachNo, Status, Description*/
                if (row["Post"] != null && row["Post"].ToString() != "")
                {
                    model.Post = row["Post"].ToString();
                }
                if (row["PostTime"] != null && row["PostTime"].ToString() != "")
                {
                    model.PostTime = DateTime.Parse(row["PostTime"].ToString());
                }

                if (row["Mobile"] != null && row["Mobile"].ToString() != "")
                {
                    model.Mobile = row["Mobile"].ToString();
                }
                if (row["TeachNo"] != null && row["TeachNo"].ToString() != "")
                {
                    model.TeachNo =row["TeachNo"].ToString();
                }

                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["Description"] != null && row["Description"].ToString() != "")
                {
                    model.Description = row["Description"].ToString();
                }
                if (row["PostOptName"] != null)
                {
                    model.PostOptName = row["PostOptName"].ToString();
                }
                if (row["PostOptId"] != null)
                {
                    model.PostOptId = row["PostOptId"].ToString();
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
			//strSql.Append("select StudentId,IdentityNo,StuName,Gender,School,Title,TelNo,CreateTime,LastModifyTime ");
            strSql.Append("select StudentId,IdentityNo,StuName,Gender,case Gender when 1 then '男' else '女' end as GenderName,School,JobTitle,TelNo,CreateTime,LastModifyTime,Picture,Birthday, Nation, FirstRecord, FirstSchool, LastRecord,LastSchool,PoliticsStatus, Rank, RankTime, Post, PostTime, Mobile, TeachNo, Status, Description,PostOptName,PostOptId  ");
			strSql.Append(" FROM Student ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            strSql.Append(" order by StuName asc, CreateTime desc ");
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
            strSql.Append(" StudentId,IdentityNo,StuName,Gender,School,JobTitle,TelNo,CreateTime,LastModifyTime,Picture,Birthday, Nation, FirstRecord, FirstSchool, LastRecord,LastSchool,PoliticsStatus, Rank, RankTime, Post, PostTime, Mobile, TeachNo, Status, Description,PostOptName,PostOptId  ");
			strSql.Append(" FROM Student ");
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
			strSql.Append("select count(1) FROM Student ");
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
                strSql.Append("order by   " + sort+" "+ order);
			}
			else
			{
                strSql.Append("order by  StuName asc");
			}
            strSql.Append(" )AS Row, a.*,b.Name as GenderName,c.Name as JobTitleName,d.Name as NationName, e.Name as PoliticsStatusName  ");
            strSql.Append(" from Student a left join Dictionaries b   on  a.Gender=b.ID ");
            strSql.Append("  left join Dictionaries c on  a.JobTitle=c.ID  left join Dictionaries d on a.Nation=d.ID  left join Dictionaries e on  a.PoliticsStatus=e.ID ");
            strSql.Append(" where a.Status=1  ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
                strSql.Append(" and " + strWhere);
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
			parameters[0].Value = "Student";
			parameters[1].Value = "StudentId";
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

