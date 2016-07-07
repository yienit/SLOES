using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using SLOES.Model;

namespace SLOES.DTO
{
    /// <summary>
    /// 简答题DTO
    /// </summary>
    public class WordItemDTO
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
        [JsonProperty("image")]
        public byte[] Image { get; set; }

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

        public WordItemDTO()
        {

        }

        public WordItemDTO(WordItem wordItem)
        {
            this.ID = wordItem.ID;
            this.ChapterID = wordItem.ChapterID;

            this.Title = wordItem.Title;
            this.Image = wordItem.Image;
            this.Answer = wordItem.Answer;
            this.Annotation = wordItem.Annotation;
            this.Difficulty = wordItem.Difficulty;
            this.AddPerson = wordItem.AddPerson;
            this.AddTime = wordItem.AddTime;
        }
    }
}
