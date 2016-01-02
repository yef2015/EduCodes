//using System.Linq;
using System.ComponentModel;

namespace TrainerEvaluate.Utility
{


    public enum EnumDeleteState      
    {
        [Description("删除")] 
        Deleted = 1,

        [Description("未删除")]
        NotDeleted = 0 

    }



    /// <summary>
    /// （1-学生2-教师3-管理员）
    /// </summary>
    public enum EnumUserRole
    {
        [Description("学生")] Student = 1,

        [Description("教师")] Teacher = 2,

        [Description("管理员")] Admin = 3,

        [Description("课程管理员")] CourseAdmin = 4

    }

    /// <summary>
    /// （1-学生2-教师3-管理员）
    /// </summary>
    public enum EnumQuestionType
    {
        [Description("不带答案")]
        NoAnswer = 1,

        [Description("答案是单选")]
        AnswerRadio = 2,

        [Description("答案是文本框")]
        AnswerText = 3
    }

    public enum EnumQuestionState
    {
        [Description("未提交")]
        Start = 0,

        [Description("已提交")]
        Submit = 1 
    }
    

    public static  class Common
    {
        public const string defaultPwd = "000000";
    }
}
