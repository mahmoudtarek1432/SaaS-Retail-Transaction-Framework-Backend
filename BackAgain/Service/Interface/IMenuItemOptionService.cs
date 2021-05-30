using BackAgain.Dto;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Service
{
    public interface IMenuItemOptionService
    {
        IEnumerable<ItemOption> GetItemOption(string MenuItemOption);

        IEnumerable<ItemOptionReadDto> GetViewableItemOption(string MenuItemOption);

        Task<ClientResponseManager<ItemOption>> CreateMenuItemOption(string userId, ItemOptionWriteDto MenuItemDto);

        ClientResponseManager UpdateMenuItemOption(ItemOptionWriteDto itemDto);

        //extras and option should be deleted first
        void DeleteItemOptionById(string ItemId);
    }
}
