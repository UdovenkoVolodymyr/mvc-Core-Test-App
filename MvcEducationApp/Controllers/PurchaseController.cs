using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using McvEducationApp.BusinessLogic.DTO;
using McvEducationApp.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcEducation.Domain.Interfaces;
using MvcEducationApp.Domain.Core.Models;

namespace MvcEducationApp.Controllers
{
    [Authorize]
    public class PurchaseController : Controller
    {
        private ILogger<HomeController> _logger;
        private UserManager<User> _userManager;
        private IBuyService _buyService;
        private IMapper _mapper;

        public PurchaseController(ILogger<HomeController> logger, UserManager<User> userManager, IBuyService buyService, IMapper mapper)
        {
            _logger = logger;
            _userManager = userManager;
            _buyService = buyService;
            _mapper = mapper;
        }

        [HttpGet]       
        public IActionResult Buy(int? Id)
        {
            if (Id == null) return RedirectToAction("Index");
            ViewBag.CourseId = Id;
            return View("Buy");
        }
        [HttpPost]
        public async Task<IActionResult> Buy(Order order)
        {
            var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            var orderDTO = _mapper.Map<Order, OrderDTO>(order);
            orderDTO.UserId = user.Id;
            _buyService.Buy(orderDTO);
            return RedirectToAction("Index", "Home");
        }
    }
}