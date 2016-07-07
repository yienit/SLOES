using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SLOES.Core
{
    /// <summary>
    /// 操作类型枚举(用于操作日志记录)
    /// </summary>
    public enum DoActionType
    {
        #region Teacher

        /// <summary>
        /// 添加老师
        /// </summary>
        AddTeacher,

        /// <summary>
        /// 更新老师
        /// </summary>
        UpdateTeacher,

        /// <summary>
        /// 删除老师
        /// </summary>
        DeleteTeacher,

        /// <summary>
        /// 删除老师(批量)
        /// </summary>
        DeleteTeacherInBatch,

        #endregion

        #region Chapter

        /// <summary>
        /// 添加章节
        /// </summary>
        AddChapter,

        /// <summary>
        /// 更新章节
        /// </summary>
        UpdateChapter,

        /// <summary>
        /// 上调章节序号
        /// </summary>
        UpChapter,

        /// <summary>
        /// 下调章节序号
        /// </summary>
        DownChapter,

        /// <summary>
        /// 删除章节
        /// </summary>
        DeleteChapter,

        /// <summary>
        /// 删除章节(批量)
        /// </summary>
        DeleteChapterInBatch,

        #endregion

        #region Single

        /// <summary>
        /// 添加单选题
        /// </summary>
        AddSingle,

        /// <summary>
        /// 更新单选题
        /// </summary>
        UpdateSingle,

        /// <summary>
        /// 删除单选题
        /// </summary>
        DeleteSingle,

        /// <summary>
        /// 删除单选题(批量)
        /// </summary>
        DeleteSingleInBatch,

        #endregion

        #region Multiple

        /// <summary>
        /// 添加多选题
        /// </summary>
        AddMultiple,

        /// <summary>
        /// 更新多选题
        /// </summary>
        UpdateMultiple,

        /// <summary>
        /// 删除多选题
        /// </summary>
        DeleteMultiple,

        /// <summary>
        /// 删除多选题(批量)
        /// </summary>
        DeleteMultipleInBatch,

        #endregion

        #region Judge

        /// <summary>
        /// 添加判断题
        /// </summary>
        AddJudge,

        /// <summary>
        /// 更新判断题
        /// </summary>
        UpdateJudge,

        /// <summary>
        /// 删除判断题
        /// </summary>
        DeleteJudge,

        /// <summary>
        /// 删除判断题(批量)
        /// </summary>
        DeleteJudgeInBatch,

        #endregion

        #region Blank

        /// <summary>
        /// 添加填空题
        /// </summary>
        AddBlank,

        /// <summary>
        /// 更新填空题
        /// </summary>
        UpdateBlank,

        /// <summary>
        /// 删除填空题
        /// </summary>
        DeleteBlank,

        /// <summary>
        /// 删除填空题(批量)
        /// </summary>
        DeleteBlankInBatch,

        #endregion

        #region Word

        /// <summary>
        /// 添加简答题
        /// </summary>
        AddWord,

        /// <summary>
        /// 更新简答题
        /// </summary>
        UpdateWord,

        /// <summary>
        /// 删除简答题
        /// </summary>
        DeleteWord,

        /// <summary>
        /// 删除简答题(批量)
        /// </summary>
        DeleteWordInBatch,

        #endregion
    }
}
