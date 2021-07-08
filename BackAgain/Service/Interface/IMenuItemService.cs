using BackAgain.Dto;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Service
{
    public interface IMenuItemService
    {
        IEnumerable<MenuItem> GetItemsbyCategory(string categoryId);

        IEnumerable<MenuItemReadDto> GetViewableMenuItem(string categoryId);

        Task<ClientResponseManager<MenuItemReadDto>> CreateMenuItem(string userId, MenuItemWriteDto MenuItemDto);

        ClientResponseManager<MenuItemReadDto> UpdateMenuItem(string userId, MenuItemWriteDto itemDto);

        //extras and option should be deleted first
        void DeleteItemById(string ItemId);
    }
}
