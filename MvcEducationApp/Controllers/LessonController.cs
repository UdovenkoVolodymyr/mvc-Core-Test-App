using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcEducation.Domain.Interfaces;
using MvcEducationApp.Domain.Core.Models;

namespace MvcEducationApp.Controllers
{
    public class LessonController : Controller
    {
        private IGenericRepository<Lesson> _lessonRepo;
        private IGenericRepository<Course> _courseRepo;
        private IGenericRepository<User> _userRepo;
        private ILogger<HomeController> _logger;

        public LessonController(ILogger<HomeController> logger, IGenericRepository<Lesson> lessonRepo, IGenericRepository<Course> courseRepo,
            IGenericRepository<User> userRepo)
        {
            _logger = logger;
            _lessonRepo = lessonRepo;
            _courseRepo = courseRepo;
            _userRepo = userRepo
        }

        [HttpGet]
        public IActionResult Create(int Id)
        {
            ViewBag.CourseId = Id;
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(Lesson model, int Id)
        {
            var course = _courseRepo.FindById(Id);
            var user = _userRepo.FindById(Convert.ToInt32(course.User.Id));
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
            var course = _lessonRepo.FindById(id);
            return View("Edit", course);
        }

        [HttpPost]
        public IActionResult Edit(Lesson model)
        {
            model.LastUpdated = DateTime.UtcNow;
            _lessonRepo.Update(model);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Delete()
        {
            var Ids = (int[])TempData["deleteList"];
            foreach (var courseId in Ids)
            {
                var lessonToDelete = _lessonRepo.FindById(courseId);
                _lessonRepo.Remove(lessonToDelete);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}