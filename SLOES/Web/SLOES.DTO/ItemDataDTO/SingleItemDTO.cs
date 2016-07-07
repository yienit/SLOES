﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using SLOES.Model;

namespace SLOES.DTO
{
    /// <summary>
    /// 单选题DTO(包含所属章节名称)
    /// </summary>
    public class SingleItemDTO
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// 章节主键ID
        /// </summary>
        [JsonProperty("chapter_id")]
        public int ChapterID { get; set; }

        /// <summary>
        /// 章节名称
        /// </summary>
        [JsonProperty("chapter_name")]
        public string ChapterName { get; set; }

        /// <summary>
        /// 标题文字
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// 题目图片(可空)
        /// </summary>
        [JsonIgnore]
        public byte[] Image { get; set; }

        /// <summary>
        /// 选项A
        /// </summary>
        [JsonProperty("a")]
        public string A { get; set; }

        /// <summary>
        /// 选项B
        /// </summary>
        [JsonProperty("b")]
        public string B { get; set; }

        /// <summary>
        /// 选项C
        /// </summary>
        [JsonProperty("c")]
        public string C { get; set; }

        /// <summary>
        /// 选项D
        /// </summary>
        [JsonProperty("d")]
        public string D { get; set; }

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
        /// 试题难易度 (1-5：评级)
        /// </summary>
        [JsonProperty("difficulty")]
        public int Difficulty { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>
        [JsonProperty("add_person")]
        public string AddPerson { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [JsonProperty("add_time")]
        public DateTime AddTime { get; set; }

        public SingleItemDTO()
        {

        }

        public SingleItemDTO(SingleItem singleItem)
        {
            this.ID = singleItem.ID;
            this.ChapterID = singleItem.ChapterID;

            this.Title = singleItem.Title;
            this.Image = singleItem.Image;
            this.A = singleItem.A;
            this.B = singleItem.B;
            this.C = singleItem.C;
            this.D = singleItem.D;
            this.Answer = singleItem.Answer;
            this.Annotation = singleItem.Annotation;
            this.Difficulty = singleItem.Difficulty;
            this.AddPerson = singleItem.AddPerson;
            this.AddTime = singleItem.AddTime;
        }
    }
}
