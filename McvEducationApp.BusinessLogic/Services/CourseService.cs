using McvEducationApp.BusinessLogic.DTO;
using McvEducationApp.BusinessLogic.Interfaces;
using MvcEducation.Domain.Interfaces;
using MvcEducationApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using System.Linq;
using System.Linq.Expressions;
using MvcEducationApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;

namespace McvEducationApp.BusinessLogic.Services
{
    public class CourseService : ICourseService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ICourseRepository _courseRepository;

        public CourseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _courseRepository = _unitOfWork.GetCourseRepository();
        }

        public CourseDTO FullGetCourse(int id)
        {
            var course = GetFullCourseByIdHelper(id);
            var courseDTO = _mapper.Map<Course, CourseDTO>(course);
            return courseDTO;

        }
        
        public IEnumerable<CourseDTO> GetAllCourse()
        {
            Func<IQueryable<Course>, IIncludableQueryable<Course, object>> includes = sourse => sourse
                .Include(a => a.CreatedBy);

            var regularCourseQuery = new RegularQuery<Course, Course>(

                Include: includes,
                Criteria: null,
                Selector: null
            );
            var courses = _unitOfWork.GetRepository<Course>().GetList(regularCourseQuery);
            var courseDTO = _mapper.Map<IEnumerable<Course>, IEnumerable<CourseDTO>>(courses);

            return courseDTO;
        }

        public PageViewModel<ShortCourseViewDTO> GetUserCourses(string userId, int pageSize, int? page)
        {
            Func<IQueryable<UserCourse>, IIncludableQueryable<UserCourse, object>> includes = sourse => sourse
                .Include(a => a.Course);

            Expression<Func<UserCourse, bool>> criteria = userCourse => userCourse.UserId == userId;
            Expression<Func<UserCourse, ShortCourseViewDTO>> selector = course => new ShortCourseViewDTO
            {
                Id = course.Course.Id,
                Title = course.Course.Title,
                Description = course.Course.Description,
                Price = course.Course.Price,
                CreatedBy = new User { Email = course.Course.CreatedBy.Email }
            };

            var paginatedCourseQuery = new PaginatedQuery<UserCourse, ShortCourseViewDTO>(
                PageNumber: page,
                PageSize: pageSize,
                Include: includes,
                Criteria: criteria,
                Selector: selector
            );
            var courses = _unitOfWork.GetRepository<UserCourse>().GetPaginatedListWithSelect(paginatedCourseQuery);

            return courses;
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
            var courseToEdit = FullGetCourse(courseDTO.Id);
            courseToEdit.Title = courseDTO.Title;
            courseToEdit.Price = courseDTO.Price;
            courseToEdit.Сategory = courseDTO.Сategory;
            courseToEdit.Description = courseDTO.Description;

            var course = _mapper.Map<CourseDTO, Course>(courseToEdit);
            course.LastUpdated = DateTime.UtcNow;
            _courseRepository.Update(course);
        }

        public void AddLinkedCourse(CourseDTO courseDTO)
        {
            var course = GetFullCourseByIdHelper(courseDTO.Id);
            var matchedLinkedCourse = course.LinkedCourses.Where(c => c.LinkedCourseId == courseDTO.LinkedCourseId).FirstOrDefault();

            if (matchedLinkedCourse == null)
            {
                var newLinkedCourse = new LinkedCourseEntity { CourseId = courseDTO.Id, LinkedCourseId = courseDTO.LinkedCourseId };
                _unitOfWork.GetRepository<LinkedCourseEntity>().Create(newLinkedCourse);
            }
        }
        
        public void UnlinkCourse(CourseDTO courseDTO)
        {
            var course = GetFullCourseByIdHelper(courseDTO.Id);
            var matchedLinkedCourse = course.LinkedCourses.Where(c => c.LinkedCourseId == courseDTO.UnlinkCourseId).FirstOrDefault();

            if (matchedLinkedCourse != null)
            {
                _unitOfWork.GetRepository<LinkedCourseEntity>().Remove(matchedLinkedCourse);
            }
        }

        public PageViewModel<ShortCourseViewDTO> GetPaginatedCoursesForIndexPage(int? page, int pageSize)
        {
            Func<IQueryable<Course>, IIncludableQueryable<Course, object>> includes = sourse => sourse
                .Include(a => a.CreatedBy);

            Expression<Func<Course, ShortCourseViewDTO>> selector = course => new ShortCourseViewDTO
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Price = course.Price,
                CreatedBy = new User { Email = course.CreatedBy.Email }
            };

            var paginatedCourseQuery = new PaginatedQuery<Course, ShortCourseViewDTO>(
                PageNumber: page,
                PageSize: pageSize,
                Include: includes,
                Criteria: null,
                Selector: selector
            );
            var courses = _unitOfWork.GetRepository<Course>().GetPaginatedListWithSelect(paginatedCourseQuery);

            return courses;
        }

        private Course GetFullCourseByIdHelper(int id)
        {
            Func<IQueryable<Course>, IIncludableQueryable<Course, object>> includes = sourse => sourse
                .Include(a => a.CreatedBy)
                .Include(a => a.LinkedCourses)
                .ThenInclude(l => l.LinkedCourse)
                .Include(a => a.Lessons);

            Expression<Func<Course, bool>> criteria = course => course.Id == id;

            var paginatedCourseQuery = new RegularQuery<Course, Course>(
                Include: includes,
                Criteria: criteria,
                Selector: null
            );

            var course = _courseRepository.GetById(paginatedCourseQuery);
            return course;

        }
    }
}
