using BackAgain.Dto;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Data
{
    public interface IUserRepo
    {
        Task<ClientResponseManager<CustomIdentityUser>> CreateUser(UserRegisterDto model);
        Task<ClientResponseManager<CustomIdentityUser>> GetUserById (string UserId);
        Task<ClientResponseManager<CustomIdentityUser>> GetUserByMail (string Email);
        Task<ClientResponseManager<string>> UpdateUserData(CustomIdentityUser user);
        Task<string> GenerateConfirmationToken(CustomIdentityUser identityUser);
        Task<bool> changePassword(CustomIdentityUser identityUser, string newPassword);
        Task<ClientResponseManager<string>> ConfirmMail(CustomIdentityUser identityUser, string Token);
        Task<bool> CheckPassword(string UserId, string Password);
    }
}
