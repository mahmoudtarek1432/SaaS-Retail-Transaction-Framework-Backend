using BackAgain.Data;
using BackAgain.Dto;
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
    public class WebSocketController : ControllerBase
    {
        private readonly ITerminalRepo _TerminalRepo;
        private readonly IPOSRepo _POSRepo;
        private readonly IUserRepo _UserRepo;

        public WebSocketController(ITerminalRepo terminalrepo, IPOSRepo POSRepo, IUserRepo userRepo)
        {
            _TerminalRepo = terminalrepo;
            _POSRepo = POSRepo;
            _UserRepo = userRepo;
        }

        [HttpPost("Terminal/{ConnId}")]
        public ActionResult<ClientResponseManager> AddTerminalSocket( string ConnId)
        {
            if (ModelState.IsValid)
            {
                var userSerial = User.FindFirst(ClaimTypes.SerialNumber); // has the pos or terminal serial
                if(userSerial != null)
                {
                    _TerminalRepo.updateTerminalConnId(userSerial.Value, ConnId);
                    _TerminalRepo.SaveChanges();

                    return new ClientResponseManager
                    {
                        IsSuccessfull = true,
                        Message = "The Terminal socket has been registered"
                    };
                }
                return BadRequest(new ClientResponseManager
                {
                    IsSuccessfull = true,
                    Message = "user login is not correct"
                });
            }
            return new ClientResponseManager
            {
                IsSuccessfull = true,
                Message = "model is not valid"
            };
        }

        [HttpPost("Pos")]
        public ActionResult<ClientResponseManager> AddPosSocket([FromBody] string ConnId)
        {
            if (ModelState.IsValid)
            {
                var userSerial = User.FindFirst(ClaimTypes.SerialNumber); // has the pos or terminal serial
                if (userSerial != null)
                {
                    _POSRepo.updatePOSConnId(userSerial.Value, ConnId);
                    _POSRepo.SaveChanges();

                    return new ClientResponseManager
                    {
                        IsSuccessfull = true,
                        Message = "The POS socket has been registered"
                    };
                }
                return BadRequest(new ClientResponseManager
                {
                    IsSuccessfull = true,
                    Message = "user login is not correct"
                });
            }
            return new ClientResponseManager
            {
                IsSuccessfull = true,
                Message = "model is not valid"
            };
        }

        [HttpPost("WebApp/{ConnId}")]
        public async Task<ActionResult<ClientResponseManager>> AddWebAppSocket ( string ConnId)
        {
            if (ModelState.IsValid)
            {
                var UserId = User.FindFirst(ClaimTypes.NameIdentifier); // has the pos or terminal serial
                if (UserId != null)
                {
                    await _UserRepo.UpdateWebAppConnId(UserId.Value, ConnId);

                    return new ClientResponseManager
                    {
                        IsSuccessfull = true,
                        Message = "The WebApp socket has been registered"
                    };
                }
                return BadRequest(new ClientResponseManager
                {
                    IsSuccessfull = true,
                    Message = "user login is not correct"
                });
            }
            return new ClientResponseManager
            {
                IsSuccessfull = true,
                Message = "model is not valid"
            };
        }
    }   
}
