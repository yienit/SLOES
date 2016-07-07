using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SLOES.Core;
using SLOES.DAL;
using SLOES.DTO;
using SLOES.Util;
using SLOES.Model;
using RestSharp;
using Newtonsoft.Json;

namespace SLOES.Service
{
    /// <summary>
    /// 安全服务,API接口参数签名校验服务
    /// </summary>
    public class SecurityService
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(SecurityService));

        /// <summary>
        /// 检测参数Sign是否合法
        /// </summary>
        /// <param name="args">参数按照首字母升序排序之后的KV字典</param>
        /// <param name="secrect">参数签名密钥</param>
        /// <param name="sign">待验证的签名</param>
        public bool CheckSign(Dictionary<string, string> args, string secrect, string sign)
        {
            bool isSuccess = false;
            if (args != null)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(secrect);
                foreach (var item in args)
                {
                    builder.Append(item.Key);
                    builder.Append(item.Value);
                }
                builder.Append(secrect);
                string resultSign = SLOES.Util.SecurityUtil.MD5(builder.ToString());
                if (resultSign.Equals(sign, StringComparison.OrdinalIgnoreCase))
                {
                    isSuccess = true;
                }
            }
            return isSuccess;
        }
    }
}
