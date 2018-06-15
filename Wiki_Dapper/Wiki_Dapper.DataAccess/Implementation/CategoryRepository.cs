using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiki_Dapper.DataAccess.Interfaces;
using Wiki_Dapper.Entities.Models;
using Dapper;

namespace Wiki_Dapper.DataAccess.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private IDbConnection _connection;

        public CategoryRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Add(Category entity)
        {
            string sql = "INSERT INTO [Categories] VALUES (@Title, @CreatorId)";

            _connection.Execute(sql, entity);
        }

        public void Delete(Category entity)
        {
            string sql = "DELETE FROM [Categories] WHERE Id=@id";

            _connection.Execute(sql, new { id = entity.Id });
        }

        public IEnumerable<Category> GetAll()
        {
            string sql = "SELECT * FROM [Categories]";

            return _connection.Query<Category>(sql).ToList();
        }

        public Category GetByKey(object key)
        {
            string sql = "SELECT * FROM [Categories] WHERE Id=@id";

            return _connection.QuerySingleOrDefault<Category>(sql);
        }

        public void Update(Category entity)
        {
            string sql = @"UPDATE [Categories]
                          SET Title=@Title
                          WHERE Id=@Id";

            _connection.Execute(sql, entity);
        }
    }
}
