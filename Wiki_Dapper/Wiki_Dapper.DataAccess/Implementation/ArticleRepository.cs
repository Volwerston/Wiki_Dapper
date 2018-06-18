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

        public void Add(Article entity)
        {
            string sql = "INSERT INTO [Articles] VALUES(@CreationTime, @Title, @Text, @CreatorId, @CategoryId)";

            _connection.Execute(sql, entity);
        }

        public void Delete(Article entity)
        {
            string sql = "DELETE FROM [Articles] WHERE Id=@id";

            _connection.Execute(sql, new { id = entity.Id });
        }

        public IEnumerable<Article> GetAll()
        {
            string sql = "sp_GetArticle";

            var articleDictionary = new Dictionary<int, Article>();

            var toReturn = _connection.Query<Article, ApplicationUser, Category, ArticleContributor, Article>(sql,
                (article, appUser, category, articleContributor) => {

                    Article articleEntry;

                    if(!articleDictionary.TryGetValue(article.Id, out articleEntry))
                    {
                        articleEntry = article;
                        articleEntry.Category = category;
                        articleEntry.Creator = appUser;

                        articleDictionary.Add(articleEntry.Id, articleEntry);
                    }

                    if (articleContributor != null && !articleEntry.ArticleContributors.Contains(articleContributor))
                    {
                        articleEntry.ArticleContributors.Add(articleContributor);
                    }

                    return articleEntry;
                },
                param: new { id = (null as string) },
                commandType: CommandType.StoredProcedure)
                .Distinct()
                .ToList();

            return toReturn;
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
            string sql = "sp_GetArticle";

            Article toReturn = null;

            var query = _connection.Query<Article, ApplicationUser, Category, ArticleContributor, Article>(sql,
                (article, appUser, category, articleContributor) =>
                {
                    if(toReturn == null)
                    {
                        toReturn = article;
                        toReturn.Category = category;
                        toReturn.Creator = appUser;
                    }

                    if (articleContributor != null )
                    {
                        toReturn.ArticleContributors.Add(articleContributor);
                    }

                    return article;
                },
                new { id = key }, 
                commandType: CommandType.StoredProcedure)
                .FirstOrDefault();

            return toReturn;
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
