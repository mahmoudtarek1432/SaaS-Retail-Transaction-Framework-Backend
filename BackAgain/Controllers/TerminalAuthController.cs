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
    public class TerminalAuthController : ControllerBase
    {
        private readonly ITerminalAuthService _TerminalAuthService;

        public TerminalAuthController(ITerminalAuthService terAuthService)
        {
            _TerminalAuthService = terAuthService;
        }

        [HttpPost("Login")]
        public ActionResult<ClientResponseManager<string>> Login([FromBody] string Serial)
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
    }
}
