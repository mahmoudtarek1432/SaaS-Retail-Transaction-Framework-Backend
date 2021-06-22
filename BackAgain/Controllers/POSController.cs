using BackAgain.Dto;
using BackAgain.Service;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class POSController : ControllerBase
    {
        private readonly IPosService _PosService;

        public POSController(IPosService PosService)
        {
            _PosService = PosService;
        }

        [HttpPost("CreatePos")]
        public async Task<ActionResult<ClientResponseManager>> CreateNewPos([FromBody] POSWriteDto model)
        {
            if (ModelState.IsValid)
            {
                var UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var result = await _PosService.CreatePOS(UserId, model);
                return result;
            }
            return new ClientResponseManager
            {
                IsSuccessfull = false,
                Message = "the model is not valid"
            };
        }

        [HttpGet("AllPOSs")]
        public ActionResult<ClientResponseManager<IEnumerable<POSReadDto>>> GetAllPOSs()
        {
            if (ModelState.IsValid)
            {
                var user= User.FindFirst(ClaimTypes.NameIdentifier);
                if(user != null)
                {
                    var result = _PosService.getAllPOSsByUserId(user.Value);
                    return result;
                }
                return new ClientResponseManager<IEnumerable<POSReadDto>>
                {
                    IsSuccessfull = false,
                    Message = "user is not logged in"
                };
            }
            return new ClientResponseManager<IEnumerable<POSReadDto>>
            {
                IsSuccessfull = false,
                Message = "the model is not valid"
            };
        }

        [HttpPatch]
        public ActionResult<ClientResponseManager> UpdatePos([FromBody] POSUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                var result = _PosService.UpdatePos(model);
                return result;
            }
            return new ClientResponseManager
            {
                IsSuccessfull = false,
                Message = "the model is not valid"
            };
        }

        [HttpDelete("{PosSerial}")]
        public ActionResult<ClientResponseManager> RemovePos(string PosSerial)
        {
            if (ModelState.IsValid)
            {
                var result = _PosService.removePos(PosSerial);
                return result;
            }
            return new ClientResponseManager
            {
                IsSuccessfull = false,
                Message = "the model is not valid"
            };
        }
    }
}
