﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using Dapper;
using SLOES.DTO;
using SLOES.Model;
using System.Data;

namespace SLOES.DAL
{
    /// <summary>
    /// 单选题题库DAL
    /// </summary>
    public class SingleItemDAL
    {
        /// <summary>
        /// 以分页的方式查询实体信息
        /// </summary>
        public QueryResultDTO<SingleItem> Query(QueryArgsDTO<SingleItem> queryDTO, int courseID)
        {
            QueryResultDTO<SingleItem> resultDTO = new QueryResultDTO<SingleItem>();
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                const string sqlBase = "SELECT * FROM SingleItem WHERE IsDeleted = 0 {0} ORDER BY AddTime DESC LIMIT @StartIndex,@PageSize;";

                StringBuilder sqlWhereBuilder = new StringBuilder();
                Dictionary<string, object> parameterDictionary = new Dictionary<string, object>();

                if (queryDTO.Model.ChapterID != -1)
                {
                    sqlWhereBuilder.Append("AND ChapterID = @ChapterID ");
                    parameterDictionary.Add("ChapterID", queryDTO.Model.ChapterID);
                }
                else
                {
                    // 查询本课程的所有章节
                    sqlWhereBuilder.Append("AND ChapterID IN (SELECT ID FROM Chapter WHERE CourseID = @CourseID) ");
                    parameterDictionary.Add("CourseID", courseID);
                }
                if (!string.IsNullOrEmpty(queryDTO.Model.Title))
                {
                    sqlWhereBuilder.Append("AND Title LIKE CONCAT('%',@Title,'%') ");
                    parameterDictionary.Add("Title", queryDTO.Model.Title);
                }
                if (queryDTO.Model.Difficulty != -1)
                {
                    sqlWhereBuilder.Append("AND Difficulty = @Difficulty ");
                    parameterDictionary.Add("Difficulty", queryDTO.Model.Difficulty);
                }
                if (!string.IsNullOrEmpty(queryDTO.Model.AddPerson))
                {
                    sqlWhereBuilder.Append("AND AddPerson LIKE CONCAT('%',@AddPerson,'%') ");
                    parameterDictionary.Add("AddPerson", queryDTO.Model.AddPerson);
                }

                // Pagination (start with 0 in mysql)
                int pageSize = queryDTO.PageSize;
                int startIndex = pageSize * (queryDTO.PageIndex - 1);
                parameterDictionary.Add("StartIndex", startIndex);
                parameterDictionary.Add("PageSize", pageSize);

                // Execute pagination sql.
                string paginationSql = string.Format(sqlBase, sqlWhereBuilder);
                var dynamicParameters = new DynamicParameters(parameterDictionary);
                resultDTO.List = connection.Query<SingleItem>(paginationSql, dynamicParameters).ToList();

                // Sets paginatiion
                resultDTO.PageSize = queryDTO.PageSize;
                resultDTO.PageIndex = queryDTO.PageIndex;

                // Sets total record with same where sql string.
                const string sqlCountBase = "SELECT COUNT(*) FROM SingleItem WHERE IsDeleted = 0 {0}";
                string sqlCount = string.Format(sqlCountBase, sqlWhereBuilder);
                int count = Convert.ToInt32(connection.ExecuteScalar(sqlCount, dynamicParameters, null, null, null));
                resultDTO.TotalRecordCount = count;
            }

            return resultDTO;
        }

        /// <summary>
        /// 根据主键ID获取实体信息
        /// </summary>
        public SingleItem GetByID(int id)
        {
            SingleItem single = null;

            const string sql = "SELECT * FROM SingleItem WHERE IsDeleted = 0 AND ID = @ID LIMIT 1";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                single = connection.Query<SingleItem>(sql, new { ID = id }).SingleOrDefault<SingleItem>();
            }
            return single;
        }

        /// <summary>
        /// 添加实体信息,返回添加成功后的主键ID
        /// </summary>
        public int Insert(SingleItem single)
        {
            int id = 0;

            const string sql = @"INSERT INTO SingleItem(ChapterID, Title, Image, A, B, C, D, Answer, Annotation, Difficulty, AddPerson) 
                               VALUES (@ChapterID, @Title, @Image, @A, @B, @C, @D, @Answer, @Annotation, @Difficulty, @AddPerson);
                               SELECT LAST_INSERT_ID();";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                id = connection.Query<int>(sql, single).SingleOrDefault<int>();
            }
            return id;
        }

        /// <summary>
        /// 添加实体信息(批量添加);
        /// </summary>
        public void Insert(List<SingleItem> singles)
        {
            string sql = @"INSERT INTO SingleItem(ChapterID, Title, A, B, C, D, Answer, Annotation, Difficulty, AddPerson) 
                           VALUES (@ChapterID, @Title, @A, @B, @C, @D, @Answer, @Annotation, @Difficulty, @AddPerson);";

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                //开始事务
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (var single in singles)
                    {
                        connection.Execute(sql, single, transaction, null, null);
                    }

                    //提交事务
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 更新实体信息
        /// </summary>
        public void Update(SingleItem single)
        {
            const string sql = @"UPDATE SingleItem SET ChapterID = @ChapterID, Title= @Title, 
                               Image= @Image, A = @A, B= @B, C = @C, D = @D, 
                               Answer = @Answer, Annotation = @Annotation, Difficulty = @Difficulty
                               WHERE IsDeleted = 0 AND ID = @ID";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                connection.Execute(sql, single);
            }
        }

        /// <summary>
        /// 根据主键ID删除实体信息(逻辑删除)
        /// </summary>
        public void DeleteByID(int id)
        {
            const string sql = @"UPDATE SingleItem SET IsDeleted = 1 WHERE ID = @ID";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                connection.Execute(sql, new { ID = id });
            }
        }

        /// <summary>
        /// 根据主键ID批量删除实体信息(逻辑删除)
        /// </summary>
        public void DeleteInBatch(List<int> ids)
        {
            const string sql = @"UPDATE SingleItem SET IsDeleted = 1 WHERE ID = @ID;";

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    // 避免因SQL拼接导致数据库注入漏洞
                    foreach (var id in ids)
                    {
                        connection.Execute(sql, new { ID = id }, transaction);
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
