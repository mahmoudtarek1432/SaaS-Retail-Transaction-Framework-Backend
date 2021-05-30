using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Data
{
    public interface IMenuItemRepo
    {
        Task<MenuItem> CreateMenuItem(MenuItem Item);
        void UpdateMenuItem(MenuItem Item);
        IEnumerable<MenuItem> GetAllCategoryItem(string CategoryId);
        void DeleteMenuItem(MenuItem MenuItem);
        MenuItem GetItemById(string MenuItemId);
        void SaveChanges();
    }
}
