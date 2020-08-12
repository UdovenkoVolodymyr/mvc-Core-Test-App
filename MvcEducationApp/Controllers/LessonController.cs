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
        private IUnitOfWork _unitOfWork;
        private ILogger<HomeController> _logger;
        private UserManager<User> _userManager;

        public LessonController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
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
            var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            var course = _unitOfWork.Repository<Course>().FindById(model.CourseId);
            model.LastUpdated = DateTime.UtcNow;
            model.Course = course;
            model.User = user;
            _unitOfWork.Repository<Lesson>().Create(model);
            return RedirectToAction("EditCourseBody", "Course", new { Id = model.CourseId });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var lesson = _unitOfWork.Repository<Lesson>().FindById(id);
            return View("Edit", lesson);
        }

        [HttpPost]
        public IActionResult Edit(Lesson model)
        {
            var modelToEdit = _unitOfWork.Repository<Lesson>().FindById(model.Id);
            modelToEdit.LastUpdated = DateTime.UtcNow;
            modelToEdit.Title = model.Title;
            modelToEdit.Description = model.Description;
            modelToEdit.Text = model.Text;
            _unitOfWork.Repository<Lesson>().Update(modelToEdit);
            return RedirectToAction("EditCourseBody", "Course", new { Id = modelToEdit.CourseId });
        }

        [HttpGet]
        public IActionResult Delete()
        {
            var Ids = (int[])TempData["deleteList"];
            foreach (var lessonId in Ids)
            {
                var lessonToDelete = _unitOfWork.Repository<Lesson>().FindById(lessonId);
                _unitOfWork.Repository<Lesson>().Remove(lessonToDelete);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}