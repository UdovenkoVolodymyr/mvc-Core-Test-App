using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcEducation.Domain.Interfaces;
using MvcEducationApp.Domain.Core.Models;

namespace MvcEducationApp.Controllers
{
    [Route("[controller]/[action]")]
    public class LessonController : Controller
    {
        private IGenericRepository<Lesson> _lessonRepo;
        private IGenericRepository<Course> _courseRepo;
        private IGenericRepository<User> _userRepo;
        private ILogger<HomeController> _logger;
        private UserManager<User> _userManager;

        public LessonController(ILogger<HomeController> logger, IGenericRepository<Lesson> lessonRepo, IGenericRepository<Course> courseRepo,
            IGenericRepository<User> userRepo, UserManager<User> userManager)
        {
            _logger = logger;
            _lessonRepo = lessonRepo;
            _courseRepo = courseRepo;
            _userRepo = userRepo;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Create(int Id)
        {
            ViewBag.CourseId = Id;
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Lesson model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var course = _courseRepo.FindById(model.CourseId);
            model.LastUpdated = DateTime.UtcNow;
            model.Course = course;
            model.User = user;
            _lessonRepo.Create(model);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.CourseId = id;
            TempData["CourseId"] = id;
            var lesson = _lessonRepo.FindById(id);
            return View("Edit", lesson);
        }

        [HttpPost]
        public IActionResult Edit(Lesson model)
        {
            var courseId = (int)TempData["CourseId"];
            model.LastUpdated = DateTime.UtcNow;
            model.CourseId = courseId;
            model.Course = _courseRepo.FindById((int)TempData["CourseId"]);
            _lessonRepo.Update(model);
            return RedirectToAction("EditCourseBody", "Course", new { Id = TempData["CourseId"] });
        }

        [HttpGet]
        public IActionResult Delete()
        {
            var Ids = (int[])TempData["deleteList"];
            foreach (var lessonId in Ids)
            {
                var lessonToDelete = _lessonRepo.FindById(lessonId);
                _lessonRepo.Remove(lessonToDelete);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}