using System;

namespace TrainerEvaluate.Models
{
	/// <summary>
	/// 教师信息表
	/// </summary>
	[Serializable]
	public partial class Teacher
	{
		public Teacher()
		{}
		#region Model
		private Guid _teacherid;
		private string _identityno;
		private string _teachername;
		private int _gender;
        private int? _title;
		private string _dept;
		private DateTime _createtime;
		private DateTime _lastmodifytime;
        private string _picture;
        private string _post;
        private string _research;
        private string _mobile;
        private int _status;
        private string _description;
        private string _researchid;
        private string _researchbigname;
        private string _researchbigid;
		/// <summary>
		/// 教师编号
		/// </summary>
		public Guid TeacherId
		{
			set{ _teacherid=value;}
			get{return _teacherid;}
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
		public string TeacherName
		{
			set{ _teachername=value;}
			get{return _teachername;}
		}
		/// <summary>
		/// 性别
		/// </summary>
		public int Gender
		{
			set{ _gender=value;}
			get{return _gender;}
		}
		/// <summary>
		/// 职称
		/// </summary>
		public int? Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 所在单位
		/// </summary>
		public string Dept
		{
			set{ _dept=value;}
			get{return _dept;}
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

        public string Picture
        {
            set { _picture = value; }
            get { return _picture; }
        }
        public string Post
        {
            set { _post = value; }
            get { return _post; }
        }
        public string Research
        {
            set { _research = value; }
            get { return _research; }
        }
        public string Mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
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
        /// 研究方向小类id
        /// </summary>
        public string ResearchId
        {
            set { _researchid = value; }
            get { return _researchid; }
        }
        /// <summary>
        /// 研究方向大类名称
        /// </summary>
        public string ResearchBigName
        {
            set { _researchbigname = value; }
            get { return _researchbigname; }
        }
        /// <summary>
        /// 研究方向大类id
        /// </summary>
        public string ResearchBigId
        {
            set { _researchbigid = value; }
            get { return _researchbigid; }
        }
		#endregion Model

	}
}

