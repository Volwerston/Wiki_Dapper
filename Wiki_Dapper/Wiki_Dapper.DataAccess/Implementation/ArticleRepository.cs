using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiki_Dapper.DataAccess.Interfaces;
using Wiki_Dapper.Entities.Models;
using System.Configuration;
using Dapper;
using System.Data;

namespace Wiki_Dapper.DataAccess.Implementation
{
    public class ArticleRepository : IArticleRepository
    {
        private IDbConnection _connection;

        public ArticleRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Delete(Article entity)
        {
            string sql = "DELETE FROM [Articles] WHERE Id=@id";

            _connection.Execute(sql, new { id = entity.Id });
        }

        public IEnumerable<Article> GetAll()
        {
            string sql = "SELECT * FROM [Articles]";

            return _connection.Query<Article>(sql).ToList();
        }

        public IEnumerable<Article> GetByCategory(string category)
        {
            string sql = @"DECLARE @categoryId int;
                           SELECT @categoryId=Id FROM Categories WHERE Title=@CategoryTitle;
                           SELECT * FROM [Articles] WHERE CategoryId=@categoryId";


            return _connection.Query<Article>(sql, new { CategoryTitle = category }).ToList();
        }

        public Article GetByKey(object key)
        {
            string sql = "SELECT * FROM [Articles] WHERE Id=@id";

            return _connection.QuerySingleOrDefault<Article>(sql, new { id = key });
        }

        public IEnumerable<Article> GetByTitle(string title)
        {
            string sql = "SELECT * FROM [Articles] WHERE TITLE LIKE '%@Title%'";

            return _connection.Query<Article>(sql, new { Title = title }).ToList();
        }

        public void Update(Article entity)
        {
            string sql = @"UPDATE [Articles]
                           SET Title=@Title, Text=@Text
                           WHERE Id=@Id";

            _connection.Execute(sql, entity);
        }
    }
}
