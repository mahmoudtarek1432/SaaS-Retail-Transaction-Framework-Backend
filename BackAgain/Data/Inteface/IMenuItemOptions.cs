using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Data
{
    public interface IMenuItemOptions
    {
        Task<ItemOption> CreateMenuItemOption(ItemOption itemOption);
        void UpdateMenuItemOption(ItemOption itemOption);
        IEnumerable<ItemOption> GetAllMenuItemOption(string MenuItemId);
        void DeleteMenuItemOption(ItemOption itemOption);
        ItemOption GetItemOptions(string ItemOptionId);
        void SaveChanges();
    }
}
