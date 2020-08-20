using MvcEducationApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MvcEducation.Domain.Interfaces
{
    public interface IUserCourseRepository : IGenericRepository<UserCourse>
    {
        IEnumerable<UserCourse> GetUserCourseWithAllInclude(string id);
    }
}
