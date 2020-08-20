using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MvcEducationApp.Domain.Core.Models
{
    public class VideoFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Lesson Lesson { get; set; }
        public int LessonId { get; set; }
    }
}
