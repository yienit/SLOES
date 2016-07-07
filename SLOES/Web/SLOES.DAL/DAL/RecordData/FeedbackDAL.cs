﻿using System.Data.Common;
using System.Linq;
using Dapper;
using SLOES.Model;

namespace SLOES.DAL
{
    /// <summary>
    /// 意见反馈DAL
    /// </summary>
    public class FeedbackDAL
    {
        /// <summary>
        /// 添加实体对象,返回插入成功后的实体对象主键ID
        /// </summary>
        public int Insert(Feedback feedback)
        {
            const string sql = @"INSERT INTO Feedback(Content, Contact, TerminalType) VALUES (@Content, @Contact, @TerminalType);
                               SELECT LAST_INSERT_ID();";

            int id = 0;
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                id = connection.Query<int>(sql, feedback).SingleOrDefault<int>();
            }
            return id;
        }
    }
}
