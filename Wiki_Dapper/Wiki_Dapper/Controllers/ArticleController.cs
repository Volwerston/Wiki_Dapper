using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Wiki_Dapper.DataAccess.Interfaces;
using Wiki_Dapper.Entities.Models;
using Wiki_Dapper.Services;

namespace Wiki_Dapper.Controllers
{
    public class ArticleController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private IUnitOfWork _unitOfWork;
        private ArticleService _articleService;

        public ArticleController(UserManager<ApplicationUser> userMgr, IUnitOfWork uow, ArticleService articleService)
        {
            _userManager = userMgr;
            _unitOfWork = uow;
            _articleService = articleService;
        }

        [Authorize]
        public IActionResult Create()
        {
            Article toAdd = new Article();

            ViewBag.Categories = _unitOfWork.CategoryRepository.GetAll();

            return View(toAdd);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(Article article)
        {
            article.CreatorId = _userManager.GetUserId(User);
            article.CreationTime = DateTime.Now;

            _unitOfWork.ArticleRepository.Add(article);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            Article toEdit = _unitOfWork.ArticleRepository.GetByKey(id);

            ViewBag.Categories = _unitOfWork.CategoryRepository.GetAll();

            return View(toEdit);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(Article toEdit)
        {
            string userId = _userManager.GetUserId(User);

            _articleService.UpdateArticle(toEdit, userId);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            string userId = _userManager.GetUserId(User);

            _articleService.Delete(id, userId);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Details(int id)
        {
            Article article = _unitOfWork.ArticleRepository.GetByKey(id);

            ViewBag.Contributors = _articleService.GetContributors(article.ArticleContributors);

            ViewBag.Comments = _unitOfWork.CommentRepository.GetCommentsByArticle(id);

            return View(article);
        }
    }
}