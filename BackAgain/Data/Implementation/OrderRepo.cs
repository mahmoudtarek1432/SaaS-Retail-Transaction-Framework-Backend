using BackAgain.Data.Inteface;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Data.Implementation
{
    public class OrderRepo : IOrderRepo
    {
        private readonly ProjContext _ctx;

        public OrderRepo(ProjContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<bool> CreateOrder(Order model)
        {
            try
            {
                model.Id = Guid.NewGuid().ToString();
                var order = (await _ctx._Order.AddAsync(model)).Entity;
                order.OrderItem.ForEach(async OI =>
                {
                    OI.OrderId = order.Id;
                    await CreateOrderItems(OI);
                });
                await _ctx._Orderstatus.AddAsync(order.OrderStatus[0]);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CreateOrderItems(OrderItem model)
        {
            try
            {
                model.Id = Guid.NewGuid().ToString();
                var item = (await _ctx._OrderItem.AddAsync(model)).Entity;
                item.OrderExtras.ForEach(I =>
                {
                    I.OrderItemId = item.Id;

                });
                await CreateOrderExtras(item.OrderExtras);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CreateOrderExtras(List<OrderItemExtra> model)
        {
            try
            {
                model.ForEach(m => m.Id = Guid.NewGuid().ToString());
                await _ctx._OrderItemExtras.AddRangeAsync(model);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<bool> AddNewItemToOrder(int OrderId, OrderItem item)
        {
            try
            {
                await CreateOrderItems(item);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void DeleteOrder(Order model)
        {
            _ctx._Order.Remove(model);
            _ctx._OrderItem.RemoveRange(model.OrderItem);
            model.OrderItem.ForEach(OI => _ctx._OrderItemExtras.RemoveRange(OI.OrderExtras));

        }

        public Task<IEnumerable<Order>> GetOrderByDate(int UserId, DateTime DateFrom, DateTime DateTo)
        {
            throw new AccessViolationException();
        }

        public Order GetOrderById(string OrderId)
        {
            var order = _ctx._Order.Where(O => O.Id == OrderId).ToList();
 
            return GetFullOrder(order).FirstOrDefault();
        }

        public IEnumerable<Order> GetOrdersByPosSerial(string PosSerial)
        {
            var order = _ctx._Order.Where(O => O.POSSerial == PosSerial)
                                   .ToList();

            order = GetFullOrder(order);
            return order;
        }

        public IEnumerable<Order> getOrdersByUserId(string userId)
        {
            var order = _ctx._Order.Where(O => O.UserId == userId).ToList();

            return GetFullOrder(order);
        }

        public List<Order> GetFullOrder(List<Order> Model)
        {
            Model.ForEach(O => {
                O.OrderItem = _ctx._OrderItem.Where(OI => OI.ItemId == O.Id).ToList();
                O.OrderItem.ForEach(OI =>
                {
                    OI.OrderExtras = _ctx._OrderItemExtras.Where(OE => OE.OrderItemId == O.Id).ToList();
                });
            });
            return Model;
        }

        public async Task SaveChanges()
        {
            await _ctx.SaveChangesAsync();
        }

        public bool AddOrderState(string OrderId, int state)
        {
            var OrderState = new OrderStatus
            {
                OrderId = OrderId,
                State = state,
                Date = DateTime.Now
            };
            try
            {
                _ctx._Orderstatus.Add(OrderState);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
