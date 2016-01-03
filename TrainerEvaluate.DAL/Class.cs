using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

//Please add references
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.DAL
{
	/// <summary>
	/// 数据访问类:Class   
	/// </summary>
	public partial class Class
	{
		public Class()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ClassId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Class");
			strSql.Append(" where ID=@ClassId ");
			SqlParameter[] parameters = {new SqlParameter("@ClassId", SqlDbType.Int,16)};
			parameters[0].Value = ClassId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
        public bool Add(Models.Class model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Class(");
            strSql.Append("ID,Name,Status,Object,Description,StartDate,FinishDate,Students,Point,PointType,Teacher,Area,Level, Type, CreatedTime,YearLevel)");
			strSql.Append(" values (");
            strSql.Append("@ClassId,@Name,@Status,@Object,@Description,@StartDate,@FinishDate,@Students,@Point,@PointType, @Teacher, @Area, @Level, @Type, @CreatedTime,@YearLevel)");
			SqlParameter[] parameters = {
					new SqlParameter("@ClassId", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.VarChar,50),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Object", SqlDbType.Text),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@FinishDate", SqlDbType.DateTime),
					new SqlParameter("@Students", SqlDbType.Int,4),
					new SqlParameter("@Point", SqlDbType.Int,4),
                    new SqlParameter("@PointType", SqlDbType.Int,4),
                    new SqlParameter("@Teacher", SqlDbType.VarChar, 50),
                    new SqlParameter("@Area", SqlDbType.Int, 4),
                    new SqlParameter("@Level", SqlDbType.Int, 4),
                    new SqlParameter("@Type", SqlDbType.Int, 4),
                    new SqlParameter("@CreatedTime", SqlDbType.DateTime),
                    new SqlParameter("@YearLevel", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.ID;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Status;
            parameters[3].Value = model.Object;
            parameters[4].Value = model.Description;
            parameters[5].Value = model.StartDate;
            parameters[6].Value = model.FinishDate;
            parameters[7].Value = model.Students;
            parameters[8].Value = model.Point;
            parameters[9].Value = model.PointType;
            parameters[10].Value = model.Teacher;
            parameters[11].Value = model.Area;
            parameters[12].Value = model.Level;
            parameters[13].Value = model.Type;
            parameters[14].Value = model.CreatedTime;
            parameters[15].Value = model.YearLevel;

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
        public bool Update(Models.Class model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Class set ");
            strSql.Append("Status=@Status,");
            strSql.Append("Name=@Name,");
            strSql.Append("Object=@Object,");
            strSql.Append("Description=@Description,");
            strSql.Append("StartDate=@StartDate,");
            strSql.Append("FinishDate=@FinishDate,");
            strSql.Append("Students=@Students,");
            strSql.Append("Point=@Point,");
            strSql.Append("PointType=@PointType,");
            strSql.Append("Teacher=@Teacher,");
            strSql.Append("Area=@Area,");
            strSql.Append("Level=@Level,");
            strSql.Append("CreatedTime=@CreatedTime,");
            strSql.Append("Type=@Type,");
            strSql.Append("YearLevel=@YearLevel");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.VarChar,50),
					new SqlParameter("@Object", SqlDbType.Text),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@FinishDate", SqlDbType.DateTime),
					new SqlParameter("@Students", SqlDbType.Int,4),
					new SqlParameter("@Point", SqlDbType.Int,4),
					new SqlParameter("@PointType", SqlDbType.Int,4),
					new SqlParameter("@Teacher", SqlDbType.NVarChar,200),
					new SqlParameter("@Area", SqlDbType.Int,4),
					new SqlParameter("@Level", SqlDbType.Int,4),
					new SqlParameter("@CreatedTime", SqlDbType.DateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@YearLevel", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.Status;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Object;
            parameters[3].Value = model.Description;
            parameters[4].Value = model.StartDate;
            parameters[5].Value = model.FinishDate;
            parameters[6].Value = model.Students;
            parameters[7].Value = model.Point;
            parameters[8].Value = model.PointType;
            parameters[9].Value = model.Teacher;
            parameters[10].Value = model.Area;
            parameters[11].Value = model.Level;
            parameters[12].Value = model.CreatedTime;
            parameters[13].Value = model.Type;
            parameters[14].Value = model.YearLevel;
            parameters[15].Value = model.ID;

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
		public bool Delete(int ClassID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update Class set Status = 0 ");
            strSql.Append(" where ID=@ClassID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ClassID", SqlDbType.Int,4)			};
            parameters[0].Value = ClassID;

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
		public bool DeleteList(string  ClassIdList)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update Class set Status = 0 ");
            strSql.Append(" where ID in (" + ClassIdList + ")  ");
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
        public Models.Class GetModel(int ClassId)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select top 1 ID,Name,Status,Object,Description,StartDate,FinishDate,Students,Point,PointType,Teacher,Area,Level, Type, CreatedTime,YearLevel from Class ");
            strSql.Append(" where ID=@ClassId and Status = 1");
			SqlParameter[] parameters = {
					new SqlParameter("@ClassId", SqlDbType.Int,4)			};
            parameters[0].Value = ClassId;

            Models.Class model = new Models.Class();
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
        public Models.Class DataRowToModel(DataRow row)
		{
            Models.Class model = new Models.Class();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
                    model.ID = int.Parse(row["ID"].ToString());
				}
				if(row["Name"]!=null)
				{
					model.Name=row["Name"].ToString();
				}
                if (row["Status"] != null && row["Status"].ToString() != "")
				{
                    model.Status = int.Parse(row["Status"].ToString());
				}
				if(row["Object"]!=null)
				{
					model.Object=row["Object"].ToString();
				}
				if(row["Description"]!=null)
				{
                    model.Description = row["Description"].ToString();
				}
                if (row["StartDate"] != null && row["StartDate"].ToString() != "")
				{
                    model.StartDate = DateTime.Parse(row["StartDate"].ToString());
				}
                if (row["FinishDate"] != null && row["FinishDate"].ToString() != "")
                {
                    model.FinishDate = DateTime.Parse(row["FinishDate"].ToString());
                }
                if (row["Students"] != null && row["Students"].ToString() != "")
				{
                    model.Students = int.Parse(row["Students"].ToString());
				}
				if(row["Point"]!=null && row["Point"].ToString()!="")
				{
					model.Point=int.Parse(row["point"].ToString());
				}

                if (row["PointType"] != null && row["PointType"].ToString() != "")
                {
                    model.PointType = int.Parse(row["PointType"].ToString());
                }

                if (row["Teacher"] != null)
                {
                    model.Teacher = row["Teacher"].ToString();
                }
                if (row["Area"] != null && row["Area"].ToString() != "")
                {
                    model.Area = int.Parse(row["Area"].ToString());
                }
                if (row["Level"] != null && row["Level"].ToString() != "")
                {
                    model.Level = int.Parse(row["Level"].ToString());
                }
                if (row["Type"] != null && row["Type"].ToString() != "")
                {
                    model.Type = int.Parse(row["Type"].ToString());
                }
                if (row["CreatedTime"] != null && row["CreatedTime"].ToString() != "")
                {
                    model.CreatedTime = DateTime.Parse(row["CreatedTime"].ToString());
                }
                if (row["YearLevel"] != null && row["YearLevel"].ToString() != "")
                {
                    model.YearLevel = row["YearLevel"].ToString();
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
            strSql.Append("select ID,Name,Status,Object,Description,StartDate,FinishDate,Students,Point,PointType,Teacher,Area,Level, Type, CreatedTime,YearLevel ");
			strSql.Append(" FROM Class ");
            strSql.Append(" where Status = 1 ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" and "+strWhere);
			}
            strSql.Append(" order by Name asc, CreatedTime desc ");
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
            strSql.Append("ID,Name,Status,Object,Description,StartDate,FinishDate,Students,Point,PointType,Teacher,Area,Level, Type, CreatedTime,YearLevel");
			strSql.Append(" FROM Class ");
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
			strSql.Append("select count(1) FROM Class ");
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
            strSql.Append("SELECT ID,Name,Status,Object,Description,StartDate,FinishDate,Students,Point,PointType,Teacher,Area,Level, Type, CreatedTime,YearLevel FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(sort.Trim()))
			{
                strSql.Append("order by T." + sort+" "+ order);
			}
			else
			{
                strSql.Append("order by T.Name asc");
			}
			strSql.Append(")AS Row, T.*  from Class T ");
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

