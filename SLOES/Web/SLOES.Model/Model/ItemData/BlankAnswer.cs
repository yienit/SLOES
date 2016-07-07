﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Drawing;

namespace SLOES.Model
{
    /// <summary>
    /// 填空题答案信息实体类
    /// </summary>
    public class BlankAnswer
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// 填空题主键ID
        /// </summary>
        [JsonProperty("blankitem_id")]
        public int BlankItemID { get; set; }

        /// <summary>
        /// 答案序号
        /// </summary>
        [JsonProperty("answer_index")]
        public int AnswerIndex { get; set; }

        /// <summary>
        /// 答案
        /// </summary>
        [JsonProperty("answer")]
        public string Answer { get; set; }

        /// <summary>
        /// 注解
        /// </summary>
        [JsonProperty("annotation")]
        public string Annotation { get; set; }

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
