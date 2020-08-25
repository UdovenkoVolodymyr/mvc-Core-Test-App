using Microsoft.AspNetCore.Mvc;
using MvcEducationApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEducationApp.ViewModels
{
    public class CoureseViewModel
    {
        [BindProperty]
        public List<int> AreChecked { get; set; }
        [BindProperty]
        public string submitbutton { get; set; }
        public ICollection<Course> UserCourses { get; set; }
    }
}
