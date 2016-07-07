using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SLOES.Model;
using SLOES.DTO;
using System.Data.Common;
using Dapper;
using System.Data;

namespace SLOES.DAL
{
    /// <summary>
    /// 数字填空题题库DAL
    /// </summary>
    public class BlankItemDAL
    {
        /// <summary>
        /// 以分页的方式查询实体信息
        /// </summary>
        public QueryResultDTO<BlankItem> Query(QueryArgsDTO<BlankItem> queryDTO, int courseID)
        {
            QueryResultDTO<BlankItem> resultDTO = new QueryResultDTO<BlankItem>();
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                const string sqlBase = "SELECT * FROM BlankItem WHERE IsDeleted = 0 {0} ORDER BY AddTime DESC LIMIT @StartIndex,@PageSize;";

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
                resultDTO.List = connection.Query<BlankItem>(paginationSql, dynamicParameters).ToList();

                // Sets paginatiion
                resultDTO.PageSize = queryDTO.PageSize;
                resultDTO.PageIndex = queryDTO.PageIndex;

                // Sets total record with same where sql string.
                const string sqlCountBase = "SELECT COUNT(*) FROM BlankItem WHERE IsDeleted = 0 {0}";
                string sqlCount = string.Format(sqlCountBase, sqlWhereBuilder);
                int count = Convert.ToInt32(connection.ExecuteScalar(sqlCount, dynamicParameters, null, null, null));
                resultDTO.TotalRecordCount = count;
            }

            return resultDTO;
        }

        /// <summary>
        /// 根据主键ID获取实体信息
        /// </summary>
        public BlankItem GetByID(int id)
        {
            BlankItem numberBlank = null;

            const string sql = "SELECT * FROM BlankItem WHERE IsDeleted = 0 AND ID = @ID LIMIT 1";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                numberBlank = connection.Query<BlankItem>(sql, new { ID = id }).SingleOrDefault<BlankItem>();
            }
            return numberBlank;
        }

        /// <summary>
        /// 获取填空题的所有答案
        /// </summary>
        public List<BlankAnswer> GetAnswers(int blankID)
        {
            List<BlankAnswer> answers = null;

            const string sql = "SELECT * FROM BlankAnswer WHERE IsDeleted = 0 AND BlankItemID = @BlankItemID ORDER BY AnswerIndex ASC";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                answers = connection.Query<BlankAnswer>(sql, new { BlankItemID = blankID }).ToList<BlankAnswer>();
            }
            return answers;
        }

        /// <summary>
        /// 添加实体信息,返回添加成功后的主键ID
        /// </summary>
        public void Insert(BlankItem numberBlankItem, List<BlankAnswer> answers)
        {
            const string insert_item_sql = @"INSERT INTO BlankItem(ChapterID, Title, Image, Difficulty, AddPerson) 
                                             VALUES (@ChapterID, @Title, @Image, @Difficulty, @AddPerson);
                                             SELECT LAST_INSERT_ID();";

            const string insert_answer_sql = @"INSERT INTO BlankAnswer(BlankItemID, AnswerIndex, Answer, Annotation) 
                                               VALUES (@BlankItemID, @AnswerIndex, @Answer, @Annotation);";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    int numberBlankID = connection.Query<int>(insert_item_sql, numberBlankItem, transaction).SingleOrDefault<int>();

                    foreach (var answer in answers)
                    {
                        connection.Execute(insert_answer_sql,
                            new
                            {
                                NumberBlankItemID = numberBlankID,
                                AnswerName = answer.AnswerIndex,
                                Answer = answer.Answer,
                                Annotation = answer.Annotation
                            },
                            transaction
                        );
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

        /// <summary>
        /// 更新实体信息
        /// </summary>
        public void Update(BlankItem blankItem, List<BlankAnswer> answers)
        {
            const string update_item_sql = @"UPDATE BlankItem SET ChapterID = @ChapterID,Title= @Title, Image = @Image, 
                                             Difficulty = @Difficulty WHERE IsDeleted = 0 AND ID = @ID";

            const string delete_answer_sql = @"UPDATE BlankAnswer SET IsDeleted = 1 WHERE BlankItemID = @BlankItemID";

            const string insert_answer_sql = @"INSERT INTO BlankAnswer(BlankItemID, AnswerIndex, Answer, Annotation) 
                                               VALUES (@BlankItemID, @AnswerIndex, @Answer, @Annotation);";

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    connection.Execute(update_item_sql, blankItem, transaction);
                    connection.Execute(delete_answer_sql, new { BlankItemID = blankItem.ID }, transaction);

                    foreach (var answer in answers)
                    {
                        connection.Execute(insert_answer_sql,
                            new
                            {
                                BlankItemID = blankItem.ID,
                                AnswerName = answer.AnswerIndex,
                                Answer = answer.Answer,
                                Annotation = answer.Annotation
                            },
                            transaction
                        );
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

        /// <summary>
        /// 根据主键ID删除实体信息(逻辑删除)
        /// </summary>
        public void DeleteByID(int id)
        {
            const string delete_item_sql = @"UPDATE BlankItem SET IsDeleted = 1 WHERE ID = @ID";
            const string delete_answer_sql = @"UPDATE BlankAnswer SET IsDeleted = 1 WHERE BlankItemID = @BlankItemID";

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    connection.Execute(delete_item_sql, new { ID = id }, transaction);
                    connection.Execute(delete_answer_sql, new { BlankItemID = id }, transaction);

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
        /// 根据主键ID批量删除实体信息(逻辑删除)
        /// </summary>
        public void DeleteInBatch(List<int> ids)
        {
            const string delete_item_sql = @"UPDATE BlankItem SET IsDeleted = 1 WHERE ID = @ID";
            const string delete_answer_sql = @"UPDATE BlankAnswer SET IsDeleted = 1 WHERE BlankItemID = @BlankItemID";

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    // 避免因SQL拼接导致数据库注入漏洞
                    foreach (var id in ids)
                    {
                        connection.Execute(delete_item_sql, new { ID = id }, transaction);
                        connection.Execute(delete_answer_sql, new { BlankItemID = id }, transaction);
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
