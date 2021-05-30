using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Data
{
    public interface IMenuItemExtrasRepo
    {
        Task<ItemExtra> CreateMenuItemExtra(ItemExtra itemExtra);
        void UpdateMenuItemExtra(ItemExtra itemExtra);
        IEnumerable<ItemExtra> GetAllMenuItemExtras(string MenuItemId);
        void DeleteMenuItemExtra(ItemExtra itemExtra);
        ItemExtra GetItemExtras(string ItemExtraId);
        void SaveChanges();
    }
}
