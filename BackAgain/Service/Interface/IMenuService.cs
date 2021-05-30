using BackAgain.Dto;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Service
{
    public interface IMenuService
    {
        Menu getMenuByUserId(string userid);

        MenuReadDto getViewableMenuById(string userid);

        void CreateMenu(string UserId);

        ClientResponseManager<MenuReadDto> GetFullMenu(string userId);
    }
}
