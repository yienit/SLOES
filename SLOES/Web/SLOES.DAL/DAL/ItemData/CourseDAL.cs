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
    /// 课程DAL
    /// </summary>
    public class CourseDAL
    {
        /// <summary>
        /// 以分页的方式查询实体信息
        /// </summary>
        public QueryResultDTO<Course> Query(QueryArgsDTO<Course> queryDTO)
        {
            QueryResultDTO<Course> resultDTO = new QueryResultDTO<Course>();
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                const string sqlBase = "SELECT * FROM Course WHERE IsDeleted = 0 {0} ORDER BY AddTime DESC LIMIT @StartIndex,@PageSize;";

                StringBuilder sqlWhereBuilder = new StringBuilder();
                Dictionary<string, object> parameterDictionary = new Dictionary<string, object>();

                if (!string.IsNullOrEmpty(queryDTO.Model.Name))
                {
                    sqlWhereBuilder.Append("AND Name LIKE CONCAT('%',@Name,'%') ");
                    parameterDictionary.Add("Name", queryDTO.Model.Name);
                }

                // Pagination (start with 0 in mysql)
                int pageSize = queryDTO.PageSize;
                int startIndex = pageSize * (queryDTO.PageIndex - 1);
                parameterDictionary.Add("StartIndex", startIndex);
                parameterDictionary.Add("PageSize", pageSize);

                // Execute pagination sql.
                string paginationSql = string.Format(sqlBase, sqlWhereBuilder);
                var dynamicParameters = new DynamicParameters(parameterDictionary);
                resultDTO.List = connection.Query<Course>(paginationSql, dynamicParameters).ToList();

                // Sets paginatiion
                resultDTO.PageSize = queryDTO.PageSize;
                resultDTO.PageIndex = queryDTO.PageIndex;

                // Sets total record with same where sql string.
                const string sqlCountBase = "SELECT COUNT(*) FROM Course WHERE IsDeleted = 0 {0}";
                string sqlCount = string.Format(sqlCountBase, sqlWhereBuilder);
                int count = Convert.ToInt32(connection.ExecuteScalar(sqlCount, dynamicParameters, null, null, null));
                resultDTO.TotalRecordCount = count;
            }

            return resultDTO;
        }

        /// <summary>
        /// 获取所有实体信息
        /// </summary>
        public List<Course> GetAll()
        {
            List<Course> courses = null;

            const string sql = "SELECT * FROM Course WHERE IsDeleted = 0 ";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                courses = connection.Query<Course>(sql, null).ToList<Course>();
            }
            return courses;
        }

        /// <summary>
        /// 根据主键ID获取实体信息
        /// </summary>
        public Course GetByID(int id)
        {
            Course course = null;

            const string sql = "SELECT * FROM Course WHERE IsDeleted = 0 AND ID = @ID LIMIT 1";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                course = connection.Query<Course>(sql, new { ID = id }).SingleOrDefault<Course>();
            }
            return course;
        }

        /// <summary>
        /// 根据科目名称获取实体信息
        /// </summary>
        public Course GetByName(string name)
        {
            Course course = null;

            const string sql = "SELECT * FROM Course WHERE IsDeleted = 0 AND Name = @Name LIMIT 1";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                course = connection.Query<Course>(sql, new { Name = name }).SingleOrDefault<Course>();
            }
            return course;
        }

        /// <summary>
        /// 添加实体信息,返回添加成功后的主键ID
        /// </summary>
        public int Insert(Course course)
        {
            int id = 0;

            const string sql = @"INSERT INTO Course(Name, Description) VALUES (@Name, @Description);
                               SELECT LAST_INSERT_ID();";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                id = connection.Query<int>(sql, course).SingleOrDefault<int>();
            }
            return id;
        }

        /// <summary>
        /// 更新实体信息
        /// </summary>
        public void Update(Course course)
        {
            const string sql = @"UPDATE Course SET Name = @Name, Description= @Description WHERE IsDeleted = 0 AND ID = @ID";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                connection.Execute(sql, course);
            }
        }

        /// <summary>
        /// 根据主键ID删除实体信息(逻辑删除)
        /// </summary>
        public void DeleteByID(int id)
        {
            const string sql = @"UPDATE Course SET IsDeleted = 1 WHERE ID = @ID";
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
            const string delete_course_sql = @"UPDATE Course SET IsDeleted = 1 WHERE ID = @ID";

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    // 避免因SQL拼接导致数据库注入漏洞
                    foreach (var id in ids)
                    {
                        connection.Execute(delete_course_sql, new { ID = id }, transaction);
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
