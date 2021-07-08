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
        public async Task<Order> CreateOrder(Order model)
        {
            try
            {
                model.OrderItem.ForEach(oi => oi.ItemOptionId = (oi.ItemOptionId == "" || oi.ItemOptionId == null) ? null : oi.ItemOptionId);
                model.Id = Guid.NewGuid().ToString();
                var order = (await _ctx._Order.AddAsync(model)).Entity;
                order.OrderItem.ForEach(async OI =>
                {
                    OI.ItemCode = " ";
                    OI.OrderId = order.Id;
                    await CreateOrderItems(OI);
                });
                var state = new OrderStatus
                {
                    OrderId = model.Id,
                    State = 1,
                    Date = DateTime.Now
                };
                await _ctx._Orderstatus.AddAsync(state);
                return order;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> CreateOrderItems(OrderItem model)
        {
            try
            {
                model.Id = Guid.NewGuid().ToString();
                model.ItemCode = " ";
                var item = (await _ctx._OrderItem.AddAsync(model)).Entity;
                
                item.OrderExtras.ForEach(I =>
                {
                    I.OrderItemId = item.Id;

                });
                var orderExtra = await CreateOrderExtras(model.OrderExtras);
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

        public bool CreateOrderComment(OrderComment orderComment)
        {
            try
            {
                _ctx._orderComments.Add(orderComment);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<bool> AddNewItemToOrder(string OrderId, IEnumerable<OrderItem> item)
        {
            try
            {
                foreach(var I in item)
                {
                    await CreateOrderItems(I);
                }
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
                O.OrderStatus = _ctx._Orderstatus.Where(S => S.OrderId == O.Id).ToList();
                O.OrderItem = _ctx._OrderItem.Where(OI => OI.OrderId == O.Id).ToList();
                O.OrderComment = _ctx._orderComments.Where(C => C.OrderId == O.Id).ToList();
                O.OrderItem.ForEach(OI =>
                {
                    OI.OrderExtras = _ctx._OrderItemExtras.Where(OE => OE.OrderItemId == OI.Id).ToList();
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
