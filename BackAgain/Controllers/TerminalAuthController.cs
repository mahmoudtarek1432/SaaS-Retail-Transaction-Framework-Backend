using BackAgain.Dto;
using BackAgain.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackAgain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerminalAuthController : ControllerBase
    {
        private readonly ITerminalAuthService _TerminalAuthService;

        public TerminalAuthController(ITerminalAuthService terAuthService)
        {
            _TerminalAuthService = terAuthService;
        }

        [HttpPost("Login/{Serial}")]
        public ActionResult<ClientResponseManager<string>> Login(string Serial)
        {
            if (ModelState.IsValid)
            {
                var result = _TerminalAuthService.TerminalLogin(Serial);
                return result;
            }

            return new ClientResponseManager<string>
            {
                IsSuccessfull = false,
                Message = "Token not correct or expired"
            };
        }


        [HttpPost("Logout")]
        public ActionResult<ClientResponseManager> Logout()
        {
            var TerminalSerial = User.FindFirst(ClaimTypes.SerialNumber).Value;
            if (TerminalSerial != null)
            {
                var result = _TerminalAuthService.TerminalLogout(TerminalSerial);
                return result;

                //send websocket message to terminals that pos is closed
            }
            return new ClientResponseManager
            {
                IsSuccessfull = false,
                Message = "Token not correct or expired"
            };
        }

        [HttpGet("CheckAuth")]
        public ActionResult<ClientResponseManager<string>> TerminalCheckAuth()
        {

            if (User != null)
            {
                var test = HttpContext.Request.Headers;
                var user = User.FindFirst(ClaimTypes.SerialNumber);
                if (user != null)
                {
                    return new ClientResponseManager<string>
                    {
                        IsSuccessfull = true,
                        Message = "Token valid"
                    };
                }
            }
            return new ClientResponseManager<string>
            {
                IsSuccessfull = false,
                Message = "user not logged in"
            };
        }

    }
}
