using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using McvEducationApp.BusinessLogic.DTO;
using McvEducationApp.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcEducation.Domain.Interfaces;
using MvcEducationApp.Domain.Core.Models;
using MvcEducationApp.ViewModels;

namespace MvcEducationApp.Controllers
{
    [Route("[controller]/[action]")]
    public class LessonController : Controller
    {
        private ILogger<HomeController> _logger;
        private IWebHostEnvironment _appEnvironment;
        private UserManager<User> _userManager;
        private ILessonService _lessonService;
        private ICourseService _courseService;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        private IVideoService _videoService;

        public LessonController(ILogger<HomeController> logger, UserManager<User> userManager, ILessonService lessonService,
            ICourseService courseService, IMapper mapper, IWebHostEnvironment appEnvironment, IUnitOfWork unitOfWork, IVideoService videoService)
        {
            _logger = logger;
            _userManager = userManager;
            _lessonService = lessonService;
            _courseService = courseService;
            _mapper = mapper;
            _appEnvironment = appEnvironment;
            _unitOfWork = unitOfWork;
            _videoService = videoService;
        }

        [HttpGet]
        public IActionResult ViewLessonDetails(int id)
        {
            var lesson = _lessonService.GetLesson(id);
            return View("ViewLessonDetails", lesson);
        }

        [HttpGet]
        public IActionResult Create(int Id)
        {
            ViewBag.CourseId = Id;
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(LessonEditViewModel model)
        {
            var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            model.CourseId = model.CourseId;
            model.CreatedBy = user;

            var lessonDTO = _mapper.Map<LessonEditViewModel, LessonDTO>(model);
            var createdLessonId = _lessonService.CreateLesson(lessonDTO);

            if (model.UploadedFile != null)
            {
                AddFile(model.UploadedFile, createdLessonId);
            }

            return RedirectToAction("EditCourseBody", "Course", new { Id = model.CourseId });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var lesson = _lessonService.GetLesson(id);
            var lessonEditViewModel = _mapper.Map<LessonDTO, LessonEditViewModel>(lesson);
            var noNelectedFileTypes = Enum.GetNames(typeof(InfoType)).ToList();

            noNelectedFileTypes.Remove(lessonEditViewModel.SelectedFileType);
            lessonEditViewModel.NoSelectedFileType = noNelectedFileTypes;
            lessonEditViewModel.SelectedFileType = lesson.InfoType.ToString();

            return View("Edit", lessonEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(LessonEditViewModel model)
        {
            var courseId = (int)TempData["CourseId"];
            _lessonService.EditLesson(_mapper.Map<Lesson, LessonDTO>(model));

            if (model.UploadedFile != null)
            {
                AddFile(model.UploadedFile, model.Id);
            }

            return RedirectToAction("EditCourseBody", "Course", new { Id = courseId });
        }

        [HttpGet]
        public IActionResult Delete()
        {
            var ids = (int[])TempData["deleteList"];
            _lessonService.DeleteLesson(ids);
            return RedirectToAction("EditCourseBody", "Course", new { Id = (int)TempData["courseId"] });
        }

        private void AddFile(IFormFile uploadedFile, int lessonId)
        {
             if (uploadedFile != null)
             {
                var uploadedFileStream = uploadedFile.OpenReadStream();
                var rootFolder = _appEnvironment.WebRootPath;
                _videoService.UploadFileAsync(uploadedFileStream, rootFolder, lessonId);
                
             }
        }
    }
}