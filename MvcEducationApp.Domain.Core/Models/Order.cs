using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEducationApp.Domain.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CardNumber { get; set; }
        public string ExpData { get; set; }
        public int Cvv { get; set; }
        public int CourseId { get; set; }
        //public Course Сourse { get; set; }
    }
}
