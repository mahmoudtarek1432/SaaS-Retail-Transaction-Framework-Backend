using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Data.Inteface
{
    public interface IOrderRepo
    {
        public  Task<bool> CreateOrder(Order model);

        Task<bool> CreateOrderItems(OrderItem model);

        Task<bool> CreateOrderExtras(List<OrderItemExtra> model);

        public Task<bool> AddNewItemToOrder(int OrderId, OrderItem item);

        public void DeleteOrder(Order model);

        public Task<IEnumerable<Order>> GetOrderByDate(int UserId, DateTime DateFrom, DateTime DateTo);

        public Order GetOrderById(string OrderId);

        public IEnumerable<Order> GetOrdersByPosSerial(string PosSerial);

        public IEnumerable<Order> getOrdersByUserId(string userId);

        List<Order> GetFullOrder(List<Order> Model);

        public bool  AddOrderState(string OrderId, int state);

         Task SaveChanges();
    }
}
