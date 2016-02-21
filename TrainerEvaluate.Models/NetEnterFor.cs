using System;

namespace TrainerEvaluate.Models
{
    /// <summary>
    /// 报名信息表
    /// </summary>
    [Serializable]
    public partial class NetEnterFor
    {
        public NetEnterFor()
        { }

        #region Model

        private Guid _guid;
        private string _classid;
        private string _classname;
        private string _trainname;
        private string _explain;
        private DateTime? _begintime;
        private DateTime? _endtime;
        private int _personmax = 0;
        private int _enterfornum = 0;
        private string _createid;
        private string _createname;
        private DateTime _createtime = DateTime.Now;
        private DateTime _lastupdatetime = DateTime.Now;
        private bool _isdelete = false;

        /// <summary>
        /// 
        /// </summary>
        public Guid Guid
        {
            set { _guid = value; }
            get { return _guid; }
        }
        /// <summary>
        /// 班级id
        /// </summary>
        public string ClassId
        {
            set { _classid = value; }
            get { return _classid; }
        }
        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName
        {
            set { _classname = value; }
            get { return _classname; }
        }
        /// <summary>
        /// 培训班名称
        /// </summary>
        public string TrainName
        {
            set { _trainname = value; }
            get { return _trainname; }
        }
        /// <summary>
        /// 说明
        /// </summary>
        public string explain
        {
            set { _explain = value; }
            get { return _explain; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginTime
        {
            set { _begintime = value; }
            get { return _begintime; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }
        /// <summary>
        /// 人数限制
        /// </summary>
        public int PersonMax
        {
            set { _personmax = value; }
            get { return _personmax; }
        }
        /// <summary>
        /// 已报名人数
        /// </summary>
        public int EnterForNum
        {
            set { _enterfornum = value; }
            get { return _enterfornum; }
        }
        /// <summary>
        /// 创建人id
        /// </summary>
        public string CreateId
        {
            set { _createid = value; }
            get { return _createid; }
        }
        /// <summary>
        /// 创建姓名
        /// </summary>
        public string CreateName
        {
            set { _createname = value; }
            get { return _createname; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastUpdateTime
        {
            set { _lastupdatetime = value; }
            get { return _lastupdatetime; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public bool IsDelete
        {
            set { _isdelete = value; }
            get { return _isdelete; }
        }

        #endregion Model

    }
}