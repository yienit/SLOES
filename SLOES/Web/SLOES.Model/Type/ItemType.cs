using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SLOES.Model
{
    /// <summary>
    /// 题型枚举
    /// </summary>
    public enum ItemType
    {
        /// <summary>
        /// 单选题
        /// </summary>
        Single = 1,

        /// <summary>
        /// 多选题
        /// </summary>
        Multiple = 2,

        /// <summary>
        /// 判断题
        /// </summary>
        Judge = 3,

        /// <summary>
        /// 填空题
        /// </summary>
        Blank = 4,

        /// <summary>
        /// 简答题
        /// </summary>
        Word = 5
    }
}
