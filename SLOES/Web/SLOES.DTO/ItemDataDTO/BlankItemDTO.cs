using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using SLOES.Model;

namespace SLOES.DTO
{
    /// <summary>
    /// 填空题DTO(包含所属章节名称)
    /// </summary>
    public class BlankItemDTO
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
        /// 图片(可空)
        /// </summary>
        [JsonProperty("image")]
        public byte[] Image { get; set; }

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

        /// <summary>
        /// 是否已删除 (0：未删除  1：已删除)
        /// </summary>
        [JsonIgnore]
        public int IsDeleted { get; set; }

        /// <summary>
        /// 答案列表
        /// </summary>
        public List<BlankAnswer> Answers { get; set; }

        public BlankItemDTO()
        {

        }

        public BlankItemDTO(BlankItem blankItem)
        {
            this.ID = blankItem.ID;
            this.ChapterID = blankItem.ChapterID;

            this.Title = blankItem.Title;
            this.Image = blankItem.Image;
            this.Difficulty = blankItem.Difficulty;
            this.AddPerson = blankItem.AddPerson;
            this.AddTime = blankItem.AddTime;
        }

        public BlankItemDTO(BlankItem blankItem, List<BlankAnswer> answers)
            : this(blankItem)
        {
            this.Answers = answers;
        }
    }
}
