using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcEducation.Domain.Interfaces;
using MvcEducationApp.Domain.Core.Models;

namespace MvcEducationApp.Controllers
{
    public class PurchaseController : Controller
    {
        private IGenericRepository<Order> _orderRepo;
        private ILogger<HomeController> _logger;

        public PurchaseController(ILogger<HomeController> logger, IGenericRepository<Order> orderRepo)
        {
            _logger = logger;
            _orderRepo = orderRepo;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Buy(int? Id)
        {
            if (Id == null) return RedirectToAction("Index");
            ViewBag.CourseId = Id;
            return View("Buy");
        }
        [HttpPost]
        [Authorize]
        public string Buy(Order order)
        {
            order.Id = 0;
            _orderRepo.Create(order);
            return "purchase successful !";
        }
    }
}