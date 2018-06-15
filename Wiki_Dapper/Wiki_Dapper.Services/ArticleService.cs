using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using Wiki_Dapper.DataAccess.Interfaces;
using Wiki_Dapper.Entities.Models;

namespace Wiki_Dapper.Services
{
    public class ArticleService
    {
        private IUnitOfWork _unitOfWork;
        private UserManager<ApplicationUser> _userManager;

        public ArticleService(IUnitOfWork uow, UserManager<ApplicationUser> uMgr)
        {
            _unitOfWork = uow;
            _userManager = uMgr;
        }

        public void UpdateArticle(Article article, string userId)
        {
            _unitOfWork.ArticleRepository.Update(article);

            IEnumerable<ArticleContributor> contributors = _unitOfWork.ArticleContributorRepository.GetArticleContributors(article.Id);

            if(!contributors.Any(x => x.ContributorId == userId))
            {
                _unitOfWork.ArticleContributorRepository.Add(new ArticleContributor()
                {
                    ArticleId = article.Id,
                    ContributorId = userId
                });
            }
        }

        public void Delete(int id, string userId)
        {
            Article toDelete = _unitOfWork.ArticleRepository.GetByKey(id);

            if (toDelete == null || toDelete.CreatorId != userId) return;

            _unitOfWork.ArticleContributorRepository.DeleteArticleContributors(toDelete.Id);

            _unitOfWork.ArticleRepository.Delete(toDelete);
        }

        public IEnumerable<ApplicationUser> GetContributors(IEnumerable<ArticleContributor> contributors)
        {
            IEnumerable<ApplicationUser> applicationUsers = _userManager.Users;

            var toReturn = contributors.Join(applicationUsers, ac => ac.ContributorId, au => au.Id, (contributor, user) => user).ToList();

            return toReturn;
        }
    }
}
