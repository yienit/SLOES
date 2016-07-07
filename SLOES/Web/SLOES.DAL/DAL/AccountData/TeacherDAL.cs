using System;
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
    /// 老师DAL
    /// </summary>
    public class TeacherDAL
    {
        /// <summary>
        /// 以分页的方式查询实体信息
        /// </summary>
        public QueryResultDTO<Teacher> Query(QueryArgsDTO<Teacher> queryDTO)
        {
            QueryResultDTO<Teacher> resultDTO = new QueryResultDTO<Teacher>();
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                const string sqlBase = "SELECT * FROM Teacher WHERE IsDeleted = 0 {0} ORDER BY AddTime DESC LIMIT @StartIndex,@PageSize;";

                StringBuilder sqlWhereBuilder = new StringBuilder();
                Dictionary<string, object> parameterDictionary = new Dictionary<string, object>();

                if (!string.IsNullOrEmpty(queryDTO.Model.UserName))
                {
                    sqlWhereBuilder.Append("AND UserName LIKE CONCAT('%',@UserName,'%') ");
                    parameterDictionary.Add("UserName", queryDTO.Model.UserName);
                }
                if (!string.IsNullOrEmpty(queryDTO.Model.ChineseName))
                {
                    sqlWhereBuilder.Append("AND ChineseName LIKE CONCAT('%',@ChineseName,'%') ");
                    parameterDictionary.Add("ChineseName", queryDTO.Model.ChineseName);
                }

                // Pagination (start with 0 in mysql)
                int pageSize = queryDTO.PageSize;
                int startIndex = pageSize * (queryDTO.PageIndex - 1);
                parameterDictionary.Add("StartIndex", startIndex);
                parameterDictionary.Add("PageSize", pageSize);

                // Execute pagination sql.
                string paginationSql = string.Format(sqlBase, sqlWhereBuilder);
                var dynamicParameters = new DynamicParameters(parameterDictionary);
                resultDTO.List = connection.Query<Teacher>(paginationSql, dynamicParameters).ToList();

                // Sets paginatiion
                resultDTO.PageSize = queryDTO.PageSize;
                resultDTO.PageIndex = queryDTO.PageIndex;

                // Sets total record with same where sql string.
                const string sqlCountBase = "SELECT COUNT(*) FROM Teacher WHERE IsDeleted = 0 {0}";
                string sqlCount = string.Format(sqlCountBase, sqlWhereBuilder);
                int count = Convert.ToInt32(connection.ExecuteScalar(sqlCount, dynamicParameters, null, null, null));
                resultDTO.TotalRecordCount = count;
            }

            return resultDTO;
        }

        /// <summary>
        /// 根据主键ID获取实体信息
        /// </summary>
        public Teacher GetByID(int id)
        {
            Teacher teacher = null;

            const string sql = "SELECT * FROM Teacher WHERE IsDeleted = 0 AND ID = @ID LIMIT 1";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                teacher = connection.Query<Teacher>(sql, new { ID = id }).SingleOrDefault<Teacher>();
            }
            return teacher;
        }

        /// <summary>
        /// 根据用户名获取实体信息
        /// </summary>
        public Teacher GetByUserName(string userName)
        {
            Teacher teacher = null;

            const string sql = @"SELECT * FROM Teacher WHERE IsDeleted = 0 AND UserName = @UserName LIMIT 1";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                teacher = connection.Query<Teacher>(sql, new { UserName = userName }).SingleOrDefault<Teacher>();
            }
            return teacher;
        }

        /// <summary>
        /// 添加实体信息,返回添加成功后的主键ID
        /// </summary>
        public int Insert(Teacher teacher)
        {
            int id = 0;

            const string sql = @"INSERT INTO Teacher(UserName, Password, ChineseName, Level, IsCanMarkPaper) 
                               VALUES (@UserName, @Password, @ChineseName, @Level, @IsCanMarkPaper);
                               SELECT LAST_INSERT_ID();";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                id = connection.Query<int>(sql, teacher).SingleOrDefault<int>();
            }
            return id;
        }

        /// <summary>
        /// 更新实体信息
        /// </summary>
        public void Update(Teacher teacher)
        {
            const string sql = @"UPDATE Teacher SET UserName= @UserName, ChineseName = @ChineseName, IsCanMarkPaper= @IsCanMarkPaper WHERE IsDeleted = 0 AND ID = @ID";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                connection.Execute(sql, teacher);
            }
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        public void UpdatePassword(int id, string password)
        {
            const string sql = @"UPDATE Teacher SET Password = @Password WHERE IsDeleted = 0 AND ID = @ID";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                connection.Execute(sql, new { ID = id, Password = password });
            }
        }

        /// <summary>
        /// 根据主键ID删除实体信息(逻辑删除)
        /// </summary>
        public void DeleteByID(int id)
        {
            const string sql = @"UPDATE Teacher SET IsDeleted = 1 WHERE ID = @ID";
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
            const string sql = @"UPDATE Teacher SET IsDeleted = 1 WHERE ID = @ID;";

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
