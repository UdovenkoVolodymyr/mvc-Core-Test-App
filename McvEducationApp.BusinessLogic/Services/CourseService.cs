using McvEducationApp.BusinessLogic.DTO;
using McvEducationApp.BusinessLogic.Interfaces;
using MvcEducation.Domain.Interfaces;
using MvcEducationApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace McvEducationApp.BusinessLogic.Services
{
    public class CourseService : ICourseService
    {
        private IUnitOfWork _unitOfWork;
        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateCourse(CourseDTO courseDTO)
        {
            var course = new Course
            {
                Title = courseDTO.Title,
                Сategory = courseDTO.Сategory,
                Price = courseDTO.Price,
                Description = courseDTO.Description,
                UserId = courseDTO.UserId,
                User = courseDTO.User,
                Lesson = courseDTO.Lesson,
                LastUpdated = DateTime.UtcNow
            };
            _unitOfWork.GetRepository<Course>().Create(course);
        }
    }
}
