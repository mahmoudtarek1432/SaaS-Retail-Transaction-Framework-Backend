using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackAgain.Dto;
using BackAgain.Model;

namespace BackAgain.Profiles
{
    public class OrderProfile : AutoMapper.Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, AdminOrderReadDto>();
            CreateMap<OrderItem, AdminOrderItemReadDto>();
            CreateMap<OrderItemExtra, AdminOrderItemExtraReadDto>();
            CreateMap<OrderStatus, OrderStateReadDto>();
            CreateMap<OrderWriteDto, Order>();
            CreateMap<OrderItemWriteDto, OrderItem>();
            CreateMap<OrderItemExtraWriteDto, OrderItemExtra>();
            
        }
    }
}
