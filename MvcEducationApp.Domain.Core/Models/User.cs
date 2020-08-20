using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEducationApp.Domain.Core.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
        public virtual ICollection<UserCourse> UserCourses { get; set; }
    }
}
