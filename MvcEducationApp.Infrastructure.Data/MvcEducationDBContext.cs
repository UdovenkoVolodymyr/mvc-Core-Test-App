using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MvcEducationApp.Domain.Core.Models;

namespace MvcEducationApp.Infrastructure.Data
{
    public class MvcEducationDBContext : IdentityDbContext<User>
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Lesson> Lesson { get; set; }
        public DbSet<Course> LinkedCourseEntity { get; set; }
        public DbSet<VideoFile> VideoFiles { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }

        public MvcEducationDBContext(DbContextOptions<MvcEducationDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var infoTypeCovertor = new EnumToStringConverter<InfoType>();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserCourse>()
                .HasKey(k => new { k.CourseId, k.UserId });

            modelBuilder.Entity<UserCourse>()
                .HasOne(u => u.User)
                .WithMany(uc => uc.UserCourses)
                .HasForeignKey(k => k.UserId);
            modelBuilder.Entity<UserCourse>()
                .HasOne(u => u.Course)
                .WithMany(uc => uc.UserCourses)
                .HasForeignKey(k => k.CourseId);

            modelBuilder.Entity<Lesson>()
                .HasOne(c => c.VideoFile)
                .WithOne(b => b.Lesson)
                .HasForeignKey<VideoFile>(k => k.LessonId);


            modelBuilder.Entity<Course>()
                .HasMany(p => p.Lessons)
                .WithOne(p => p.Course).IsRequired()
                .HasForeignKey(fk => fk.CourseId);

            modelBuilder.Entity<LinkedCourseEntity>()
                 .HasKey(x => new { x.LinkedCourseId, x.CourseId });

            modelBuilder.Entity<LinkedCourseEntity>()
                 .HasOne(pt => pt.LinkedCourse)
                 .WithMany() // p => p.LinkedCoursesOf
                 .HasForeignKey(pt => pt.LinkedCourseId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LinkedCourseEntity>()
                .HasOne(pt => pt.Course)
                .WithMany(t => t.LinkedCourses)
                .HasForeignKey(pt => pt.CourseId);

            modelBuilder.Entity<Lesson>()
                .Property(e => e.InfoType)
                .HasConversion(infoTypeCovertor);
        }
    }
}
