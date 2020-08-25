using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using McvEducationApp.BusinessLogic.DTO;
using McvEducationApp.BusinessLogic.Interfaces;
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
        private IUnitOfWork _unitOfWork;
        private ICourseService _courseService;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, ICourseService courseService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _courseService = courseService;
        }

        [HttpGet]
        public IActionResult Index(int? page)
        {
            var coursePageview = _unitOfWork.GetCourseRepository().GetAllCourseWithPaginate(page);

            return View("Index", coursePageview);
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
                var returnModel = _unitOfWork.GetRepository<Course>().Get();
                return View("Index", returnModel);
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
