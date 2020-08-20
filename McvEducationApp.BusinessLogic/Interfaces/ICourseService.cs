using McvEducationApp.BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace McvEducationApp.BusinessLogic.Interfaces
{
    public interface ICourseService
    {
        CourseDTO GetCourse(int id);
        IEnumerable<CourseDTO> GetAllCourse();
        IEnumerable<CourseDTO> GetUserCourses(string userId);
        void CreateCourse(CourseDTO courseDTO);
        void DeleteCourse(int[] Ids);
        void EditCourse(CourseDTO courseDTO);
        void AddLinkedCourse(CourseDTO courseDTO);
        void UnlinkCourse(CourseDTO courseDTO);
    }
}
