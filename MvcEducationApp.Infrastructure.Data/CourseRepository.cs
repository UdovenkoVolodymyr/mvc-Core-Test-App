using Microsoft.EntityFrameworkCore;
using MvcEducation.Domain.Interfaces;
using MvcEducationApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MvcEducationApp.Infrastructure.Data
{
    public class CourseRepository : EFGenericRepository<Course>, ICourseRepository
    {
        MvcEducationDBContext _context;

        public CourseRepository(MvcEducationDBContext context) : base(context)
        {
            _context = context;
        }

        public Course GetCourseWithAllInclude(int id)
        {
            var entity = _context.Courses.AsNoTracking()
                .Include( i => i.Lessons)
                .Include(i => i.CreatedBy)
                .Include(i => i.LinkedCourses)
                .ThenInclude( l => l.LinkedCourse)
                .Where(x => x.Id == id).First();
            return entity;
        }
        
        public PageViewModel<Dto> GetAllCourseWithPaginate<Dto>(int? page, Expression<Func<Course, int, Dto>> selector)
            where Dto : class
        {
            var pageSize = 3;
            var courseCount = _context.Courses.Count();
            var displayCourse = _context.Courses
                .Skip(((page ?? 1) - 1) * pageSize)
                .Take(pageSize)
                .Include(i => i.Lessons)
                .Include(i => i.CreatedBy)
                .Include(i => i.LinkedCourses)
                .ThenInclude(l => l.LinkedCourse)
                .Select(selector)
                .ToList();

            var coursePageView = new PageViewModel<Dto>(courseCount, page ?? 1, pageSize, displayCourse);
            return coursePageView;
        }
    }
}
