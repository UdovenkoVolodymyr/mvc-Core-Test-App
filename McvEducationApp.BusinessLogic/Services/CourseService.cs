using McvEducationApp.BusinessLogic.DTO;
using McvEducationApp.BusinessLogic.Interfaces;
using MvcEducation.Domain.Interfaces;
using MvcEducationApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using System.Linq;

namespace McvEducationApp.BusinessLogic.Services
{
    public class CourseService : ICourseService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public CourseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public CourseDTO GetCourse(int id)
        {
            var course = _unitOfWork.GetCourseRepository().GetCourseWithAllInclude(id);
            var courseDTO = _mapper.Map<Course, CourseDTO>(course);
            return courseDTO;
        }
        
        public IEnumerable<CourseDTO> GetAllCourse()
        {
            var course = _unitOfWork.GetRepository<Course>().GetWithInclude(c => c.CreatedBy);
            var courseDTO = _mapper.Map<IEnumerable<Course>, IEnumerable<CourseDTO>>(course);
            return courseDTO;
        }

        public IEnumerable<CourseDTO> GetUserCourses(string userId)
        {
            //var userCourses = _unitOfWork.GetRepository<UserCourse>().GetWithInclude(x => x.UserId == userId, c => c.Course);
            var userCourses = _unitOfWork.GetUserCourseRepository().GetUserCourseWithAllInclude(userId);
            var courses = userCourses.Select(c => c.Course).ToList();
            var returnCourses = _mapper.Map<IEnumerable<Course>, IEnumerable<CourseDTO>>(courses);
            return returnCourses;
        }

        public void CreateCourse(CourseDTO courseDTO)
        {

            var course = _mapper.Map<CourseDTO, Course>(courseDTO);
            course.LastUpdated = DateTime.UtcNow;
            _unitOfWork.GetRepository<Course>().Create(course);
        }

        public void DeleteCourse(int[] IdsToDelete)
        {
            foreach (var courseId in IdsToDelete)
            {
                _unitOfWork.GetRepository<Course>().RemoveById(courseId);
            }
        }

        public void EditCourse(CourseDTO courseDTO)
        {
            var courseToEdit = GetCourse(courseDTO.Id);
            courseToEdit.Title = courseDTO.Title;
            courseToEdit.Price = courseDTO.Price;
            courseToEdit.Сategory = courseDTO.Сategory;
            courseToEdit.Description = courseDTO.Description;

            var course = _mapper.Map<CourseDTO, Course>(courseDTO);
            course.LastUpdated = DateTime.UtcNow;
            _unitOfWork.GetRepository<Course>().Update(course);
        }

        public void AddLinkedCourse(CourseDTO courseDTO)
        {
            var course = _unitOfWork.GetCourseRepository().GetCourseWithAllInclude(courseDTO.Id);
            var matchedLinkedCourse = course.LinkedCourses.Where(c => c.LinkedCourseId == courseDTO.LinkedCourseId).FirstOrDefault();

            if (matchedLinkedCourse == null)
            {
                var newLinkedCourse = new LinkedCourseEntity { CourseId = courseDTO.Id, LinkedCourseId = courseDTO.LinkedCourseId };
                _unitOfWork.GetRepository<LinkedCourseEntity>().Create(newLinkedCourse);
            }
        }
        
        public void UnlinkCourse(CourseDTO courseDTO)
        {
            var course = _unitOfWork.GetCourseRepository().GetCourseWithAllInclude(courseDTO.Id);
            var matchedLinkedCourse = course.LinkedCourses.Where(c => c.LinkedCourseId == courseDTO.UnlinkCourseId).FirstOrDefault();

            if (matchedLinkedCourse != null)
            {
                _unitOfWork.GetRepository<LinkedCourseEntity>().Remove(matchedLinkedCourse);
            }
        }
    }
}
