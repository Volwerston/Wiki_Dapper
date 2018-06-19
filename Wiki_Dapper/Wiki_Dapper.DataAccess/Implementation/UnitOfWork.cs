using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiki_Dapper.DataAccess.Interfaces;
using Wiki_Dapper.Models.DTO;

namespace Wiki_Dapper.DataAccess.Implementation
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IDbConnection _connection;

        private IArticleRepository _articleRepository;
        private ICategoryRepository _categoryRepository;
        private IArticleContributorRepository _articleContributorRepository;
        private ICommentRepository _commentRepository;
        

        public UnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public IArticleRepository ArticleRepository
        {
            get
            {
                if(_articleRepository == null)
                {
                    _articleRepository = new ArticleRepository(_connection);
                }

                return _articleRepository;
            }
        }
        
        public ICategoryRepository CategoryRepository
        {
            get
            {
                if(_categoryRepository == null)
                {
                    _categoryRepository = new CategoryRepository(_connection);
                }

                return _categoryRepository;
            }
        }

        public IArticleContributorRepository ArticleContributorRepository
        {
            get
            {
                if(_articleContributorRepository == null)
                {
                    _articleContributorRepository = new ArticleContributorRepository(_connection);
                }

                return _articleContributorRepository;
            }
        }

        public ICommentRepository CommentRepository
        {
            get
            {
                if(_commentRepository == null)
                {
                    _commentRepository = new CommentRepository(_connection);
                }

                return _commentRepository;
            }
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        #region Views

        public IEnumerable<StatisticsDTO> GetArticleStatistics()
        {
            string sql = "SELECT * FROM VW_Article_Statistics";

            return _connection.Query<StatisticsDTO>(sql);
        }

        #endregion
    }
}
