using BackAgain.Dto;
using BackAgain.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackAgain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderViewController : ControllerBase
    {
        private readonly IAdminOrderService _AdminService;

        public OrderViewController(IAdminOrderService AdminService)
        {
            _AdminService = AdminService;
        }

        [HttpGet("{DateFrom}/{DateTo}")]
        public ClientResponseManager<IEnumerable<AdminOrderReadDto>> getOrdersByDate(DateTime DateFrom, DateTime DateTo)
        {
            if (ModelState.IsValid)
            {
                var UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var result = _AdminService.getOrderByDate(UserId, DateFrom, DateTo);
                return result;
            }
            return new ClientResponseManager<IEnumerable<AdminOrderReadDto>>
            {
                IsSuccessfull = false,
                Message = "Model is not correct"
            };
        }

        [HttpGet("Updates")]
        public ClientResponseManager<IEnumerable<AdminOrderReadDto>> GetOrderUpdates()
        {
            if (ModelState.IsValid)
            {
                var UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var result = _AdminService.getOrderUpdates(UserId);
                return result;
            }
            return new ClientResponseManager<IEnumerable<AdminOrderReadDto>>
            {
                IsSuccessfull = false,
                Message = "Model is not correct"
            };
        }

        [HttpPost("OrderState/{OrderId}/{status}")]
        public ClientResponseManager GetOrderUpdates(string OrderId, int status)
        {
            if (ModelState.IsValid)
            {
                var UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var result = _AdminService.UpdateOrderStatus(UserId, OrderId,status);
                return result;
            }
            return new ClientResponseManager
            {
                IsSuccessfull = false,
                Message = "Model is not correct"
            };
        }
    }
}
