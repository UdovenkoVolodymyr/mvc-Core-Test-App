using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcEducation.Domain.Interfaces;
using MvcEducationApp.Domain.Core.Models;
using MvcEducationApp.ViewModels;

namespace MvcEducationApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class CourseController : Controller
    {
        private IGenericRepository<Course> _courseRepo;
        private IGenericRepository<Lesson> _lessonRepo;
        private ILogger<HomeController> _logger;

        public CourseController(ILogger<HomeController> logger, IGenericRepository<Course> courseRepo, IGenericRepository<Lesson> lessonRepo)
        {
            _logger = logger;
            _courseRepo = courseRepo;
            _lessonRepo = lessonRepo;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }
        
        [HttpPost]
        public IActionResult Create(Course model)
        {
            model.LastUpdated = DateTime.UtcNow;
            _courseRepo.Create(model);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Delete()
        {
            var Ids = (int[])TempData["deleteList"];
            foreach (var courseId in Ids)
            {
                var courseToDelete = _courseRepo.FindById(courseId);
                _courseRepo.Remove(courseToDelete);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult EditCourseBody(int id)
        {
            ViewBag.CourseId = id;
            var course = _courseRepo.FindById(id);
            return View("Edit", course);
        }
        
        [HttpPost]
        public IActionResult EditCourseBody(Course model)
        {
            model.LastUpdated = DateTime.UtcNow;
            _courseRepo.Update(model);
            return RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        public IActionResult EditCourseLessons(LessonViewModel model, string submitbutton, int Id)
        {
            switch (submitbutton)
            {
                case "Create": return RedirectToAction("Create", "Lesson", new { Id });
                case "Edit": return EditLessonHandler(model, Id);
                case "Delete": return DeleteLessonHandler(model, Id);
            }
            return RedirectToAction("Index", "Home");
        }

        private IActionResult EditLessonHandler(LessonViewModel model, int courseId)
        {
            if (model.AreChecked == null || model.AreChecked.Count > 1)
            {
                return RedirectToAction("EditCourseBody", "Course", new { Id = courseId });
            }
            else
            {
                return RedirectToAction("Edit", "Lesson", new { Id = model.AreChecked[0] });
            }
        }

        private IActionResult DeleteLessonHandler(LessonViewModel model, int courseId)
        {
            if (model.AreChecked == null)
            {
                return RedirectToAction("EditCourseBody", "Course", new { Id = courseId });
            }
            else
            {
                TempData["deleteList"] = model.AreChecked;
                return RedirectToAction("Delete", "Lesson");
            }
        }
    }
}