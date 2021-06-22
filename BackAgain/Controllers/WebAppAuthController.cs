using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackAgain.Data.Inteface;
using BackAgain.Dto;
using BackAgain.Model;
using BackAgain.Service;
using BackAgain.Service.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BackAgain.Controllers
{
    [Route("api/WebApp/")]
    [ApiController]
    public class WebAppAuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IVerisonUpdateRepo _VerRepo;

        public WebAppAuthController(IUserService userservice, IConfiguration config, IVerisonUpdateRepo verRepo)
        {
            _userService = userservice;
            _VerRepo = verRepo;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ClientResponseManager<CustomIdentityUser>>> Register([FromBody] UserRegisterDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterClientAsync(model);

                if (result.IsSuccessfull)
                {
                    var version = new VersionUpdateLog
                    {
                        MenuVersion = 0,
                        SettingsVersion = 0,
                        UserId = result.ResponseObject.Id,
                        UpdateIn = 1
                    };
                    await _VerRepo.CreateVersion(version);
                    _VerRepo.SaveChanges();
                }

                return result;
            }

            return new ClientResponseManager<CustomIdentityUser>
            {
                IsSuccessfull = false,
                Message = "model is not correct"
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<ClientResponseManager<string>>> Login([FromBody] UserLoginDto model)
        {
            if (ModelState.IsValid)
            {
                User.FindFirst(ClaimTypes.NameIdentifier);
                var result = await _userService.LoginClientAsync(model.Email, model.Password);
                if (result.IsSuccessfull)
                {
                    Response.Cookies.Append("AccessToken", result.ResponseObject, new CookieOptions { Expires = DateTime.Now.AddDays(30), HttpOnly = true, SameSite = SameSiteMode.None });
                }

                return result;
            }
            return new ClientResponseManager<string>
            {
                IsSuccessfull = false,
                Message = "model is not valid"
            };
        }

        [HttpGet("logout")]
        public async Task<ActionResult<ClientResponseManager<string>>> Logout()
        {
            if(User != null)
            {
                Response.Cookies.Append("AccessToken", "", new CookieOptions { Expires = DateTime.Now});
                return new ClientResponseManager<string>
                {
                    IsSuccessfull = true,
                    Message = "Logout Successfully"
                };
            }
            return new ClientResponseManager<string>
            {
                IsSuccessfull = false,
                Message = "user not logged in"
            };
        }

        [HttpGet("ConfirmMail")]
        public async Task<ActionResult<ClientResponseManager<string>>> ConfirmMail(string userId, string token)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ConfirmEmail(userId, token);

                return result;
            }
            return new ClientResponseManager<string>
            {
                IsSuccessfull = false,
                Message = "model is not valid"
            };
        }

        [HttpPost("ChangePassword")]
        public async Task<ActionResult<ClientResponseManager<string>>> ChangePassword(ChangePasswordDto changePassword)
        {
            if (ModelState.IsValid)
            {
                if(User != null)
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var result = await _userService.ChangePassword(userId,changePassword.OldPassword,changePassword.NewPassword);

                    return result;
                }
                return new ClientResponseManager<string>
                {
                    IsSuccessfull = false,
                    Message = "User Not LoggedIn"
                };
            }
            return new ClientResponseManager<string>
            {
                IsSuccessfull = false,
                Message = "model is not valid"
            };
        }

        [HttpPatch("UpdateInfo")]
        public async Task<ActionResult<ClientResponseManager<string>>> editUser([FromBody]UserInfoDto model)
        {
            if (ModelState.IsValid)
            {
                if (User != null)
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var result = await _userService.UpdateUserAsync(userId, model);

                    return result;
                }
                return new ClientResponseManager<string>
                {
                    IsSuccessfull = false,
                    Message = "User Not LoggedIn"
                };
            }
            return new ClientResponseManager<string>
            {
                IsSuccessfull = false,
                Message = "model is not valid"
            };
        }
    }
}