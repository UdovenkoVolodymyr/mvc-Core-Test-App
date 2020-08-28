using System;
using System.Collections.Generic;
using System.Text;

namespace McvEducationApp.BusinessLogic.DTO
{
    public class OrderDTO
    {
        public int CardNumber { get; set; }
        public string ExpData { get; set; }
        public int Cvv { get; set; }
        public int CourseId { get; set; }
        public string UserId { get; set; }
    }
}
