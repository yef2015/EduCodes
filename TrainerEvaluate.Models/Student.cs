using System;

namespace TrainerEvaluate.Models
{
	/// <summary>
	/// 问卷管理
	/// </summary>
	[Serializable]
	public partial class Student
	{
		public Student()
		{}
		#region Model
		private Guid _studentid;
		private string _identityno;
		private string _stuname;
		private int? _gender;
		private string _school;
		private int? _title;
		private string _telno;
		private DateTime _createtime;
		private DateTime _lastmodifytime;
        private string _picture;
        private DateTime? _birthday;
        private int _nation;
        private string _firstRecord;
        private string _firstSchool;
        private string _lastRecord;
        private string _lastSchool;
        private int _politicsStatus;
        private string _rank;
        private string _rankTime;
        private string _post;
        private string _postTime;
        private string _mobile;
        private string _teachNo;
        private int _status;
        private string _description;
        private string _postoptname;
        private string _postoptid;
        private string _managework;
            
		/// <summary>
		/// 编号
		/// </summary>
		public Guid StudentId
		{
			set{ _studentid=value;}
			get{return _studentid;}
		}
		/// <summary>
		/// 身份证号
		/// </summary>
		public string IdentityNo
		{
			set{ _identityno=value;}
			get{return _identityno;}
		}
		/// <summary>
		/// 姓名
		/// </summary>
		public string StuName
		{
			set{ _stuname=value;}
			get{return _stuname;}
		}
		/// <summary>
		/// 性别
		/// </summary>
		public int? Gender
		{
			set{ _gender=value;}
			get{return _gender;}
		}
		/// <summary>
		/// 所在学校
		/// </summary>
		public string School
		{
			set{ _school=value;}
			get{return _school;}
		}
		/// <summary>
		/// 职务
		/// </summary>
		public int? JobTitle
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 联系电话
		/// </summary>
		public string TelNo
		{
			set{ _telno=value;}
			get{return _telno;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 最后修改时间
		/// </summary>
		public DateTime LastModifyTime
		{
			set{ _lastmodifytime=value;}
			get{return _lastmodifytime;}
		}

        //照片存放路径
        public string Picture
        {
            set { _picture = value; }
            get { return _picture; }
        }

        //出生年月
        public DateTime? Birthday
        {
            set { _birthday = value; }
            get { return _birthday; }
        }
        //民族
        public int Nation
        {
            set { _nation = value; }
            get { return _nation; }
        }
        //全日制学历
        public string FirstRecord
        {
            set { _firstRecord = value; }
            get { return _firstRecord; }
        }
        //全日制学校
        public string FirstSchool
        {
            set { _firstSchool = value; }
            get { return _firstSchool; }
        }
        public string LastRecord
        {
            set { _lastRecord = value; }
            get { return _lastRecord; }
        }
        public string LastSchool
        {
            set { _lastSchool = value; }
            get { return _lastSchool; }
        }
        public int PoliticsStatus
        {
            set { _politicsStatus = value; }
            get { return _politicsStatus; }
        }
        public string Rank
        {
            set { _rank = value; }
            get { return _rank; }
        }
        public string RankTime
        {
            set { _rankTime = value; }
            get { return _rankTime; }
        }
        public string Post
        {
            set { _post = value; }
            get { return _post; }
        }
        public string PostTime
        {
            set { _postTime = value; }
            get { return _postTime; }
        }
        public string Mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        public string TeachNo
        {
            set { _teachNo = value; }
            get { return _teachNo; }
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
        /// <summary>
        /// 
        /// </summary>
        public string PostOptName
        {
            set { _postoptname = value; }
            get { return _postoptname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PostOptId
        {
            set { _postoptid = value; }
            get { return _postoptid; }
        }
        /// <summary>
        /// 主管工作
        /// </summary>
        public string ManageWork
        {
            set { _managework = value; }
            get { return _managework; }
        }
		#endregion Model

	}
}

