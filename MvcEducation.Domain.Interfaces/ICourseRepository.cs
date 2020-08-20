using MvcEducationApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MvcEducation.Domain.Interfaces
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Course GetCourseWithAllInclude(int id);
    }
}
