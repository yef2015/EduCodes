using System;

namespace TrainerEvaluate.Models
{
    /// <summary>
    /// 学校信息表
    /// </summary>
    [Serializable]
    public partial class SPSchool
    {
        public SPSchool()
		{}
		#region Model
        private Guid _schoolid;
        private string _schoolname;
        private string _schdisId;
        private string _schdisname;
        private string _runnaturecode;
        private string _runnaturename;
        private string _schooltypecode;
        private string _schooltypename;
        private string _addrnum;
        private string _classnum;
        private string _studentnum;
        private string _teachernum;
        private string _partynum;
        private string _legalname;
        private string _linktel;
		private DateTime _createdate;
		private DateTime _lastmodifytime;
        private int _status;
        private string _description;
		/// <summary>
		/// 学校编号
		/// </summary>
        public Guid SchoolId
		{
			set{ _schoolid=value;}
            get { return _schoolid; }
		}
		/// <summary>
		/// 名称
		/// </summary>
        public string SchoolName
		{
			set{ _schoolname=value;}
            get { return _schoolname; }
		}
		/// <summary>
		/// 所属学区编号
		/// </summary>
        public string SchDisId
		{
			set{ _schdisId=value;}
            get { return _schdisId; }
		}
		/// <summary>
		/// 所属学区名称
		/// </summary>
        public string SchDisName
		{
			set{ _schdisname=value;}
            get { return _schdisname; }
		}

        /// <summary>
        /// 办学性质编号
        /// </summary>
        public string RunNatureCode
        {
            set { _runnaturecode = value; }
            get { return _runnaturecode; }
        }

        /// <summary>
        /// 办学性质名称
        /// </summary>
        public string RunNatureName
        {
            set { _runnaturename = value; }
            get { return _runnaturename; }
        }

        /// <summary>
        /// 学校类型编号
        /// </summary>
        public string SchoolTypeCode
        {
            set { _schooltypecode = value; }
            get { return _schooltypecode; }
        }

        /// <summary>
        /// 学校类型名称
        /// </summary>
        public string SchoolTypeName
        {
            set { _schooltypename = value; }
            get { return _schooltypename; }
        }

        /// <summary>
        /// 校址数
        /// </summary>
        public string AddrNum
        {
            set { _addrnum = value; }
            get { return _addrnum; }
        }

        /// <summary>
        /// 班级数
        /// </summary>
        public string ClassNum
        {
            set { _classnum = value; }
            get { return _classnum; }
        }

        /// <summary>
        /// 学生数
        /// </summary>
        public string StudentNum
        {
            set { _studentnum = value; }
            get { return _studentnum; }
        }

        /// <summary>
        /// 教师数
        /// </summary>
        public string TeacherNum
        {
            set { _teachernum = value; }
            get { return _teachernum; }
        }

        /// <summary>
        /// 党员数
        /// </summary>
        public string PartyNum
        {
            set { _partynum = value; }
            get { return _partynum; }
        }

        /// <summary>
        /// 法人名称
        /// </summary>
        public string LegalName
        {
            set { _legalname = value; }
            get { return _legalname; }
        }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string LinkTel
        {
            set { _linktel = value; }
            get { return _linktel; }
        }

		/// <summary>
		/// 创建时间
		/// </summary>
        public DateTime CreatedDate
		{
			set{ _createdate=value;}
            get { return _createdate; }
		}
		/// <summary>
		/// 最后修改时间
		/// </summary>
		public DateTime LastModifyTime
		{
			set{ _lastmodifytime=value;}
			get{return _lastmodifytime;}
		}
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
		#endregion Model

	}
}
