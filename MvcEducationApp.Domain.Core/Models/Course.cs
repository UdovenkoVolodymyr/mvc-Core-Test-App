using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MvcEducationApp.Domain.Core.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Сategory { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastUpdated { get; set; }

        public virtual ICollection<UserCourse> UserCourses { get; set; }
        public virtual string CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<LinkedCourseEntity> LinkedCourses { get; set; }
        //public virtual ICollection<LinkedCourseEntity> LinkedCoursesOf { get; set; }

    }
}
