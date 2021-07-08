using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackAgain.Model;

namespace BackAgain.Data
{
    public class MenuItemOptionRepo : IMenuItemOptions
    {
        private readonly ProjContext _ctx;

        public MenuItemOptionRepo(ProjContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<ItemOption> CreateMenuItemOption(ItemOption itemOption)
        {
            itemOption.Id = Guid.NewGuid().ToString();
            var result = await _ctx._ItemOption.AddAsync(itemOption);
            return result.Entity;
        }

        public void DeleteMenuItemOption(ItemOption itemOption)
        {
            _ctx._ItemOption.Remove(itemOption);
        }

        public IEnumerable<ItemOption> GetAllMenuItemOption(string MenuItemId)
        {
            return _ctx._ItemOption.Where(IO => IO.ItemId == MenuItemId).ToList();
        }

        public ItemOption GetItemOptions(string ItemOptionId)
        {
            return _ctx._ItemOption.Where(IO => IO.Id == ItemOptionId).FirstOrDefault();
        }

        public void UpdateMenuItemOption(ItemOption itemOption)
        {
            _ctx._ItemOption.Update(itemOption);
        }

        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }
    }
}
