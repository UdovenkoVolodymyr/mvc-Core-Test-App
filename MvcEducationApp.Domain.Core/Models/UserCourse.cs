﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MvcEducationApp.Domain.Core.Models
{
    public class UserCourse
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
