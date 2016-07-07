using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace SLOES.Model
{
    /// <summary>
    /// 学生成绩信息实体类
    /// </summary>
    public class StudentScore
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// 学生主键ID
        /// </summary>
        [JsonProperty("student_id")]
        public int StudentID { get; set; }

        /// <summary>
        /// 试卷主键ID
        /// </summary>
        [JsonProperty("paper_id")]
        public int PaperID { get; set; }

        /// <summary>
        /// 用时 (单位：分钟)
        /// </summary>
        [JsonProperty("used_time")]
        public int UsedTime { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        [JsonProperty("score")]
        public int Score { get; set; }

        /// <summary>
        /// 是否需要人工阅卷 (0：否  1：是)
        /// </summary>
        [JsonProperty("is_need_mark")]
        public int IsNeedMark { get; set; }

        /// <summary>
        /// 是否已阅卷 (0：否  1：是)
        /// </summary>
        [JsonProperty("is_finished_mark")]
        public int IsFinishedMark { get; set; }

        /// <summary>
        /// 阅卷老师主键ID(可空)
        /// </summary>
        [JsonProperty("mark_teacher_id")]
        public int MarkTeacherID { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [JsonProperty("add_time")]
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 是否已删除 (0：未删除  1：已删除)
        /// </summary>
        [JsonIgnore]
        public int IsDeleted { get; set; }
    }
}
