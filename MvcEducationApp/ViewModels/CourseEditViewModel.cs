using Microsoft.AspNetCore.Mvc.Rendering;
using MvcEducationApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEducationApp.ViewModels
{
    public class CourseEditViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Сategory { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastUpdated { get; set; }

        public virtual string UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Lesson> Lesson { get; set; }
        public virtual ICollection<Course> LinkedCourse { get; set; }

        public int UnlinkCourseId { get; set; }
        public int LinkedCourseId { get; set; }
    }
}
