using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcEducation.Domain.Interfaces;
using MvcEducationApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ILogger<CourseController> _logger;
        private IUnitOfWork _unitOfWork;

        public CourseController(ILogger<CourseController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

    /*    [HttpGet]
        public ActionResult<PageViewModel<Course>> Get()
        {
            var coursePageview = _unitOfWork.GetCourseRepository().GetAllCourseWithPaginate(null);
            return coursePageview;      }*/
    }
}
