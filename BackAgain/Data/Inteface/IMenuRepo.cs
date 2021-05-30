using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Data.Inteface
{
    public interface IMenuRepo
    {
        Task<Menu> CreateMenu(string userId);
        Menu GetMenu(string userId);
        void SaveChanges();
    }
}
