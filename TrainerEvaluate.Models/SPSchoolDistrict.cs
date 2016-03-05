using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrainerEvaluate.Models
{
    /// <summary>
    /// 学区信息表
    /// </summary>
    [Serializable]
    public partial class SPSchoolDistrict
    {
        public SPSchoolDistrict()
        {}

        #region Model
        private Guid _schdisId;
        private string _schdiname;
        private DateTime _createdate;
        private int _status;
        private string _description;
        private string _addressInfo;
        private string _postCode;
        private DateTime _lastmodifytime;
        /// 学区编号
        /// </summary>
        public Guid SchDisId
        {
            set { _schdisId = value; }
            get { return _schdisId; }
        }
        /// <summary>
        /// 学区名称
        /// </summary>
        public string SchDisName
        {
            set { _schdiname = value; }
            get { return _schdiname; }
        }
        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime CreatedDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        public string AddressInfo
        {
            set { _addressInfo = value; }
            get { return _addressInfo; }
        }
        public string PostCode
        {
            set { _postCode = value; }
            get { return _postCode; }
        }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifyTime
        {
            set { _lastmodifytime = value; }
            get { return _lastmodifytime; }
        }
        #endregion Model
    }
}
