using McvEducationApp.BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace McvEducationApp.BusinessLogic.Interfaces
{
    public interface ICourseService
    {
        void CreateCourse(CourseDTO courseDTO);
    }
}
