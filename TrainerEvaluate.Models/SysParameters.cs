using System;

namespace TrainerEvaluate.Models
{
	/// <summary>
	/// 系统参数表
	///设置系统默认的一些参数，目前只启用学员评估课程的起止时间，后期根据情况进行增减。
	/// </summary>
	[Serializable]
	public partial class SysParameters
	{
		public SysParameters()
		{}
		#region Model
		private Guid _id;
		private string _parameter;
		private int? _ptype;
		private DateTime? _starttime;
		private DateTime? _endtime;
		private int? _pstatus;
		private DateTime _createtime;
		private Guid _createid;
		private string _createname;
		private DateTime? _lastmodifytime;
		private Guid _lastmodifyid;
		private string _lastmodifyname;
		/// <summary>
		/// 编号
		/// </summary>
		public Guid ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 参数名称
		/// </summary>
		public string Parameter
		{
			set{ _parameter=value;}
			get{return _parameter;}
		}
		/// <summary>
		/// 1：时间，2：状态
		/// </summary>
		public int? Ptype
		{
			set{ _ptype=value;}
			get{return _ptype;}
		}
		/// <summary>
		/// 开始时间
		/// </summary>
		public DateTime? StartTime
		{
			set{ _starttime=value;}
			get{return _starttime;}
		}
		/// <summary>
		/// 结束时间
		/// </summary>
		public DateTime? EndTime
		{
			set{ _endtime=value;}
			get{return _endtime;}
		}
		/// <summary>
		/// 状态
		/// </summary>
		public int? Pstatus
		{
			set{ _pstatus=value;}
			get{return _pstatus;}
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
		/// 创建人Id
		/// </summary>
		public Guid CreateId
		{
			set{ _createid=value;}
			get{return _createid;}
		}
		/// <summary>
		/// 创建人名称
		/// </summary>
		public string CreateName
		{
			set{ _createname=value;}
			get{return _createname;}
		}
		/// <summary>
		/// 最后修改时间
		/// </summary>
		public DateTime? LastModifyTime
		{
			set{ _lastmodifytime=value;}
			get{return _lastmodifytime;}
		}
		/// <summary>
		/// 最后一次修改人id
		/// </summary>
		public Guid LastModifyId
		{
			set{ _lastmodifyid=value;}
			get{return _lastmodifyid;}
		}
		/// <summary>
		/// 最后一次修改人姓名
		/// </summary>
		public string LastModifyName
		{
			set{ _lastmodifyname=value;}
			get{return _lastmodifyname;}
		}
		#endregion Model

	}
}

