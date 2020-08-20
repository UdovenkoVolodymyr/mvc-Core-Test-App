using Microsoft.EntityFrameworkCore;
using MvcEducation.Domain.Interfaces;
using MvcEducationApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcEducationApp.Infrastructure.Data
{
    public class UserCourseRepository : EFGenericRepository<UserCourse>, IUserCourseRepository
    {
        MvcEducationDBContext _context;

        public UserCourseRepository(MvcEducationDBContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<UserCourse> GetUserCourseWithAllInclude(string id)
        {
            var entity = _context.UserCourses.AsNoTracking().Include(i => i.Course).ThenInclude(l => l.CreatedBy).Where(c => c.UserId == id);
            return entity;
        }
    }
}
