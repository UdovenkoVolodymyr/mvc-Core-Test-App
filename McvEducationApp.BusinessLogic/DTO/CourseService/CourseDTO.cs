using MvcEducationApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace McvEducationApp.BusinessLogic.DTO
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Сategory { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastUpdated { get; set; }
        public string CreatedById { get; set; }
        public User CreatedBy { get; set; }
        public ICollection<UserCourse> UserCourses { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<LinkedCourseEntity> LinkedCourses { get; set; }
        public int LinkedCourseId { get; set; }
        public int UnlinkCourseId { get; set; }
    }
}
