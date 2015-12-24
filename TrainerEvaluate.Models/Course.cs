using System;

namespace TrainerEvaluate.Models
{
	/// <summary>
	/// Course:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Course
	{
		public Course()
		{}
		#region Model
		private Guid _courseid;
		private string _coursename;
        private string _teacherid;
		private string _teachername;
		private string _teachtime;
		private string _teachplace;
		private DateTime _creattime;
		private DateTime _lastmodifytime;
        private int? _type;
        private int _status;
        private string _description;
        private string _typename;
        private string _typesmallid;
        private string _typesmallname;
		/// <summary>
		/// 课程编号
		/// </summary>
		public Guid CourseId
		{
			set{ _courseid=value;}
			get{return _courseid;}
		}
		/// <summary>
		/// 课程名称
		/// </summary>
		public string CourseName
		{
			set{ _coursename=value;}
			get{return _coursename;}
		}
		/// <summary>
		/// 授课教师
		/// </summary>
        public string TeacherId
		{
			set{ _teacherid=value;}
			get{return _teacherid;}
		}
		/// <summary>
		/// 教师名称
		/// </summary>
		public string TeacherName
		{
			set{ _teachername=value;}
			get{return _teachername;}
		}
		/// <summary>
		/// 授课时间
		/// </summary>
		public string TeachTime
		{
			set{ _teachtime=value;}
			get{return _teachtime;}
		}
		/// <summary>
		/// 授课地点
		/// </summary>
		public string TeachPlace
		{
			set{ _teachplace=value;}
			get{return _teachplace;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreatTime
		{
			set{ _creattime=value;}
			get{return _creattime;}
		}
		/// <summary>
		/// 最后修改时间
		/// </summary>
		public DateTime LastModifyTime
		{
			set{ _lastmodifytime=value;}
			get{return _lastmodifytime;}
		}

        public int? Type
        {
            set { _type = value; }
            get { return _type; }
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
        public string TypeName
        {
            set { _typename = value; }
            get { return _typename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TypeSmallId
        {
            set { _typesmallid = value; }
            get { return _typesmallid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TypeSmallName
        {
            set { _typesmallname = value; }
            get { return _typesmallname; }
        }
		#endregion Model

	}
}

