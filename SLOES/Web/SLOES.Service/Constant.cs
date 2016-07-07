using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SLOES.Service
{
    /// <summary>
    /// 常用的常量定义类
    /// </summary>
    public class Constant
    {
        #region Common

        /// <summary>
        /// 老师账号加密Salt key
        /// </summary>
        public const string TEACHER_SALT_KEY = "#_&_SLOES_TEACHER_SALT&_#";

        /// <summary>
        /// 学生账号加密Salt key
        /// </summary>
        public const string STUDENT_SALT_KEY = "#_&_SLOES_STUDENT_SALT&_#";

        /// <summary>
        /// 重置老师密码后的密码
        /// </summary>
        public const string TEACHER_RESET_DEFAULT_PASSWORD = "123456";

        #endregion

        #region Debug

        /// <summary>
        /// Debug log message when start invoke service method.
        /// </summary>
        public const string DEBUG_START = "Start invoke method";

        /// <summary>
        /// Debug log message when end invoke service method.
        /// </summary>
        public const string DEBUG_END = "End invoke method";

        /// <summary>
        /// Debug log message when argument is invalid.
        /// </summary>
        public const string DEBUG_ARG_ERROR_FORMATER = "Invalid argument with error message:{0} ";

        #endregion
    }
}
