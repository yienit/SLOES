using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace SLOES.Model
{
    /// <summary>
    /// 老师操作记录实体类
    /// </summary>
    public class TeacherDoRecord
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// 老师主键ID
        /// </summary>
        [JsonProperty("teacher_id")]
        public int TeacherID { get; set; }

        /// <summary>
        /// 老师姓名
        /// </summary>
        [JsonProperty("teacher_name")]
        public string TeacherName { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [JsonProperty("do_time")]
        public DateTime DoTime { get; set; }

        /// <summary>
        /// 操作名称
        /// </summary>
        [JsonProperty("do_name")]
        public string DoName { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        [JsonProperty("do_content")]
        public string DoContent { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty("remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [JsonProperty("add_time")]
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 是否已删除 (0: 未删除  1: 已删除)
        /// </summary>
        [JsonIgnore]
        public int IsDeleted { get; set; }
    }
}
