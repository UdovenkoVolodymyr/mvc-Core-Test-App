using AutoMapper;
using McvEducationApp.BusinessLogic.DTO;
using McvEducationApp.BusinessLogic.Interfaces;
using MvcEducation.Domain.Interfaces;
using MvcEducationApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace McvEducationApp.BusinessLogic.Services
{
    public class BuyService : IBuyService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public BuyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Buy(OrderDTO orderDTO)
        {
            var order = _mapper.Map<OrderDTO, Order>(orderDTO);
            _unitOfWork.GetRepository<Order>().Create(order);

            var userCourse = new UserCourse { UserId = orderDTO.UserId, CourseId = orderDTO.CourseId };
            _unitOfWork.GetRepository<UserCourse>().Create(userCourse);
        }
    }
}
