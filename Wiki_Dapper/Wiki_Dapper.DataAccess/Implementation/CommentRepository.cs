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
    public class CommentRepository : ICommentRepository
    {
        private IDbConnection _connection;

        public CommentRepository(IDbConnection con)
        {
            _connection = con;
        }

        public void Add(Comment entity)
        {
            string sql = "INSERT INTO [Comments] VALUES(@AddingTime, @Text, @AuthorId, @ArticleId)";

            _connection.Execute(sql, entity);
        }

        public void Delete(Comment entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteArticleComments(int articleId)
        {
            string sql = "DELETE FROM [Comments] WHERE [ArticleId] = @aid";

            _connection.Execute(sql, new { aid = articleId });
        }

        public IEnumerable<Comment> GetAll()
        {
            throw new NotImplementedException();
        }

        public Comment GetByKey(object key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comment> GetCommentsByArticle(int articleId)
        {
            string sql = @"SELECT * FROM [Comments] C
                           INNER JOIN [AspNetUsers] U
                           ON C.AuthorId=U.Id
                           WHERE [ArticleId]=@aid";


            Dictionary<int, Comment> commentCache = new Dictionary<int, Comment>();

            var list = _connection.Query<Comment, ApplicationUser, Comment>(sql,
                (comment, appUser) => {

                    Comment toReturn;

                    if(!commentCache.TryGetValue(comment.Id, out toReturn))
                    {
                        toReturn = comment;
                        toReturn.Author = appUser;

                        commentCache.Add(comment.Id, toReturn);
                    }

                    return toReturn;
                }, new { aid = articleId})
                .Distinct()
                .ToList();

            return list;
        }

        public void Update(Comment entity)
        {
            throw new NotImplementedException();
        }
    }
}
