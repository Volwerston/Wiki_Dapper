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
    public class CategoryController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private IUnitOfWork _unifOfWork;

        public CategoryController(UserManager<ApplicationUser> uMgr, IUnitOfWork uow)
        {
            _userManager = uMgr;
            _unifOfWork = uow;
        }

        [Authorize]
        public IActionResult Create()
        {
            Category toCreate = new Category();

            return View(toCreate);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(Category category)
        {
            category.CreatorId = _userManager.GetUserId(User);

            _unifOfWork.CategoryRepository.Add(category);

            return RedirectToAction("Index", "Home");
        }
    }
}