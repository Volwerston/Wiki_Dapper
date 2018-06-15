using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiki_Dapper.Entities.Models;

namespace Wiki_Dapper.DataAccess.Interfaces
{
    public interface IArticleContributorRepository : IRepository<ArticleContributor>
    {
        IEnumerable<ArticleContributor> GetArticleContributors(int articleId);
        void DeleteArticleContributors(int articleId);
    }
}
