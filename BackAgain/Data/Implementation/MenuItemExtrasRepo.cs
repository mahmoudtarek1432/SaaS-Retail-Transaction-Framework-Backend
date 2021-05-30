using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackAgain.Model;

namespace BackAgain.Data
{
    public class MenuItemExtrasRepo : IMenuItemExtrasRepo
    {
        private readonly ProjContext _ctx;

        public MenuItemExtrasRepo(ProjContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<ItemExtra> CreateMenuItemExtra(ItemExtra itemExtra)
        {
            itemExtra.Id = Guid.NewGuid().ToString();
            var result = await _ctx._ItemExtra.AddAsync(itemExtra);
            return result.Entity;
        }

        public void DeleteMenuItemExtra(ItemExtra itemExtra)
        {
            _ctx._ItemExtra.Remove(itemExtra);
        }

        public ItemExtra GetItemExtras(string ItemExtraId)
        {
            return _ctx._ItemExtra.Where(ie => ie.Id == ItemExtraId).FirstOrDefault();
        }

        public IEnumerable<ItemExtra> GetAllMenuItemExtras(string MenuItemId)
        {
            return _ctx._ItemExtra.Where(ie => ie.ItemId == MenuItemId).ToList();
        }

        public void UpdateMenuItemExtra(ItemExtra itemExtra)
        {
             _ctx._ItemExtra.Update(itemExtra);
        }

        public void SaveChanges()
        {
            _ctx.SaveChangesAsync();
        }
    }
}
