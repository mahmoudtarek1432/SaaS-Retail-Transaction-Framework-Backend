using AutoMapper;
using BackAgain.Dto;
using BackAgain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Data
{
    public class UserRepo : IUserRepo
    {

        public readonly UserManager<CustomIdentityUser> _UserManager;

        public UserRepo(UserManager<CustomIdentityUser> userManager)
        {
            _UserManager = userManager;
        }

        public async Task<bool> CheckPassword(string UserId, string Password)
        {
            var user = await GetUserById(UserId);
            var result = await _UserManager.CheckPasswordAsync(user.ResponseObject, Password);

            return result;
        }



        public async Task<ClientResponseManager<CustomIdentityUser>> CreateUser(UserRegisterDto model)
        {
            /*if(model.Password != model.ConfirmPassword)
            {
                return new ClientResponseManager<string>
                {
                    IsSuccessfull = false,
                    Message = "Confirm Password and password field does not match"

                };
            }*/

            var IdentityUser = new CustomIdentityUser
            {
                Email = model.Email,
                UserName = model.Name,
                FirstName = model.Name,
                //PublicKey = model.PublicKey
            };

            var email = await _UserManager.FindByEmailAsync(IdentityUser.Email);

            if (email != null)
            {
                return new ClientResponseManager<CustomIdentityUser>
                {
                    IsSuccessfull = false,
                    Message = "Email Dupplicated",
                    
                };
            }

            var result = await _UserManager.CreateAsync(IdentityUser, model.Password);

            if (result.Succeeded)
            {
                var user = await _UserManager.FindByEmailAsync(IdentityUser.Email);
                return new ClientResponseManager<CustomIdentityUser>
                {
                    IsSuccessfull = true,
                    Message = "User Registered to database successfully",
                    ResponseObject = user
                };
            }

            return new ClientResponseManager<CustomIdentityUser>
            {
                IsSuccessfull = false,
                Message = "User didn't get created",
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<ClientResponseManager<CustomIdentityUser>> GetUserById(string UserId)
        {
            var user = await _UserManager.FindByIdAsync(UserId);

            if (user == null)
            {
                return new ClientResponseManager<CustomIdentityUser>
                {
                    IsSuccessfull = false,
                    Message = "User Not Found"
                };
            }

            return new ClientResponseManager<CustomIdentityUser>
            {
                IsSuccessfull = true,
                Message = "process successfull",
                ResponseObject = user
            };
        }

        public async Task<ClientResponseManager<CustomIdentityUser>> GetUserByMail(string Email)
        {
            Console.WriteLine(Email);
            var user = await _UserManager.FindByEmailAsync(Email);

            if (user == null)
            {
                return new ClientResponseManager<CustomIdentityUser>
                {
                    IsSuccessfull = false,
                    Message = "User Not Found"
                };
            }

            return new ClientResponseManager<CustomIdentityUser>
            {
                IsSuccessfull = true,
                Message = "process successfull",
                ResponseObject = user
            };
        }

        public async Task<ClientResponseManager<string>> UpdateUserData(CustomIdentityUser user)
        {
            var result = await _UserManager.UpdateAsync(user);
            
            if (!result.Succeeded)
            {
                return new ClientResponseManager<string>
                {
                    IsSuccessfull = false,
                    Message = "process not successfull",
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            return new ClientResponseManager<string>
            {
                IsSuccessfull = true,
                Message = "process successfull",
            };
        }

        public async Task<string> GenerateConfirmationToken (CustomIdentityUser identityUser){
            var confirmationToken = await _UserManager.GenerateEmailConfirmationTokenAsync(identityUser);
            return confirmationToken;
        }

        public async Task<bool> changePassword(CustomIdentityUser identityUser, string newPassword)
        {
            var result = await _UserManager.CheckPasswordAsync(identityUser, newPassword);
            return result;
        }

        public async Task<ClientResponseManager<string>> ConfirmMail(CustomIdentityUser identityUser, string Token)
        {
            var result = await _UserManager.ConfirmEmailAsync(identityUser, Token);
            if (!result.Succeeded)
            {
                
                return new ClientResponseManager<string>
                {
                    IsSuccessfull = false,
                    Message = "email not confirmed",
                };
            }
            return new ClientResponseManager<string>
            {
                IsSuccessfull = true,
                Message = "email confirmed successfully",
            };
        }
    }
}
