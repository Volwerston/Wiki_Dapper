using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wiki_Dapper.DataAccess.Interfaces;
using Wiki_Dapper.Entities.Models;
using Wiki_Dapper.Models.View;

namespace Wiki_Dapper.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Article> articles = _unitOfWork.ArticleRepository.GetAll();

            ViewBag.Statistics = _unitOfWork.GetArticleStatistics();

            return View(articles);
        }
    }
}