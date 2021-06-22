using AutoMapper;
using BackAgain.Data;
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
    public class AdminOrderService : IAdminOrderService
    {
        private readonly IOrderRepo _OrderRepo;
        private readonly IMenuItemRepo _ItemRepo;
        private readonly IMenuItemExtrasRepo _ItemExtraRepo;
        private readonly IMenuItemOptions _ItemOptionRepo;
        private readonly IMapper _mapper;

        public AdminOrderService(IOrderRepo OrderRepo, IMenuItemRepo ItemRepo, IMenuItemExtrasRepo ItemExtra, IMenuItemOptions ItemOptions, IMapper mapper)
        {
            _OrderRepo = OrderRepo;
            _ItemRepo = ItemRepo;
            _ItemExtraRepo = ItemExtra;
            _ItemOptionRepo = ItemOptions;
            _mapper = mapper;
        }

        public ClientResponseManager<IEnumerable<AdminOrderReadDto>> getOrderByDate(string UserId, DateTime DateFrom, DateTime DateTo)
        {
             var Orders = _OrderRepo.getOrdersByUserId(UserId);
             if(Orders != null)
             {
                 var LimitedByDate = Orders.Where(O => O.Date.CompareTo(DateFrom) >= 0 && O.Date.CompareTo(DateTo) <= 0).ToList();
                 if(LimitedByDate != null)
                 {
                    var AdminOrders = OrderToAdminOrder(LimitedByDate);
                    return new ClientResponseManager<IEnumerable<AdminOrderReadDto>>{
                         IsSuccessfull = true,
                         ResponseObject = AdminOrders
                     };
                 }
               
            }
            return new ClientResponseManager<IEnumerable<AdminOrderReadDto>>
            {
                IsSuccessfull = false,
                Message = "No Orders were found"
            };
        }

        public ClientResponseManager<IEnumerable<AdminOrderReadDto>> getOrderUpdates(string UserId)
        {
            var Orders = _OrderRepo.getOrdersByUserId(UserId);
            if(Orders != null)
            {
                var FilteredOrders = Orders.Where(O => O.OrderStatus.LastOrDefault().State == 1 || O.OrderStatus.LastOrDefault().State == 2);
                if(FilteredOrders != null)
                {
                    var AdminOrders = OrderToAdminOrder(FilteredOrders.ToList());
                    return new ClientResponseManager<IEnumerable<AdminOrderReadDto>>
                    {
                        IsSuccessfull = true,
                        ResponseObject = AdminOrders
                    };
                }
                return new ClientResponseManager<IEnumerable<AdminOrderReadDto>>
                {
                    IsSuccessfull = false,
                    Message = "No Updated Orders Were found"
                };
            }
            return new ClientResponseManager<IEnumerable<AdminOrderReadDto>>
            {
                IsSuccessfull = false,
                Message = "No Orders Were found"
            };


        }

        public ClientResponseManager UpdateOrderStatus(string UserId, string OrderId, int status)
        {
            var order = _OrderRepo.GetOrderById(OrderId);
            if(order.UserId == UserId)
            {
                bool result = _OrderRepo.AddOrderState(OrderId, status);
                if (result)
                {
                    try
                    {
                        _OrderRepo.SaveChanges();
                    }
                    catch( Exception e){
                        return new ClientResponseManager
                        {
                            IsSuccessfull = true,
                            Message = "Database Update not Successful"
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

        public AdminOrderItemReadDto MapOrderItem(OrderItem OrderItem)
        {
            var MenuItem = _ItemRepo.GetItemById(OrderItem.ItemId);
            ItemOption ItemOption = _ItemOptionRepo.GetItemOptions(OrderItem.ItemOptionId);
            var OrderItemDTO = new AdminOrderItemReadDto
            {
                Name = MenuItem.Name,
                Price = MenuItem.Price,
                Description = MenuItem.Description,
                Image = MenuItem.Image,
                ItemId = MenuItem.Id,
                Quantity = OrderItem.Quantity,
                ItemCode = OrderItem.ItemCode
            };
            if (OrderItem.ItemOptionId != null)
            {
                OrderItemDTO.Name = ItemOption.Name;
                OrderItemDTO.Price = ItemOption.Price;

            }
            return OrderItemDTO;
        }
        public AdminOrderItemExtraReadDto MapOrderItemExtra(OrderItemExtra OrderItemExtra)
        {
            var MenuItemExtra = _ItemExtraRepo.GetItemExtras(OrderItemExtra.ItemExtraId);
            var OrderItemDTO = new AdminOrderItemExtraReadDto
            {
                Name = MenuItemExtra.Name,
                Price = MenuItemExtra.Price,
                Code = MenuItemExtra.Code,
                Image = MenuItemExtra.Image,
                ItemExtraId = MenuItemExtra.Id,
                OrderExtraId = OrderItemExtra.Id
            };
            return OrderItemDTO;
        }

        public IEnumerable<AdminOrderReadDto> OrderToAdminOrder (List<Order> model)
        {
            var AdminOrders = Mapper.Map<IEnumerable<AdminOrderReadDto>>(model).ToList();
            for (int i = 0; i < model.Count; i++)
            {
                AdminOrders[i].OrderStatus = _mapper.Map<List<OrderStateReadDto>>(model[i].OrderStatus);
                AdminOrders[i].OrderItem = model[i].OrderItem.Select(MapOrderItem).ToList();
                for (int j = 0; j < model[i].OrderItem.Count; j++)
                {
                    AdminOrders[i].OrderItem[j].OrderExtras = model[i].OrderItem[j].OrderExtras.Select(MapOrderItemExtra).ToList();
                }
            }
            return AdminOrders;
        }
    }
}
