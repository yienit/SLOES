using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SLOES.DTO;
using SLOES.Model;
using System.Data.Common;
using Dapper;

namespace SLOES.DAL
{
    /// <summary>
    /// 试卷DAL
    /// </summary>
    public class PaperDAL
    {
        /// <summary>
        /// 以分页的方式查询实体信息
        /// </summary>
        public QueryResultDTO<Paper> Query(QueryArgsDTO<Paper> queryDTO)
        {
            QueryResultDTO<Paper> resultDTO = new QueryResultDTO<Paper>();
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                const string sqlBase = "SELECT * FROM Paper WHERE IsDeleted = 0 {0} ORDER BY AddTime DESC LIMIT @StartIndex,@PageSize;";

                StringBuilder sqlWhereBuilder = new StringBuilder();
                Dictionary<string, object> parameterDictionary = new Dictionary<string, object>();

                if (queryDTO.Model.CourseID != -1)
                {
                    sqlWhereBuilder.Append("AND CourseID = @CourseID ");
                    parameterDictionary.Add("CourseID", queryDTO.Model.CourseID);
                }
                if (Enum.IsDefined(typeof(PaperType), queryDTO.Model.PaperType))
                {
                    sqlWhereBuilder.Append("AND PaperType = @PaperType ");
                    parameterDictionary.Add("PaperType", queryDTO.Model.PaperType);
                }
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
                resultDTO.List = connection.Query<Paper>(paginationSql, dynamicParameters).ToList();

                // Sets paginatiion
                resultDTO.PageSize = queryDTO.PageSize;
                resultDTO.PageIndex = queryDTO.PageIndex;

                // Sets total record with same where sql string.
                const string sqlCountBase = "SELECT COUNT(*) FROM Paper WHERE IsDeleted = 0 {0}";
                string sqlCount = string.Format(sqlCountBase, sqlWhereBuilder);
                int count = Convert.ToInt32(connection.ExecuteScalar(sqlCount, dynamicParameters, null, null, null));
                resultDTO.TotalRecordCount = count;
            }

            return resultDTO;
        }

        /// <summary>
        /// 根据主键ID获取实体信息
        /// </summary>
        public Paper GetByID(int id)
        {
            Paper paper = null;

            const string sql = "SELECT * FROM Paper WHERE IsDeleted = 0 AND ID = @ID LIMIT 1";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                paper = connection.Query<Paper>(sql, new { ID = id }).SingleOrDefault<Paper>();
            }
            return paper;
        }

        /// <summary>
        /// 添加实体信息,返回添加成功后的主键ID
        /// </summary>
        public int Insert(Paper paper)
        {
            int id = 0;

            const string sql = @"INSERT INTO Paper(CourseID, PaperType, Name, Duration, AddPerson) VALUES (@CourseID, @PaperType, @Name, @Duration, @AddPerson);
                               SELECT LAST_INSERT_ID();";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                id = connection.Query<int>(sql, paper).SingleOrDefault<int>();
            }
            return id;
        }

        /// <summary>
        /// 更新实体信息
        /// </summary>
        public void Update(Paper paper)
        {
            const string sql = @"UPDATE Paper SET PaperType = @PaperType, Name= @Name, Duration= @Duration WHERE IsDeleted = 0 AND ID = @ID";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                connection.Execute(sql, paper);
            }
        }

        /// <summary>
        /// 根据主键ID删除实体信息(逻辑删除)
        /// </summary>
        public void DeleteByID(int id)
        {
            const string sql = @"UPDATE Paper SET IsDeleted = 1 WHERE ID = @ID";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                connection.Execute(sql, new { ID = id });
            }
        }
    }
}
