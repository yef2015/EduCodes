using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrainerEvaluate.Models
{
    /// <summary>
    /// 学员考勤信息（打卡记录表）
    /// </summary>
    [Serializable]
    public partial class StuAttBaseInfo
    {
        public StuAttBaseInfo()
        { }
        #region Model
        private Guid _id;
        private Guid _studentid;
        private int? _classid;
        private Guid _courseid;
        private DateTime? _theday;
        private DateTime _createtime;
        private Guid _createid;
        private string _createname;
        private string _status;
        private string _remark;
        private bool _isvalid;
        private DateTime? _lastmodifytime;
        private Guid _lastmodifyid;
        /// <summary>
        /// 编号
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
        public Guid CourseID
        {
            set { _courseid = value; }
            get { return _courseid; }
        }
        /// <summary>
        /// 考勤时间
        /// </summary>
        public DateTime? TheDay
        {
            set { _theday = value; }
            get { return _theday; }
        }
        /// <summary>
        /// 数据导入时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 入库人
        /// </summary>
        public Guid CreateId
        {
            set { _createid = value; }
            get { return _createid; }
        }
        /// <summary>
        /// 入库人姓名
        /// </summary>
        public string CreateName
        {
            set { _createname = value; }
            get { return _createname; }
        }
        /// <summary>
        /// 考勤状态
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 备注，用于考勤说明
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
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
        /// 
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
