using McvEducationApp.BusinessLogic.DTO;
using MvcEducationApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace McvEducationApp.BusinessLogic.Interfaces
{
    public interface ICourseService
    {
        CourseDTO FullGetCourse(int id);
        IEnumerable<CourseDTO> GetAllCourse();
        PageViewModel<ShortCourseViewDTO> GetUserCourses(string userId, int pageSize, int? page);
        PageViewModel<ShortCourseViewDTO> GetPaginatedCoursesForIndexPage(int? page, int pageSize);
        void CreateCourse(CourseDTO courseDTO);
        void DeleteCourse(int[] Ids);
        void EditCourse(CourseDTO courseDTO);
        void AddLinkedCourse(CourseDTO courseDTO);
        void UnlinkCourse(CourseDTO courseDTO);
    }
}
