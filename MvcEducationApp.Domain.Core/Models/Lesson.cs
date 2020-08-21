using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MvcEducationApp.Domain.Core.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastUpdated { get; set; }
        public InfoType InfoType { get; set; }
        public virtual string CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public virtual VideoFile VideoFile { get; set; }
    }

    public enum InfoType
    {
        Text,
        Video
    }
}
