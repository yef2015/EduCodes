using System;

namespace TrainerEvaluate.Models
{
	/// <summary>
	/// 数据字典表
	/// </summary>
	[Serializable]
	public partial class Dictionaries
	{
        public Dictionaries()
        { }
        #region Model
        private int _id;
        private string _name;
        private string _dtype;
        private int _dstatus;
        private DateTime _createtime;
        private Guid _createid;
        private string _createname;
        private DateTime? _lastmodifytime;
        private Guid _lastmodifyid;
        private string _lastmodifyname;
        /// <summary>
        /// 编号
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 字典名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 字典类型
        /// </summary>
        public string DType
        {
            set { _dtype = value; }
            get { return _dtype; }
        }
        /// <summary>
        /// 状态 是否有效
        /// </summary>
        public int Dstatus
        {
            set { _dstatus = value; }
            get { return _dstatus; }
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
        /// 创建人Id
        /// </summary>
        public Guid CreateId
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
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime
        {
            set { _lastmodifytime = value; }
            get { return _lastmodifytime; }
        }
        /// <summary>
        /// 最后一次修改人id
        /// </summary>
        public Guid LastModifyId
        {
            set { _lastmodifyid = value; }
            get { return _lastmodifyid; }
        }
        /// <summary>
        /// 最后一次修改人姓名
        /// </summary>
        public string LastModifyName
        {
            set { _lastmodifyname = value; }
            get { return _lastmodifyname; }
        }
        #endregion Model

	}
}

