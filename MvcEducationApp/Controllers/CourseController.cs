using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MvcEducation.Domain.Interfaces;
using MvcEducationApp.Domain.Core.Models;
using MvcEducationApp.ViewModels;
using System.Security.Claims;
using McvEducationApp.BusinessLogic.Interfaces;
using McvEducationApp.BusinessLogic.DTO;

namespace MvcEducationApp.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private ILogger<HomeController> _logger;
        private UserManager<User> _userManager;
        private ICourseService _courseService;
        private IMapper _mapper;

        public CourseController(ILogger<HomeController> logger, UserManager<User> userManager, ICourseService courseService, IMapper mapper)
        {
            _logger = logger;
            _userManager = userManager;
            _courseService = courseService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ViewCourseDetails(int id)
        {
            var course = _courseService.GetCourse(id);
            return View("ViewCourseDetails", course);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserCourses()
        {
            var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            var userCourses = _courseService.GetUserCourses(user.Id);
            return View("UserCourses", userCourses);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Course model)
        {
            var courseDTO = _mapper.Map<Course, CourseDTO>(model);

            var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            courseDTO.CreatedBy = user;

            _courseService.CreateCourse(courseDTO);
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Delete()
        {
            var idsToDelete = (int[])TempData["deleteList"];
            _courseService.DeleteCourse(idsToDelete);

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult EditCourseBody(int id)
        {
            ViewBag.CourseId = id;
            var course = _courseService.GetCourse(id);
            return View("Edit", course);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult EditCourseBody(Course model)
        {
            _courseService.EditCourse(_mapper.Map<Course, CourseDTO>(model));
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddLinkedCourse(CourseEditViewModel model)
        {
            var courseDTO = _mapper.Map<CourseEditViewModel, CourseDTO>(model);
            _courseService.AddLinkedCourse(courseDTO);
            return RedirectToAction("EditCourseBody", "Course", new { Id = model.Id });
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult UnlinkCourse(CourseEditViewModel model)
        {
            var courseDTO = _mapper.Map<CourseEditViewModel, CourseDTO>(model);
            _courseService.UnlinkCourse(courseDTO);
            return RedirectToAction("EditCourseBody", "Course", new { Id = model.Id });
        }

        [HttpPost]
        public ActionResult GetCourseForDropdown()
        {
            var courseDTO = _courseService.GetAllCourse();
            return Json(courseDTO);
        }

        [Authorize(Roles = "admin")]
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
                TempData["CourseId"] = courseId;
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
                TempData["courseId"] = courseId;
                return RedirectToAction("Delete", "Lesson");
            }
        }
    }
}