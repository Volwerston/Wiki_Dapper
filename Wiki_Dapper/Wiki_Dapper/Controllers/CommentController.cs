using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Wiki_Dapper.DataAccess.Interfaces;
using Wiki_Dapper.Entities.Models;

namespace Wiki_Dapper.Controllers
{
    public class CommentController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private UserManager<ApplicationUser> _userManager;

        public CommentController(IUnitOfWork uow, UserManager<ApplicationUser> uMgr)
        {
            _unitOfWork = uow;
            _userManager = uMgr;
        }

        [Authorize]
        public IActionResult Index(int id)
        {
            ViewBag.ArticleId = id;

            Comment toCreate = new Comment();

            return View(toCreate);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(Comment comment, int articleId)
        {
            comment.AddingTime = DateTime.Now;
            comment.AuthorId = _userManager.GetUserId(User);
            comment.ArticleId = articleId;

            _unitOfWork.CommentRepository.Add(comment);

            return RedirectToAction("Index", "Home");
        }
    }
}