using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiki_Dapper.Models.DTO;

namespace Wiki_Dapper.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        IArticleRepository ArticleRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IArticleContributorRepository ArticleContributorRepository { get; }
        ICommentRepository CommentRepository { get; }

        IEnumerable<StatisticsDTO> GetArticleStatistics();
    }
}
