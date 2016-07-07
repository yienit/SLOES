using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SLOES.Model
{
    /// <summary>
    /// 老师账号级别枚举
    /// </summary>
    public enum TeacherLevel
    {
        /// <summary>
        /// 题库管理员
        /// </summary>
        [Description("题库管理员")]
        ItemAdmin = 0,

        /// <summary>
        /// 系统管理员
        /// </summary>
        [Description("系统管理员")]
        SystemAdmin = 1
    }
}
