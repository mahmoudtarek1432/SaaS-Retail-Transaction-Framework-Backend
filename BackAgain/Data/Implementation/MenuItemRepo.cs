using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackAgain.Model;

namespace BackAgain.Data
{
    public class MenuItemRepo : IMenuItemRepo
    {
        private readonly ProjContext _ctx;

        public MenuItemRepo(ProjContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<MenuItem> CreateMenuItem(MenuItem Item)
        {
            Item.Id = Guid.NewGuid().ToString(); 
            var result = await _ctx._MenuItem.AddAsync(Item);
            return result.Entity;
        }

        public void DeleteMenuItem(MenuItem MenuItem)
        {
            _ctx._MenuItem.Remove(MenuItem);
        }

        public IEnumerable<MenuItem> GetAllCategoryItem(string CategoryId)
        {
            return _ctx._MenuItem.Where(MI => MI.CategoryId == CategoryId).ToList();
        }

        public MenuItem GetItemById(string MenuItemId)
        {
            return _ctx._MenuItem.Where(MI => MI.Id == MenuItemId).FirstOrDefault();
        }

        public void UpdateMenuItem(MenuItem Item)
        {
            _ctx._MenuItem.Update(Item);
        }

        public void SaveChanges()
        {
            _ctx.SaveChangesAsync();
        }
    }
}
