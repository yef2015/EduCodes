using System;

namespace TrainerEvaluate.Models
{
/// <summary>
	/// 课程教师关系表
	/// </summary>
	[Serializable]
	public partial class CourseTeacher
	{
        public CourseTeacher()
		{}
		#region Model
        private Guid _rid;
        private Guid _courseid;
        private string _coursname;
        private string _teacherid;
        private string _teachername;
        private string _teacherplace;
        private string _classid;
        private string _classname;
        private DateTime? _startdate;
        private DateTime? _finishdate;
        private DateTime? _createtime;

        /// <summary>
        /// 主键id
        /// </summary>
        public Guid RId
        {
            set { _rid = value; }
            get { return _rid; }
        }
        /// <summary>
        /// 课程主键
        /// </summary>
        public Guid CourseId
        {
            set { _courseid = value; }
            get { return _courseid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CoursName
        {
            set { _coursname = value; }
            get { return _coursname; }
        }
        /// <summary>
        /// 教师编号
        /// </summary>
        public string TeacherId
        {
            set { _teacherid = value; }
            get { return _teacherid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TeacherName
        {
            set { _teachername = value; }
            get { return _teachername; }
        }

        public string teacherplace
        {
            set { _teacherplace = value; }
            get { return _teacherplace; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ClassId
        {
            set { _classid = value; }
            get { return _classid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ClassName
        {
            set { _classname = value; }
            get { return _classname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? FinishDate
        {
            set { _finishdate = value; }
            get { return _finishdate; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
		#endregion Model

	}
}

