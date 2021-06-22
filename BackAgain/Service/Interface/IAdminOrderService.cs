using BackAgain.Dto;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Service.Interface
{
    public interface IAdminOrderService
    {
       /* ClientResponseManager CreateOrder(OrderWriteDto model);
        ClientResponseManager AddToExistingOrder(int OrderId);*/

        public ClientResponseManager<IEnumerable<AdminOrderReadDto>> getOrderByDate(string UserId, DateTime DateFrom, DateTime DateTo);

        public ClientResponseManager<IEnumerable<AdminOrderReadDto>> getOrderUpdates(string UserId);

        public ClientResponseManager UpdateOrderStatus(string userId, string OrderId, int status);

        AdminOrderItemReadDto MapOrderItem(OrderItem OrderItem);
        AdminOrderItemExtraReadDto MapOrderItemExtra(OrderItemExtra OrderItemExtra);

        IEnumerable<AdminOrderReadDto> OrderToAdminOrder(List<Order> model);
    }
}
