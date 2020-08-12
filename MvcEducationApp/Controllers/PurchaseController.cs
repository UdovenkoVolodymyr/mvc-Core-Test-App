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
        private IUnitOfWork _unitOfWork;
        private ILogger<HomeController> _logger;

        public PurchaseController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
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
            _unitOfWork.GetRepository<Order>().Create(order);
            return "purchase successful !";
        }
    }
}