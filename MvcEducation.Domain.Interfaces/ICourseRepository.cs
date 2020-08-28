using MvcEducationApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MvcEducation.Domain.Interfaces
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Course GetCourseWithAllInclude(int id);
        PageViewModel<Dto> GetAllCourseWithPaginate<Dto>(int? page, Expression<Func<Course, int, Dto>> selector) where Dto : class;
    }
}
