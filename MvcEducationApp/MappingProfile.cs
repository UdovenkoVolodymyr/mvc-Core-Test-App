using AutoMapper;
using McvEducationApp.BusinessLogic.DTO;
using MvcEducationApp.Domain.Core.Models;
using MvcEducationApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEducationApp
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDTO>();
            CreateMap<CourseDTO, Course>();
            CreateMap<Lesson, LessonDTO>();
            CreateMap<LessonDTO, Lesson>();
            CreateMap<LessonDTO, LessonEditViewModel>();
            CreateMap<LessonEditViewModel, LessonDTO>();
            CreateMap<CourseEditViewModel, CourseDTO>();
            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDTO, Order>();
        }
    }
}
