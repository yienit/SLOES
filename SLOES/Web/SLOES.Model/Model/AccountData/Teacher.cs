using System;
using Newtonsoft.Json;

namespace SLOES.Model
{
    /// <summary>
    /// 老师信息实体类
    /// </summary>
    public class Teacher
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [JsonProperty("user_name")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [JsonProperty("chinese_name")]
        public string ChineseName { get; set; }

        /// <summary>
        /// 账号级别 (0: 题库管理员  1：系统管理员)
        /// </summary>
        [JsonProperty("level")]
        public TeacherLevel Level { get; set; }

        /// <summary>
        /// 是否拥有阅卷功能(0：否  1：是)
        /// </summary>
        [JsonProperty("is_can_mark_paper")]
        public int IsCanMarkPaper { get; set; }

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
