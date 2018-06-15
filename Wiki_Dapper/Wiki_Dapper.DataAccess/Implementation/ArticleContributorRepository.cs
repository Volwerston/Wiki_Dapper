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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public IEnumerable<ArticleContributor> GetArticleContributors(int articleId)
        {
            string sql = "SELECT * FROM [ArticleContributors] WHERE [ArticleId] = @ArticleId";

            return _connection.Query<ArticleContributor>(sql, new { ArticleId = articleId }).ToList();
        }

        public ArticleContributor GetByKey(object key)
        {
            throw new NotImplementedException();
        }

        public void Update(ArticleContributor entity)
        {
            throw new NotImplementedException();
        }
    }
}
