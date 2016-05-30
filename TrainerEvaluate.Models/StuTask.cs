using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrainerEvaluate.Models
{
    /// <summary>
    /// 学员作业
    /// </summary>
    [Serializable]
    public partial class StuTask
    {
        public StuTask()
        { }
        #region Model
        private Guid _id;
        private Guid _studentid;
        private int? _classid;
        private Guid _courseid;
        private DateTime? _createtime;
        private string _taskurl;
        private string _fileType;
        private string _remark;
        private string _score;
        private Guid _teacherid;
        private string _teachername;
        private DateTime? _scoretime;
        private string _taskname;
        private bool _isvalid;
        private DateTime? _lastmodifytime;
        private Guid _lastmodifyid;
        /// <summary>
        /// 
        /// </summary>
        public Guid Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 学员编号
        /// </summary>
        public Guid StudentId
        {
            set { _studentid = value; }
            get { return _studentid; }
        }
        /// <summary>
        /// 班级编号
        /// </summary>
        public int? ClassId
        {
            set { _classid = value; }
            get { return _classid; }
        }
        /// <summary>
        /// 课程编号
        /// </summary>
        public Guid CourseId
        {
            set { _courseid = value; }
            get { return _courseid; }
        }
        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 作业路径
        /// </summary>
        public string TaskUrl
        {
            set { _taskurl = value; }
            get { return _taskurl; }
        }

        public string FileType
        {
            set { _fileType = value; }
            get { return _fileType; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 评分
        /// </summary>
        public string Score
        {
            set { _score = value; }
            get { return _score; }
        }
        /// <summary>
        /// 评价老师
        /// </summary>
        public Guid TeacherId
        {
            set { _teacherid = value; }
            get { return _teacherid; }
        }
        /// <summary>
        /// 评价老师
        /// </summary>
        public string TeacherName
        {
            set { _teachername = value; }
            get { return _teachername; }
        }
        /// <summary>
        /// 评价时间
        /// </summary>
        public DateTime? ScoreTime
        {
            set { _scoretime = value; }
            get { return _scoretime; }
        }
        /// <summary>
        /// 作业名称
        /// </summary>
        public string TaskName
        {
            set { _taskname = value; }
            get { return _taskname; }
        }
        /// <summary>
        /// 是否有效：0-否，1-是
        /// </summary>
        public bool IsValid
        {
            set { _isvalid = value; }
            get { return _isvalid; }
        }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime
        {
            set { _lastmodifytime = value; }
            get { return _lastmodifytime; }
        }
        /// <summary>
        /// 最后修改人
        /// </summary>
        public Guid LastModifyId
        {
            set { _lastmodifyid = value; }
            get { return _lastmodifyid; }
        }
        #endregion Model

    }
}
