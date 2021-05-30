using BackAgain.Dto;
using BackAgain.Service;
using Microsoft.AspNetCore.Http;
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
    public class POSAuthController : ControllerBase
    {
        private readonly IPOSAuthService _posAuthService;

        public POSAuthController(IPOSAuthService AuthService)
        {
            _posAuthService = AuthService;
        }

        [HttpPost("Login")]
        public ActionResult<ClientResponseManager<string>> Login([FromBody] string Serial)
        {
            if (ModelState.IsValid)
            {
                var result = _posAuthService.POSLogin(Serial);
                return result;
            }
            
            return new ClientResponseManager<string>
            {
                IsSuccessfull = false,
                Message = "Token not correct or expired"
            };
        }


        [HttpPost("Logout")]
        public async Task<ActionResult<ClientResponseManager>> Logout()
        {
            var PosSerial= User.FindFirst(ClaimTypes.SerialNumber).Value;
            if(PosSerial != null)
            {
                var result = _posAuthService.POSLogout(PosSerial);
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
