using BackAgain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Service.Interface
{
    public interface IOrderService
    {
        ClientResponseManager CreateOrder(OrderWriteDto model);
        ClientResponseManager AddToExistingOrder(int OrderId, IEnumerable<OrderWriteDto> NewOrders);
        ClientResponseManager DeleteOrder(int OrderId);
        public ClientResponseManager UpdateOrderStatus(string userId, string OrderId, int status);

    }
}
