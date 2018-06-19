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
            string sql = "DELETE FROM [Comments] WHERE [id] = @eid";

            _connection.Execute(sql, new { eid = entity.Id });
        }

        public void DeleteArticleComments(int articleId)
        {
            string sql = "DELETE FROM [Comments] WHERE [ArticleId] = @aid";

            _connection.Execute(sql, new { aid = articleId });
        }

        public IEnumerable<Comment> GetAll()
        {
            string sql = "SELECT * FROM [Comments]";

            return _connection.Query<Comment>(sql);
        }

        public Comment GetByKey(object key)
        {
            string sql = @"SELECT * FROM [Comments]
                           WHERE [Id] = @id";

            return _connection.QueryFirstOrDefault<Comment>(sql, new { id = key });
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
            string sql = @"UPDATE [Comments]
                           SET [Text] = @Text,
                               [AddingTime] = @AddingTime,
                               [ArticleId] = @ArticleId,
                               [AuthorId] = @AuthorId";

            _connection.Execute(sql, entity);
        }
    }
}
