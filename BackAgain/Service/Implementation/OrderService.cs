using AutoMapper;
using BackAgain.Data.Inteface;
using BackAgain.Dto;
using BackAgain.Model;
using BackAgain.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _OrderRepo;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepo orderRepo, IMapper mapper)
        {
            _OrderRepo = orderRepo;
            _mapper = mapper;
        }

        public void AddCommentToOrder(OrderCommentWriteDto model)
        {
            var OC = new OrderComment
            {
                Comment = model.Comment,
                Date = DateTime.Now,
                OrderId = model.OrderId
            };
            _OrderRepo.CreateOrderComment(OC);
            _OrderRepo.SaveChanges();
        }

        public async Task<ClientResponseManager> AddToExistingOrder(string userId, string OrderId, IEnumerable<OrderItemWriteDto> NewOrders)
        {
            var Order = _OrderRepo.GetOrderById(OrderId);
            if(Order != null)
            {
                if(Order.UserId == userId)
                {
                   

                        var MappedOrderItems = _mapper.Map<IEnumerable<OrderItem>>(NewOrders);
                        await _OrderRepo.AddNewItemToOrder(OrderId, MappedOrderItems);
                        await _OrderRepo.SaveChanges();

                    return new ClientResponseManager
                    {
                        IsSuccessfull = true,
                        Message = "Items Added",
                    };
                }
                return new ClientResponseManager
                {
                    IsSuccessfull = false,
                    Message = "Item does not belong to user"
                };
            }
            return new ClientResponseManager
            {
                IsSuccessfull = false,
                Message = "Item not found"
            };
        }

        public async Task<ClientResponseManager<Order>> CreateOrder(OrderWriteDto model)
        {
            var Order = _mapper.Map<Order>(model);
            var result = await _OrderRepo.CreateOrder(Order);
            if (result != null)
            {
                await _OrderRepo.SaveChanges();
                return new ClientResponseManager<Order>
                {
                    IsSuccessfull = true,
                    Message = "Order created successfully",
                    ResponseObject = Order
                };
            }
            return new ClientResponseManager<Order>
            {
                IsSuccessfull = false,
                Message = "Process not successful",
                ResponseObject = Order
            };
        }

        public ClientResponseManager DeleteOrder(string userId, string OrderId)
        {
            var order = _OrderRepo.GetOrderById(OrderId);
            if (order != null)
            {
                if (order.UserId == userId)
                {

                    try
                    {
                        _OrderRepo.DeleteOrder(order);
                        _OrderRepo.SaveChanges();
                        return new ClientResponseManager
                        {
                            IsSuccessfull = true,
                            Message = "Order Deleted"
                        };
                    }
                    catch(Exception e)
                    {
                        return new ClientResponseManager
                        {
                            IsSuccessfull = false,
                            Message = "Order not deleted"
                        };
                    }
                }
                return new ClientResponseManager
                {
                    IsSuccessfull = false,
                    Message = "Order Does not belong to user"
                };
            }
            return new ClientResponseManager
            {
                IsSuccessfull = false,
                Message = "Order not found"
            };
        }

        public Order GetOrderByid(string OrderId)
        {
            return _OrderRepo.GetOrderById(OrderId);
        }

        public async Task<ClientResponseManager> UpdateOrderStatus(string UserId, string OrderId, int status)
        {
            var order = _OrderRepo.GetOrderById(OrderId);
            if (order.UserId == UserId)
            {
                bool result = _OrderRepo.AddOrderState(OrderId, status);
                if (result)
                {
                    try
                    {
                        await _OrderRepo.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return new ClientResponseManager
                        {
                            IsSuccessfull = true,
                            Message = $"Database Update not Successful - {e.Message}"
                        };
                    }
                    return new ClientResponseManager
                    {
                        IsSuccessfull = true,
                        Message = "status add"
                    };
                }
                return new ClientResponseManager
                {
                    IsSuccessfull = true,
                    Message = "Database Update not Successful"
                };
            }
            return new ClientResponseManager
            {
                IsSuccessfull = false,
                Message = "Order Does Not Belong to user"
            };
        }
    }
}
