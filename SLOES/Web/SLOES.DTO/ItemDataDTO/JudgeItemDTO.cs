using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using SLOES.Model;

namespace SLOES.DTO
{
    /// <summary>
    /// 判断题DTO(包含所属章节名称)
    /// </summary>
    public class JudgeItemDTO
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
        /// 答案 (0: 错误  1: 正确)
        /// </summary>
        [JsonProperty("answer")]
        public int Answer { get; set; }

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

        public JudgeItemDTO()
        {

        }

        public JudgeItemDTO(JudgeItem judgeItem)
        {
            this.ID = judgeItem.ID;
            this.ChapterID = judgeItem.ChapterID;

            this.Title = judgeItem.Title;
            this.Image = judgeItem.Image;
            this.Answer = judgeItem.Answer;
            this.Annotation = judgeItem.Annotation;
            this.Difficulty = judgeItem.Difficulty;
            this.AddPerson = judgeItem.AddPerson;
            this.AddTime = judgeItem.AddTime;
        }
    }
}
