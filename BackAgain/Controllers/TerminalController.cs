using BackAgain.Dto;
using BackAgain.Service;
using BackAgain.Service.Interface;
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
    public class TerminalController : ControllerBase
    {
        private readonly ITerminalService _TerminalService;

        public IPosService _PosService { get; }

        public TerminalController(ITerminalService terservice, IPosService posService)
        {
            _TerminalService = terservice;
            _PosService = posService;
        }

        [HttpPost("CreateTerminal")]
        public ActionResult<ClientResponseManager> CreateNewTerminal([FromBody] TerminalWriteDto model)
        {
            if (ModelState.IsValid)
            {
                var UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var result = _TerminalService.CreateTerminal(UserId, model);
                return result;
            }
            return new ClientResponseManager
            {
                IsSuccessfull = false,
                Message = "the model is not valid"
            };
        }

        [HttpGet("AllTerminals/{PosSerial}")] //get terminals by userid
        public ActionResult<ClientResponseManager<IEnumerable<TerminalReadDto>>> GetAllTerminals(string PosSerial)
        {
            if (ModelState.IsValid)
            {
                var user = User.FindFirst(ClaimTypes.NameIdentifier);
                if (user != null)
                {
                    var pos = _PosService.GetPOSBySerial(PosSerial);
                    if (pos.IsSuccessfull)
                    {
                        var Poserial = pos.ResponseObject.Serial;
                        var result = _TerminalService.getAllTerminalsByPosSerial(user.Value, Poserial);
                        return result;
                    }
                    
                }
                return new ClientResponseManager<IEnumerable<TerminalReadDto>>
                {
                    IsSuccessfull = false,
                    Message = "user is not logged in"
                };
            }
            return new ClientResponseManager<IEnumerable<TerminalReadDto>>
            {
                IsSuccessfull = false,
                Message = "the model is not valid"
            };
        }

        [HttpPatch]
        public ActionResult<ClientResponseManager> UpdateTerminal([FromBody] TerminalUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                var user = User.FindFirst(ClaimTypes.NameIdentifier);
                if (user != null)
                {
                    var result = _TerminalService.UpdateTerminal(user.Value, model);
                    return result;
                }
                return new ClientResponseManager
                {
                    IsSuccessfull = false,
                    Message = "user is not logged in"
                };
            }
            return new ClientResponseManager
            {
                IsSuccessfull = false,
                Message = "the model is not valid"
            };
        }

        [HttpDelete("{TerminalSerial}")]
        public ActionResult<ClientResponseManager> RemoveTerminal(string TerminalSerial)
        {
            if (ModelState.IsValid)
            {
                var user = User.FindFirst(ClaimTypes.NameIdentifier);
                if (user != null)
                {
                    var result = _TerminalService.removeTerminal(user.Value,TerminalSerial);
                    return result;
                }
                return new ClientResponseManager
                {
                    IsSuccessfull = false,
                    Message = "user is not logged in"
                };
            }
            return new ClientResponseManager
            {
                IsSuccessfull = false,
                Message = "the model is not valid"
            };
        }
    }
}
