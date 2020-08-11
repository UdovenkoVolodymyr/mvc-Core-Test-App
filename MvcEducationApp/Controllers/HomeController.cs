using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcEducation.Domain.Interfaces;
using MvcEducationApp.Domain.Core.Models;
using MvcEducationApp.Models;
using MvcEducationApp.ViewModels;

namespace MvcEducationApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IGenericRepository<Course> _courseRepo;

        public HomeController(ILogger<HomeController> logger, IGenericRepository<Course> courseRepo)
        {
            _logger = logger;
            _courseRepo = courseRepo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Index", _courseRepo.Get());
        }

        [HttpPost]
        public IActionResult Index(CoureseViewModel model, string submitbutton)
        {
            switch (submitbutton)
            {
                case "Create": return RedirectToAction("Create", "Course");
                case "Edit": return EditCourseHandler(model);
                case "Delete": return DeleteCourseHandler(model);
                case "Link": return RedirectToAction("Index", "Home");
                case "Buy": return BuyCourseHandler(model);
            }
            return RedirectToAction("Index", "Home");
        }

        private IActionResult EditCourseHandler(CoureseViewModel model)
        {
            if (model.AreChecked == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (model.AreChecked.Count > 1)
            {
                ViewBag.ErrorMessage = "You can not Edit more than 1 course at ones !";
                return View("Index", _courseRepo.Get());
            }
            else
            {
                return RedirectToAction("EditCourseBody", "Course", new { Id = model.AreChecked[0] });
            }
        }

        private IActionResult BuyCourseHandler(CoureseViewModel model)
        {
            if (model.AreChecked == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Buy", "Purchase", new { Id = model.AreChecked });
            }
        }
        private IActionResult DeleteCourseHandler(CoureseViewModel model)
        {
            if (model.AreChecked == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["deleteList"] = model.AreChecked;
                return RedirectToAction("Delete", "Course");
            }
        }
    }
}
