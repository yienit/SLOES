using System;
using System.Drawing;
using Newtonsoft.Json;

namespace SLOES.Model
{
    /// <summary>
    /// 学生信息实体类
    /// </summary>
    public class Student
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [JsonProperty("chinese_name")]
        public string ChineseName { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// 用户名(默认为电话)
        /// </summary>
        [JsonProperty("user_name")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码(默认为电话后六位)
        /// </summary>
        [JsonIgnore]
        public string Password { get; set; }

        /// </summary>
        [JsonProperty("state")]
        public StudentState State { get; set; }

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
    }
}
