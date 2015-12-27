using System;

namespace TrainerEvaluate.Models
{
    /// <summary>
    /// 系统用户表
    /// </summary>
    [Serializable]
    public partial class SysUser
    {
        public SysUser()
        { }
        #region Model
        private Guid _userid;
        private int _userrole;
        private string _username;
        private string _userpassword;
        private DateTime? _createtime;
        private string _useraccount;
        private int _status = 1;
        private string _Dept;
        private string _identityno;
        /// <summary>
        /// 用户Id，同学员表、教师表中的id，用于关联
        /// </summary>
        public Guid UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 用户角色（1-学生2-教师3-管理员）
        /// </summary>
        public int UserRole
        {
            set { _userrole = value; }
            get { return _userrole; }
        }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPassWord
        {
            set { _userpassword = value; }
            get { return _userpassword; }
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
        /// 用户账号（登录名）
        /// </summary>
        public string UserAccount
        {
            set { _useraccount = value; }
            get { return _useraccount; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        
        /// <summary>
        /// 用户部门
        /// </summary>
        public string Dept
        {
            set { _Dept = value; }
            get { return _Dept; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string IdentityNo
        {
            set { _identityno = value; }
            get { return _identityno; }
        }
        #endregion Model


	}
}

