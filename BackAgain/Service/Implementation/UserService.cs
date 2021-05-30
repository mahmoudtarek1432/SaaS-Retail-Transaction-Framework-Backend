using AutoMapper;
using BackAgain.Data;
using BackAgain.Dto;
using BackAgain.Model;
using BackAgain.Service;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BackAgain.Service
{
    public class UserService: IUserService
    {
        private readonly IUserRepo _UserRepo;
        private readonly IMenuService _menuService;
        private readonly IConfiguration _Configuration;
        private readonly IEmailService _EmailService;
        private readonly IMapper _IMapper;
        private readonly ISubscriptionRepo _SubRepo;

        public UserService(IUserRepo userRepo, ISubscriptionRepo subRepo, IMenuService menuService, IConfiguration config, IEmailService mailservice,IMapper mapper)
        {
            _UserRepo = userRepo;
            _Configuration = config;
            _EmailService = mailservice;
            _IMapper = mapper;
            _SubRepo = subRepo;
            _menuService = menuService;
        }

        public async Task<ClientResponseManager<string>> ChangePassword(string UserId, string OldPassword, string NewPassword)
        {
            var checkPassword = await _UserRepo.CheckPassword(UserId, OldPassword);
            if (checkPassword)
            {
                if(OldPassword != NewPassword)
                {
                    var user = (await _UserRepo.GetUserById(UserId)).ResponseObject;
                    var result = await _UserRepo.changePassword(user, NewPassword);
                    return new ClientResponseManager<string>
                    {
                        IsSuccessfull = true,
                        Message = "password changed successfully"
                    };
                }
                return new ClientResponseManager<string>
                {
                    IsSuccessfull = false,
                    Message = "oldPassword field equals NewPassword Field"
                };
            }
            return new ClientResponseManager<string>
            {
                IsSuccessfull = false,
                Message = "Old Password is incorrect"
            };
        }

        public async Task<string> ConfirmationMailURL(CustomIdentityUser identityUser)
        {
            var confirmationToken = await _UserRepo.GenerateConfirmationToken(identityUser);
            var validationToken = WebEncoders.Base64UrlEncode(System.Text.Encoding.UTF8.GetBytes(confirmationToken));
            string url = $"{_Configuration["BaseUrl"]}/api/WebApp/ConfirmMail/?userid={identityUser.Id}&token={validationToken}";
            return url;
        }
        
        public async Task<ClientResponseManager<string>> ConfirmEmail(string userId, string Token)
        {
            /* var user = await _UserRepo.GetUserByMail(Email);
             if (user.IsSuccessfull)
             {
                 var url = await ConfirmationMailURL(user.ResponseObject);
                 var mailbody = _MailService.ComposeConfirmationMail(Email, url);
                 var result = await _MailService.SendEMailAsync(_Configuration["SMTPEmail:Email"], _Configuration["SMTPEmail:Password"], Email, mailbody, "Confirmation Mail", true);

             }*/

            var user = await _UserRepo.GetUserById(userId);
            if(!user.IsSuccessfull)
            {
                return new ClientResponseManager<string>
                {
                    IsSuccessfull = false,
                    Message = "wrong user id"
                };
            }
            var DecodedToken = WebEncoders.Base64UrlDecode(Token);
            string normalToken = Encoding.UTF8.GetString(DecodedToken);

            var result = await _UserRepo.ConfirmMail(user.ResponseObject, normalToken);
            return result;
        }

        public async Task<ClientResponseManager<UserInfoDto>> GetUserInfo(string UserID)
        {
            var result = await _UserRepo.GetUserById(UserID);
            if (result.IsSuccessfull)
            {
                var user = _IMapper.Map<UserInfoDto>(result.ResponseObject);
                return new ClientResponseManager<UserInfoDto>
                {
                    IsSuccessfull = true,
                    ResponseObject = user
                };
            }

            return new ClientResponseManager<UserInfoDto>
            {
                IsSuccessfull = false,
                Message = "user not found"
            };
        }

        public async Task<ClientResponseManager<string>> LoginClientAsync(string UserEmail, string Password)
        {
            var user = await _UserRepo.GetUserByMail(UserEmail);

            if (!user.IsSuccessfull)
            {
                return new ClientResponseManager<string>
                {
                    IsSuccessfull = false,
                    Message = "Wrong Email"
                };
            }

            var result = await _UserRepo.CheckPassword(user.ResponseObject.Id, Password);

            if (!result)
            {
                return new ClientResponseManager<string>
                {
                    IsSuccessfull = false,
                    Message = "Wrong Password"
                };
            }

            if (!user.ResponseObject.EmailConfirmed)
            {
                return new ClientResponseManager<string>
                {
                    IsSuccessfull = false,
                    Message = "Email Not Confirmed"
                };
            }

            var claims = new[]
            {
                new Claim("Email",UserEmail),
                new Claim(ClaimTypes.NameIdentifier,user.ResponseObject.Id)
            };

            var TokenAsString = JWTService.MakeJwtToken(_Configuration,claims,90);

            return new ClientResponseManager<string>
            {
                IsSuccessfull = true,
                Message = "session token produced successfully",
                ResponseObject = TokenAsString
            };
        }

        public async Task<ClientResponseManager<CustomIdentityUser>> RegisterClientAsync(UserRegisterDto Model)
        {
            if (Model.Password != Model.ConfirmPassword)
            {
                return new ClientResponseManager<CustomIdentityUser>
                {
                    IsSuccessfull = false,
                    Message = "Confirm Password and password field does not match"

                };
            }

            var IdentityUser = new CustomIdentityUser
            {
                Email = Model.Email,
                UserName = Model.Name,
                FirstName = Model.Name,
                PairingId = Guid.NewGuid().ToString()
               // PublicKey = Model.PublicKey
            };

            var result = await _UserRepo.CreateUser(Model);

            

            if (result.IsSuccessfull)
            {
                IdentityUser = result.ResponseObject;
                _menuService.CreateMenu(IdentityUser.Id); // user's menu is instatiated here
                
                var url = await ConfirmationMailURL(IdentityUser);
                var MailBody = _EmailService.ComposeConfirmationMail(IdentityUser.Email, url);
                _EmailService.SendEMailAsync(_Configuration["SMTPEmail:Email"], _Configuration["SMTPEmail:Password"],
                                                               IdentityUser.Email, MailBody, "Email Confimation", true); 
                return result;
            }

            return result;

        }

        public async Task<ClientResponseManager<string>> UpdateUserAsync(string userId, UserInfoDto updateinfo)
        {
            var result = await _UserRepo.GetUserById(userId);

            if (!result.IsSuccessfull)
            {
                return new ClientResponseManager<string>
                {
                    IsSuccessfull = false,
                    Message = "user not found"
                };
            }
            var user = result.ResponseObject;
            if (!string.IsNullOrEmpty(updateinfo.FirstName))
            {
                user.FirstName = updateinfo.FirstName;
            }
            if (!string.IsNullOrEmpty(updateinfo.PairingId))
            {
                user.PairingId = updateinfo.PairingId;
            }
            if (!string.IsNullOrEmpty(updateinfo.PublicKey))
            {
                //user.PublicKey = updateinfo.PublicKey;
            }
            if (!string.IsNullOrEmpty(updateinfo.Role))
            {
                user.Role = updateinfo.Role;
            }
            if ((updateinfo.SubDate) != null)
            {
                user.SubDate = updateinfo.SubDate;
            }
            if (updateinfo.SubType != 0)
            {
                user.SubType = updateinfo.SubType;
            }

            var process = await _UserRepo.UpdateUserData(user);

            return process;
        }
    }
}
