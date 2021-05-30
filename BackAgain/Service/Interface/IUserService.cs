using BackAgain.Dto;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Service
{
    public interface IUserService
    {
        Task<ClientResponseManager<CustomIdentityUser>> RegisterClientAsync(UserRegisterDto Model);
        Task<ClientResponseManager<string>> LoginClientAsync(string UserEmail, string Password);
        Task<ClientResponseManager<UserInfoDto>> GetUserInfo(string UserID);
        Task<ClientResponseManager<string>> ConfirmEmail(string UserId, string Token);
        Task<ClientResponseManager<string>> UpdateUserAsync(string userId, UserInfoDto updateinfo);
        Task<ClientResponseManager<string>> ChangePassword(string UserId, string OldPassword, string NewPassword);
    }
}
