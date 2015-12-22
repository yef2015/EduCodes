using System;

namespace TrainerEvaluate.Models
{
	/// <summary>
	/// 用户角色表
	/// </summary>
	[Serializable]
	public partial class Roles
	{
		public Roles()
		{}
		#region Model
		private Guid _id;
		private string _name;
		private int? _rstatus;
		private string _description;
		private DateTime? _createtime;
		private Guid _createid;
		private string _createname;
		private DateTime? _lastmodifytime;
		private Guid _lastmodifyid;
		private long? _lastmodifyname;
		/// <summary>
		/// 编号
		/// </summary>
		public Guid ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 角色名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 状态
		/// </summary>
		public int? Rstatus
		{
			set{ _rstatus=value;}
			get{return _rstatus;}
		}
		/// <summary>
		/// 描述
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? CreateTime
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
		public long? LastModifyName
		{
			set{ _lastmodifyname=value;}
			get{return _lastmodifyname;}
		}
		#endregion Model

	}
}

