using BackAgain.Data.Inteface;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Data.Implementation
{
    public class MenuRepo : IMenuRepo
    {
        private readonly ProjContext _ctx;

        public MenuRepo(ProjContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Menu> CreateMenu(string userId)
        {
            Menu menu = new Menu { UserId = userId };
            menu.Id = Guid.NewGuid().ToString();
            var result = await _ctx._Menu.AddAsync(menu);
            return result.Entity;
        }

        public Menu GetMenu(string userId)
        {
            return _ctx._Menu.Where(m => m.UserId == userId).FirstOrDefault();
        }

        public void SaveChanges()
        {
            _ctx.SaveChangesAsync();
        }
    }
}
