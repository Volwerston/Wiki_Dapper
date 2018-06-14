using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiki_Dapper.DataAccess.Interfaces;

namespace Wiki_Dapper.DataAccess.Implementation
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IArticleRepository _articleRepository;
        private IDbConnection _connection;

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

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
