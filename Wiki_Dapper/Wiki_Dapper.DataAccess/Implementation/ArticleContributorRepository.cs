using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiki_Dapper.DataAccess.Interfaces;
using Wiki_Dapper.Entities.Models;

namespace Wiki_Dapper.DataAccess.Implementation
{
    public class ArticleContributorRepository : IArticleContributorRepository
    {
        private IDbConnection _connection;

        public ArticleContributorRepository(IDbConnection _con)
        {
            _connection = _con;
        }

        public void Add(ArticleContributor entity)
        {
            string sql = "INSERT INTO [ArticleContributors] VALUES(@ArticleId, @ContributorId, @ContributionTime)";

            _connection.Execute(sql, new
            {
                ArticleId = entity.ArticleId,
                ContributorId = entity.ContributorId,
                ContributionTime = DateTime.Now.ToUniversalTime()
            });
        }

        public void Delete(ArticleContributor entity)
        {
            string sql = "DELETE FROM [ArticleContributors] WHERE [Id] = @eid";

            _connection.Execute(sql, new { eid = entity.Id });
        }

        public void DeleteArticleContributors(int articleId)
        {
            string sql = @"DELETE FROM [ArticleContributors] WHERE [ArticleId] = @ArticleId";

            _connection.Execute(sql, new {
                ArticleId = articleId
            });
        }

        public IEnumerable<ArticleContributor> GetAll()
        {
            string sql = "SELECT * FROM [ArticleContributors]";

            return _connection.Query<ArticleContributor>(sql);
        }

        public IEnumerable<ArticleContributor> GetArticleContributors(int articleId)
        {
            string sql = "SELECT * FROM [ArticleContributors] WHERE [ArticleId] = @ArticleId";

            return _connection.Query<ArticleContributor>(sql, new { ArticleId = articleId }).ToList();
        }

        public ArticleContributor GetByKey(object key)
        {
            string sql = "SELECT * FROM [ArticleContributors] WHERE [Id]=@id";

            return _connection.QuerySingleOrDefault<ArticleContributor>(sql, new { id = key });
        }

        public void Update(ArticleContributor entity)
        {
            string sql = @"UPDATE [ArticleContributors]
                           SET [ArticleId] = @ArticleId,
                               [ContributorId] = @ContributorId,
                               [ContributionTime] = @ContributionTime
                               WHERE [Id] = @Id";

            _connection.Execute(sql, entity);
        }
    }
}
