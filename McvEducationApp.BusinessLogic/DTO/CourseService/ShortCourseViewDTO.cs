using MvcEducationApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace McvEducationApp.BusinessLogic.DTO
{
    public class ShortCourseViewDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Сategory { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastUpdated { get; set; }
        public User CreatedBy { get; set; }
    }
}
