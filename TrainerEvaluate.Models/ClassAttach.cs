using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrainerEvaluate.Models
{
   public class ClassAttach
    {
        public ClassAttach()
        { }
        #region Model
        private Guid _id;
        private int? _classid;
        private string _name;
        private string _url;
        private string _createUserName;
        private bool _isvalid;
        private Guid _createid;
        private DateTime? _createtime;
        private string _filetype;
        private string _remark;
        /// <summary>
        /// 
        /// </summary>
        public Guid Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ClassId
        {
            set { _classid = value; }
            get { return _classid; }
        }
        /// <summary>
        /// 附件名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid
        {
            set { _isvalid = value; }
            get { return _isvalid; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public Guid CreateId
        {
            set { _createid = value; }
            get { return _createid; }
        }

        public string CreateUserName
        {
            set { _createUserName = value; }
            get { return _createUserName; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType
        {
            set { _filetype = value; }
            get { return _filetype; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model
    }
}
