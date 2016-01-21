using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

//Please add references
using TrainerEvaluate.Utility.DB;

namespace TrainerEvaluate.DAL
{
    /// <summary>
    /// 数据访问类:SPSchool
    /// </summary>
    public partial class SPSchool
    {
        public SPSchool()
        { }

        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(Guid TeacherId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from School");
            strSql.Append(" where SchoolId=@SchoolId ");
            SqlParameter[] parameters = {
					new SqlParameter("@SchoolId", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = TeacherId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Models.SPSchool model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into School(");
            strSql.Append("SchoolId,SchoolName,SchDisName,RunNatureCode,RunNatureName,SchoolTypeCode,SchoolTypeName,AddrNum,ClassNum,StudentNum, TeacherNum, PartyNum, LegalName, LinkTel, Status, Description,CreatedDate,LastModifyTime,SchDisId)");
            strSql.Append(" values (");
            strSql.Append("@SchoolId,@SchoolName,@SchDisName,@RunNatureCode,@RunNatureName,@SchoolTypeCode,@SchoolTypeName,@AddrNum,@ClassNum,@StudentNum, @TeacherNum, @PartyNum, @LegalName, @LinkTel, @Status, @Description,@CreatedDate,@LastModifyTime,@SchDisId)");
            SqlParameter[] parameters = {
					new SqlParameter("@SchoolId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@SchoolName", SqlDbType.NVarChar,50),
					new SqlParameter("@SchDisName", SqlDbType.NVarChar,50),
					new SqlParameter("@RunNatureCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@RunNatureName", SqlDbType.NVarChar,50),
					new SqlParameter("@SchoolTypeCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@SchoolTypeName", SqlDbType.NVarChar,50),
                    new SqlParameter("@AddrNum", SqlDbType.NVarChar,50),
                    new SqlParameter("@ClassNum", SqlDbType.NVarChar,50),
                    new SqlParameter("@StudentNum", SqlDbType.NVarChar,50),
                    new SqlParameter("@TeacherNum", SqlDbType.NVarChar,50),
                    new SqlParameter("@PartyNum", SqlDbType.NVarChar,50),
                    new SqlParameter("@LegalName", SqlDbType.NVarChar,50),
                    new SqlParameter("@LinkTel", SqlDbType.NVarChar,50),
                    new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@Description", SqlDbType.Text),
                    new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@LastModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@SchDisId", SqlDbType.NVarChar,50)};
            parameters[0].Value = Guid.NewGuid();
            parameters[1].Value = model.SchoolName;
            parameters[2].Value = model.SchDisName;
            parameters[3].Value = model.RunNatureCode;
            parameters[4].Value = model.RunNatureName;
            parameters[5].Value = model.SchoolTypeCode;
            parameters[6].Value = model.SchoolTypeName;
            parameters[7].Value = model.AddrNum;
            parameters[8].Value = model.ClassNum;
            parameters[9].Value = model.StudentNum;
            parameters[10].Value = model.TeacherNum;
            parameters[11].Value = model.PartyNum;
            parameters[12].Value = model.LegalName;
            parameters[13].Value = model.LinkTel;
            parameters[14].Value = model.Status;
            parameters[15].Value = model.Description;
            parameters[16].Value = model.CreatedDate;
            parameters[17].Value = model.LastModifyTime;
            parameters[18].Value = model.SchDisId;

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
        public bool Update(Models.SPSchool model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update School set ");
            strSql.Append("SchoolId=@SchoolId,");
            strSql.Append("SchoolName=@SchoolName,");
            strSql.Append("SchDisId=@SchDisId,");

            strSql.Append("SchDisName=@SchDisName,");
            strSql.Append("RunNatureCode=@RunNatureCode,");
            strSql.Append("RunNatureName=@RunNatureName,");

            strSql.Append("SchoolTypeCode=@SchoolTypeCode,");
            strSql.Append("SchoolTypeName=@SchoolTypeName,");
            strSql.Append("AddrNum=@AddrNum,");
            strSql.Append("ClassNum=@ClassNum,");
            strSql.Append("StudentNum=@StudentNum,");

            strSql.Append("TeacherNum=@TeacherNum,");
            strSql.Append("PartyNum=@PartyNum,");
            strSql.Append("LegalName=@LegalName,");
            strSql.Append("LinkTel=@LinkTel,");
            strSql.Append("Description=@Description, ");
            strSql.Append("LastModifyTime=@LastModifyTime ");
            strSql.Append(" where SchoolId=@SchoolId ");
            

            SqlParameter[] parameters = {
					new SqlParameter("@SchoolId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@SchoolName", SqlDbType.NVarChar,50),
					new SqlParameter("@SchDisName", SqlDbType.NVarChar,50),
					new SqlParameter("@RunNatureCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@RunNatureName", SqlDbType.NVarChar,50),

					new SqlParameter("@SchoolTypeCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@SchoolTypeName", SqlDbType.NVarChar,50),
                    new SqlParameter("@AddrNum", SqlDbType.NVarChar,50),
                    new SqlParameter("@ClassNum", SqlDbType.NVarChar,50),
                    new SqlParameter("@StudentNum", SqlDbType.NVarChar,50),

                    new SqlParameter("@TeacherNum", SqlDbType.NVarChar,50),
                    new SqlParameter("@PartyNum", SqlDbType.NVarChar,50),
                    new SqlParameter("@LegalName", SqlDbType.NVarChar,50),
                    new SqlParameter("@LinkTel", SqlDbType.NVarChar,50),
                    new SqlParameter("@Description", SqlDbType.Text),

                    new SqlParameter("@LastModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@SchDisId", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.SchoolId;
            parameters[1].Value = model.SchoolName;
            parameters[2].Value = model.SchDisName;
            parameters[3].Value = model.RunNatureCode;
            parameters[4].Value = model.RunNatureName;
            parameters[5].Value = model.SchoolTypeCode;
            parameters[6].Value = model.SchoolTypeName;
            parameters[7].Value = model.AddrNum;
            parameters[8].Value = model.ClassNum;
            parameters[9].Value = model.StudentNum;
            parameters[10].Value = model.TeacherNum;
            parameters[11].Value = model.PartyNum;
            parameters[12].Value = model.LegalName;
            parameters[13].Value = model.LinkTel;
            parameters[14].Value = model.Description;
            parameters[15].Value = model.LastModifyTime;
            parameters[16].Value = model.SchDisId;

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
        public bool Delete(Guid SchoolId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update School set Status = 0");
            strSql.Append(" where SchoolId=@SchoolId ");
            SqlParameter[] parameters = {
					new SqlParameter("@SchoolId", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = SchoolId;

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
        public bool DeleteList(string SchoolIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update School set Status = 0 ");
            strSql.Append(" where SchoolId in (" + SchoolIdlist + ")  ");
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
        public Models.SPSchool GetModel(Guid SchoolId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 [SchoolId],[SchoolName],[SchDisId],[SchDisName],[RunNatureCode],[RunNatureName],[SchoolTypeCode],[SchoolTypeName] ");
            strSql.Append(",[AddrNum],[ClassNum],[StudentNum],[TeacherNum],[PartyNum],[LegalName],[LinkTel],[Status],[Description],[CreatedDate],[LastModifyTime]");
            strSql.Append("   from School ");
            strSql.Append(" where SchoolId=@SchoolId ");
            SqlParameter[] parameters = {
					new SqlParameter("@SchoolId", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = SchoolId;

            Models.SPSchool model = new Models.SPSchool();
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
        public Models.SPSchool DataRowToModel(DataRow row)
        {
            Models.SPSchool model = new Models.SPSchool();
            if (row != null)
            {
                if (row["SchoolId"] != null && row["SchoolId"].ToString() != "")
                {
                    model.SchoolId = new Guid(row["SchoolId"].ToString());
                }
                if (row["SchoolName"] != null)
                {
                    model.SchoolName = row["SchoolName"].ToString();
                }
                if (row["SchDisId"] != null)
                {
                    model.SchDisId = row["SchDisId"].ToString();
                }
                if (row["SchDisName"] != null)
                {
                    model.SchDisName = row["SchDisName"].ToString();
                }
                if (row["RunNatureCode"] != null)
                {
                    model.RunNatureCode = row["RunNatureCode"].ToString();
                }
                if (row["RunNatureName"] != null)
                {
                    model.RunNatureName = row["RunNatureName"].ToString();
                }
                if (row["SchoolTypeCode"] != null)
                {
                    model.SchoolTypeCode = row["SchoolTypeCode"].ToString();
                }
                if (row["SchoolTypeName"] != null)
                {
                    model.SchoolTypeName = row["SchoolTypeName"].ToString();
                }
                if (row["AddrNum"] != null)
                {
                    model.AddrNum = row["AddrNum"].ToString();
                }
                if (row["ClassNum"] != null)
                {
                    model.ClassNum = row["ClassNum"].ToString();
                }
                if (row["StudentNum"] != null)
                {
                    model.StudentNum = row["StudentNum"].ToString();
                }
                if (row["TeacherNum"] != null)
                {
                    model.TeacherNum = row["TeacherNum"].ToString();
                }
                if (row["PartyNum"] != null)
                {
                    model.PartyNum = row["PartyNum"].ToString();
                }
                if (row["LegalName"] != null)
                {
                    model.LegalName = row["LegalName"].ToString();
                }
                if (row["LinkTel"] != null)
                {
                    model.LinkTel = row["LinkTel"].ToString();
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [SchoolId],[SchoolName],[SchDisId],[SchDisName],[RunNatureCode],[RunNatureName],[SchoolTypeCode],[SchoolTypeName] ");
            strSql.Append(" ,[AddrNum],[ClassNum],[StudentNum],[TeacherNum],[PartyNum],[LegalName],[LinkTel],[Status],[Description],[CreatedDate],[LastModifyTime] ");
            strSql.Append(" FROM School ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by SchoolName asc ");
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
            strSql.Append(" [SchoolId],[SchoolName],[SchDisId],[SchDisName],[RunNatureCode],[RunNatureName],[SchoolTypeCode],[SchoolTypeName] ");
            strSql.Append(" ,[AddrNum],[ClassNum],[StudentNum],[TeacherNum],[PartyNum],[LegalName],[LinkTel],[Status],[Description],[CreatedDate],[LastModifyTime] ");

            strSql.Append(" FROM School ");
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
            strSql.Append("select count(1) FROM School ");
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
                strSql.Append("order by T.SchoolName asc");
            }
            strSql.Append(")AS Row, T.*  from   ");
            strSql.Append(" ( select * from School where Status=1  ) ");
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

        /// <summary>
        /// 是否存在该记录,根据学校名称
        /// </summary>
        public bool ExistsBySchoolName(string SchoolName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from School");
            strSql.Append(" where SchoolName=@SchoolName ");
            SqlParameter[] parameters = {
					new SqlParameter("@SchoolName", SqlDbType.NVarChar,50)	};
            parameters[0].Value = SchoolName;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        #endregion  ExtensionMethod
    }
}

