using System;

namespace TrainerEvaluate.Models
{
    /// <summary>
    /// QuestionnaireSurvey:实体类(问卷的试题部分)
    /// </summary>
    [Serializable]
    public partial class QuestionnaireSurvey
    {
        public QuestionnaireSurvey()
        { }
        #region Model
        private Guid _id;
        private string _showname;
        private string _showtype;
        private string _showorder;
        private string _showcode;
        private string _showid;
        private string _opttext1;
        private string _opttype1;
        private string _optvalue1;
        private string _opttext2;
        private string _opttype2;
        private string _optvalue2;
        private string _opttext3;
        private string _opttype3;
        private string _optvalue3;
        private string _opttext4;
        private string _opttype4;
        private string _optvalue4;
        private string _treenode;
        private DateTime? _createtime;
        private bool _isdelete;
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
        public string ShowName
        {
            set { _showname = value; }
            get { return _showname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShowType
        {
            set { _showtype = value; }
            get { return _showtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShowOrder
        {
            set { _showorder = value; }
            get { return _showorder; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShowCode
        {
            set { _showcode = value; }
            get { return _showcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShowId
        {
            set { _showid = value; }
            get { return _showid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OptText1
        {
            set { _opttext1 = value; }
            get { return _opttext1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OptType1
        {
            set { _opttype1 = value; }
            get { return _opttype1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OptValue1
        {
            set { _optvalue1 = value; }
            get { return _optvalue1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OptText2
        {
            set { _opttext2 = value; }
            get { return _opttext2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OptType2
        {
            set { _opttype2 = value; }
            get { return _opttype2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OptValue2
        {
            set { _optvalue2 = value; }
            get { return _optvalue2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OptText3
        {
            set { _opttext3 = value; }
            get { return _opttext3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OptType3
        {
            set { _opttype3 = value; }
            get { return _opttype3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OptValue3
        {
            set { _optvalue3 = value; }
            get { return _optvalue3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OptText4
        {
            set { _opttext4 = value; }
            get { return _opttext4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OptType4
        {
            set { _opttype4 = value; }
            get { return _opttype4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OptValue4
        {
            set { _optvalue4 = value; }
            get { return _optvalue4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TreeNode
        {
            set { _treenode = value; }
            get { return _treenode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsDelete
        {
            set { _isdelete = value; }
            get { return _isdelete; }
        }
        #endregion Model

    }
}