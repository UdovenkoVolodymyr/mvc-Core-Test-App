using MvcEducationApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
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
        public virtual string UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Lesson> Lesson { get; set; }
    }
}
