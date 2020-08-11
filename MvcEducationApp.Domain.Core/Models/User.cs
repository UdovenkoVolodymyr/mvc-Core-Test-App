using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEducationApp.Domain.Core.Models
{
    public class User : IdentityUser<int>
    {
        public int Year { get; set; }
        public ICollection<Course> Course { get; set; }
        public ICollection<Lesson> Lesson { get; set; }
    }
}
