using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcEducationApp.Domain.Core.Models;

namespace MvcEducationApp.Infrastructure.Data
{
    public class MvcEducationDBContext : IdentityDbContext<User>
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Lesson> Lesson { get; set; }

        public MvcEducationDBContext(DbContextOptions<MvcEducationDBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
