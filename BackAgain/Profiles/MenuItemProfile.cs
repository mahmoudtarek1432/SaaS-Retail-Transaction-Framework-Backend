using BackAgain.Dto;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Profiles
{
    public class MenuItemProfile:AutoMapper.Profile
    {
        public MenuItemProfile()
        {
            CreateMap<MenuItemWriteDto, MenuItem>();
            CreateMap<MenuItem, MenuItemReadDto>();

            CreateMap<ItemOptionWriteDto, ItemOption>();
            CreateMap<ItemOption, ItemOptionReadDto>();

            CreateMap<ItemExtrasWriteDto, ItemExtra>();
            CreateMap<ItemExtra, ItemExtraReadDto>();
        }
    }
}
