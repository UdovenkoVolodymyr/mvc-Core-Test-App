using Microsoft.AspNetCore.Http;
using MvcEducationApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEducationApp.ViewModels
{
    public class LessonEditViewModel : Lesson
    {
        public IFormFile UploadedFile { get; set; }
        public string SelectedFileType { get; set; }
        public IEnumerable<string> NoSelectedFileType { get; set; }
    }
}
