using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEducationApp.ViewModels
{
    public class LessonViewModel
    {
        [BindProperty]
        public List<int> AreChecked { get; set; }
    }
}
