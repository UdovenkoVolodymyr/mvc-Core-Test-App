using System;
using System.Collections.Generic;
using System.Text;

namespace MvcEducationApp.Domain.Core.Models
{
    public class LinkedCourseEntity
    {
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public int LinkedCourseId { get; set; }
        public virtual Course LinkedCourse { get; set; }
    }
}
