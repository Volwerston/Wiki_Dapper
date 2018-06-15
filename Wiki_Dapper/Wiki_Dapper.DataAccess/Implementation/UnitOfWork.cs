﻿using System;
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
        private IDbConnection _connection;

        private IArticleRepository _articleRepository;
        private ICategoryRepository _categoryRepository;
        private IArticleContributorRepository _articleContributorRepository;
        

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

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
