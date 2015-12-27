using System;

namespace TrainerEvaluate.Models
{
	/// <summary>
	/// 班级管理
	/// </summary>
	[Serializable]
	public partial class Class
	{
		public Class()
		{}
		#region Model
		private int _id;
		private string _name;
		private int _status;
		private string _object;
		private string _description;
		private DateTime? _startDate;
		private DateTime? _finishDate;
        private int? _students;
        private int? _point;
        private int? _pointType;
        private string _teacher;
        private int? _area;
        private int? _level;
        private int? _type;
        private DateTime _createdTime;
        private string _yearlevel;
            
		/// <summary>
		/// 编号
		/// </summary>
		public int ID
		{
			set{ _id = value;}
			get{return _id;}
		}
		/// <summary>
		/// 名称
		/// </summary>
		public string Name
		{
			set{ _name = value;}
			get{return _name;}
		}
		/// <summary>
		/// 状态
		/// </summary>
		public int Status
		{
			set{ _status = value;}
			get{return _status;}
		}
		/// <summary>
		/// 培训对象
		/// </summary>
		public string Object
		{
			set{ _object = value;}
            get { return _object; }
		}
		/// <summary>
		/// 描述
		/// </summary>
		public string Description
		{
			set{ _description = value;}
            get { return _description; }
		}
		/// <summary>
		/// 开始日期
		/// </summary>
		public DateTime? StartDate
		{
			set{ _startDate = value;}
            get { return _startDate; }
		}
		/// <summary>
		/// 结束日期
		/// </summary>
		public DateTime? FinishDate
		{
			set{ _finishDate = value;}
            get { return _finishDate; }
		}
		/// <summary>
		/// 学员人数
		/// </summary>
		public int? Students
		{
			set{ _students = value;}
            get { return _students; }
		}
		/// <summary>
		/// 学时
		/// </summary>
		public int? Point
		{
			set{ _point = value;}
            get { return _point; }
		}

        /// <summary>
        /// 学时类型
        /// </summary>
        public int? PointType
        {
            set { _pointType = value; }
            get { return _pointType; }
        }

        /// <summary>
        /// 班主任
        /// </summary>
        public string Teacher
        {
            set { _teacher = value; }
            get { return _teacher; }
        }

        /// <summary>
        /// 培训范围
        /// </summary>
        public int? Area
        {
            set { _area = value; }
            get { return _area; }
        }

        /// <summary>
        /// 培训级别
        /// </summary>
        public int? Level
        {
            set { _level = value; }
            get { return _level; }
        }

        /// <summary>
        /// 培训类型
        /// </summary>
        public int? Type
        {
            set { _type = value; }
            get { return _type; }
        }
        public DateTime CreatedTime
        {
            set { _createdTime = value; }
            get { return _createdTime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string YearLevel
        {
            set { _yearlevel = value; }
            get { return _yearlevel; }
        }
		#endregion Model

	}
}

