using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcEducation.Domain.Interfaces;
using MvcEducationApp.Domain.Core.Models;
using MvcEducationApp.ViewModels;
using System.Security.Claims;

namespace MvcEducationApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class CourseController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private ILogger<HomeController> _logger;
        private UserManager<User> _userManager;

        public CourseController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(Course model)
        {
            var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            model.User = user;
            _unitOfWork.GetRepository<Course>().Create(model);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Delete()
        {
            var Ids = (int[])TempData["deleteList"];
            foreach (var courseId in Ids)
            {
                var courseToDelete = _unitOfWork.GetRepository<Course>().FindById(courseId);
                _unitOfWork.GetRepository<Course>().Remove(courseToDelete);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult EditCourseBody(int id)
        {
            ViewBag.CourseId = id;
            var course = _unitOfWork.GetRepository<Course>().FindById(id);
            return View("Edit", course);
        }
        
        [HttpPost]
        public IActionResult EditCourseBody(Course model)
        {
            var modelToEdit = _unitOfWork.GetRepository<Course>().FindById(model.Id);
            modelToEdit.Title = model.Title;
            modelToEdit.Price = model.Price;
            modelToEdit.Сategory = model.Сategory;
            modelToEdit.Description = model.Description;
            modelToEdit.LastUpdated = DateTime.UtcNow;
            _unitOfWork.GetRepository<Course>().Update(modelToEdit);
            return RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        public IActionResult EditCourseLessons(LessonViewModel model, string submitbutton, int Id)
        {
            switch (submitbutton)
            {
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