using System;

namespace TrainerEvaluate.Models
{
    /// <summary>
    /// NetEnterStudent:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class NetEnterStudent
    {
        public NetEnterStudent()
        { }
        #region Model
        private Guid _guid;
        private Guid _studentid;
        private string _stuname;
        private Guid _netenteryid;
        private string _netentername;
        private string _createid;
        private string _createname;
        private DateTime _createtime = DateTime.Now;
        private DateTime _lastupdatetime = DateTime.Now;
        private bool _isdelete = false;
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Guid
        {
            set { _guid = value; }
            get { return _guid; }
        }
        /// <summary>
        /// 学生id
        /// </summary>
        public Guid StudentId
        {
            set { _studentid = value; }
            get { return _studentid; }
        }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StuName
        {
            set { _stuname = value; }
            get { return _stuname; }
        }
        /// <summary>
        /// 培训班主键
        /// </summary>
        public Guid NetEnteryId
        {
            set { _netenteryid = value; }
            get { return _netenteryid; }
        }
        /// <summary>
        /// 培训班名称
        /// </summary>
        public string NetEnterName
        {
            set { _netentername = value; }
            get { return _netentername; }
        }
        /// <summary>
        /// 创建人Id
        /// </summary>
        public string CreateId
        {
            set { _createid = value; }
            get { return _createid; }
        }
        /// <summary>
        /// 创建人名称
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
        /// 是否删除
        /// </summary>
        public bool IsDelete
        {
            set { _isdelete = value; }
            get { return _isdelete; }
        }
        #endregion Model

    }
}