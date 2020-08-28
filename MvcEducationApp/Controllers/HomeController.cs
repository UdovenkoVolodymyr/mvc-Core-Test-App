using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using McvEducationApp.BusinessLogic.DTO;
using McvEducationApp.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcEducation.Domain.Interfaces;
using MvcEducationApp.Domain.Core.Models;
using MvcEducationApp.Infrastructure.Data;
using MvcEducationApp.Models;
using MvcEducationApp.ViewModels;
using Newtonsoft.Json.Schema;

namespace MvcEducationApp.Controllers
{
    public class HomeController : Controller
    {
        public static int _coursePageSize = 3;
        private readonly ILogger<HomeController> _logger;
        private ICourseService _courseService;

        public HomeController(ILogger<HomeController> logger, ICourseService courseService)
        {
            _logger = logger;
            _courseService = courseService;
        }

        [HttpGet]
        public IActionResult Index(int? page)
        {
            var coursePageView = _courseService.GetPaginatedCoursesForIndexPage(page, _coursePageSize);

            return View("Index", coursePageView);
        }

        [HttpPost]
        public IActionResult Index(CoureseViewModel model, string submitbutton)
        {
            switch (submitbutton)
            {
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
                var coursePageView = _courseService.GetPaginatedCoursesForIndexPage(null, _coursePageSize);
                return View("Index", coursePageView);
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
                return RedirectToAction("Buy", "Purchase", new { Id = model.AreChecked[0] });
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
