using System;

namespace TrainerEvaluate.Models
{
	/// <summary>
	/// 问卷管理
	/// </summary>
	[Serializable]
	public partial class Questionnaire
	{
		public Questionnaire()
		{}
		#region Model
        private Guid _questionnaireid;
        private Guid _courseid;
        private string _classid;
        private string _studentid;
        private string _questairid;
        private string _teacherid;
        private string _teachername;
        private int _totalevaluation;
        private int _coursesubject;
        private int _courserich;
        private int? _coursepractical;
        private int? _coursekey;
        private int? _coursedevelop;
        private int? _teacherprepare;
        private int? _teacherlanguage;
        private int? _teacherbearing;
        private int? _teacherstyle;
        private int? _teachercommunication;
        private int? _orgservice;
        private int? _orgtime;
        private int? _orgarrange;
        private Guid _appraiserid;
        private DateTime _appraisertime;
        private string _suggest;
        private int _total;
        private int _totalcousre = 0;
        private int _totalteacher = 0;
        private int _totalorg = 0;
        /// <summary>
        /// 问卷Id
        /// </summary>
        public Guid QuestionnaireId
        {
            set { _questionnaireid = value; }
            get { return _questionnaireid; }
        }
        /// <summary>
        /// 课程Id
        /// </summary>
        public Guid CourseId
        {
            set { _courseid = value; }
            get { return _courseid; }
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
        /// 学生Id
        /// </summary>
        public string StudentId
        {
            set { _studentid = value; }
            get { return _studentid; }
        }
        /// <summary>
        /// 问卷id
        /// </summary>
        public string QuestairId
        {
            set { _questairid = value; }
            get { return _questairid; }
        }
        /// <summary>
        /// 
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
        /// <summary>
        /// 总体评价
        /// </summary>
        public int TotalEvaluation
        {
            set { _totalevaluation = value; }
            get { return _totalevaluation; }
        }
        /// <summary>
        /// 课程主题
        /// </summary>
        public int CourseSubject
        {
            set { _coursesubject = value; }
            get { return _coursesubject; }
        }
        /// <summary>
        /// 课程内容丰富
        /// </summary>
        public int CourseRich
        {
            set { _courserich = value; }
            get { return _courserich; }
        }
        /// <summary>
        /// 课程内容实际
        /// </summary>
        public int? CoursePractical
        {
            set { _coursepractical = value; }
            get { return _coursepractical; }
        }
        /// <summary>
        /// 课程内容重点突出
        /// </summary>
        public int? CourseKey
        {
            set { _coursekey = value; }
            get { return _coursekey; }
        }
        /// <summary>
        /// 课程内容有助于个人发展
        /// </summary>
        public int? CourseDevelop
        {
            set { _coursedevelop = value; }
            get { return _coursedevelop; }
        }
        /// <summary>
        /// 讲师准备充分
        /// </summary>
        public int? TeacherPrepare
        {
            set { _teacherprepare = value; }
            get { return _teacherprepare; }
        }
        /// <summary>
        /// 2、语言表达清晰，态度端正
        /// </summary>
        public int? TeacherLanguage
        {
            set { _teacherlanguage = value; }
            get { return _teacherlanguage; }
        }
        /// <summary>
        /// 3、仪表仪容端庄大方，有亲和力
        /// </summary>
        public int? TeacherBearing
        {
            set { _teacherbearing = value; }
            get { return _teacherbearing; }
        }
        /// <summary>
        /// 4、培训方式多样，生动有趣
        /// </summary>
        public int? TeacherStyle
        {
            set { _teacherstyle = value; }
            get { return _teacherstyle; }
        }
        /// <summary>
        /// 5、与学员沟通和互动有效
        /// </summary>
        public int? TeacherCommunication
        {
            set { _teachercommunication = value; }
            get { return _teachercommunication; }
        }
        /// <summary>
        /// 1、培训服务周到细致
        /// </summary>
        public int? OrgService
        {
            set { _orgservice = value; }
            get { return _orgservice; }
        }
        /// <summary>
        /// 2、培训时间安排和控制合理
        /// </summary>
        public int? OrgTime
        {
            set { _orgtime = value; }
            get { return _orgtime; }
        }
        /// <summary>
        /// 3、培训场所、设备安排到位
        /// </summary>
        public int? OrgArrange
        {
            set { _orgarrange = value; }
            get { return _orgarrange; }
        }
        /// <summary>
        /// 评价人Id（学生Id）
        /// </summary>
        public Guid AppraiserId
        {
            set { _appraiserid = value; }
            get { return _appraiserid; }
        }
        /// <summary>
        /// 评价时间
        /// </summary>
        public DateTime AppraiserTime
        {
            set { _appraisertime = value; }
            get { return _appraisertime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Suggest
        {
            set { _suggest = value; }
            get { return _suggest; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Total
        {
            set { _total = value; }
            get { return _total; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int TotalCousre
        {
            set { _totalcousre = value; }
            get { return _totalcousre; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int TotalTeacher
        {
            set { _totalteacher = value; }
            get { return _totalteacher; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int TotalOrg
        {
            set { _totalorg = value; }
            get { return _totalorg; }
        }

        #endregion Model
    }
}

