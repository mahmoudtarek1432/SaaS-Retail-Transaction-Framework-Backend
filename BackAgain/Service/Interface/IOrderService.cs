using BackAgain.Dto;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Service.Interface
{
    public interface IOrderService
    {
        Task<ClientResponseManager> AddToExistingOrder(string userId, string OrderId, IEnumerable<OrderItemWriteDto> NewOrders);

        Task<ClientResponseManager<Order>> CreateOrder(OrderWriteDto model);

        void AddCommentToOrder(OrderCommentWriteDto comment);

        ClientResponseManager DeleteOrder(string userId, string OrderId);

        Task<ClientResponseManager> UpdateOrderStatus(string UserId, string OrderId, int status);

        Order GetOrderByid(string OrderId);
    }
}
