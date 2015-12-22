using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrainerEvaluate.Models
{
    public class QuestionInfo   
    {

        /// <summary>
        /// Id
        /// </summary>		
        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 问卷名称
        /// </summary>		
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 班级课程编号
        /// </summary>		
        private Guid _classcourseid;
        public Guid ClassCourseID
        {
            get { return _classcourseid; }
            set { _classcourseid = value; }
        }
        /// <summary>
        /// 状态
        /// </summary>		
        private int _status;
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary>
        /// CreatedTime
        /// </summary>		
        private DateTime _createdtime;
        public DateTime CreatedTime
        {
            get { return _createdtime; }
            set { _createdtime = value; }
        }  
         
        
        private DateTime _starttime;
        public DateTime StartTime
        {
            get { return _starttime; }
            set { _starttime = value; }
        }


        private DateTime _endTime;
        public DateTime EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

    }
}
