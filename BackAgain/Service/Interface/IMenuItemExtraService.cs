using BackAgain.Dto;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Service
{
    public interface IMenuItemExtraService
    {
        IEnumerable<ItemExtra> GetItemExtras(string MenuItemExtra);
        IEnumerable<ItemExtraReadDto> GetViewableItemExtras(string MenuItemOptionId);
        Task<ClientResponseManager<ItemExtraReadDto>> CreateMenuItem(string userId, ItemExtrasWriteDto MenuItemDto);

        ClientResponseManager<ItemExtraReadDto> UpdateMenuItem(ItemExtrasWriteDto itemDto);

        //extras and option should be deleted first
        void DeleteItemById(string ItemId);
    }
}
