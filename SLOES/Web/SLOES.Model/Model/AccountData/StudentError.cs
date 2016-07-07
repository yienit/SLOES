﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace SLOES.Model
{
    /// <summary>
    /// 学生错题信息实体类
    /// </summary>
    public class StudentError
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
        /// 题目类型
        /// </summary>
        [JsonProperty("item_type")]
        public ItemType ItemType { get; set; }

        /// <summary>
        /// 是否为试卷试题 (0：不是  1：是)
        /// </summary>
        [JsonProperty("is_paper_item")]
        public int IsPaperItem { get; set; }

        /// <summary>
        /// 题目主键ID
        /// </summary>
        [JsonProperty("item_id")]
        public int ItemID { get; set; }

        /// <summary>
        /// 用户笔记
        /// </summary>
        [JsonProperty("note")]
        public string Note { get; set; }

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
