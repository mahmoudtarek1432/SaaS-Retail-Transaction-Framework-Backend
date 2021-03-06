using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackAgain.Dto;
using BackAgain.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using BackAgain.Data;
using BackAgain.Model;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using BackAgain.Service.Interface;

namespace BackAgain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerminalSettingsConfigurationController : ControllerBase
    {
        private readonly IUserSettingsService _UserSettings;

        public IHostingEnvironment WebHost;

        private readonly ITerminalRepo _terminalRepo;
        private readonly IWebSocketService _SocketService;
        private readonly IVersionUpdateService _VerRepo;

        public TerminalSettingsConfigurationController(IVersionUpdateService VerRepo, IUserSettingsService UserSettings, IHostingEnvironment webHost, ITerminalRepo terminalRepo, IWebSocketService ws)
        {
            _UserSettings = UserSettings;
            WebHost = webHost;
            _terminalRepo = terminalRepo;
            _SocketService = ws;
            _VerRepo = VerRepo;
        }

        [HttpPost("")]
        [Authorize]
        public async Task<ActionResult<ClientResponseManager>> SetupSettingsConfiguration([FromBody] UserSettingsWriteDto userSettings)
        {
            if (ModelState.IsValid)
            {
                if(User != null)
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var result = await _UserSettings.CreateUserSettings(userId, userSettings);
                    if (result.IsSuccessfull)
                    {
                        return Ok(result);
                    }
                }
                return BadRequest(new ClientResponseManager
                {
                    IsSuccessfull = false,
                    Message = "The login Token is not valid"
                });
            }
            return BadRequest(new ClientResponseManager
            {
                IsSuccessfull = false,
                Message = "The input is not correct"
            });
        }

        [HttpPatch("")]
        [Authorize]
        public async Task<ActionResult<ClientResponseManager>> UpdateSettingsConfiguration([FromBody] UserSettingsWriteDto userSettings)
        {
            if (ModelState.IsValid)
            {
                if (User != null)
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var result = await _UserSettings.UpdateUserSettings(userId, userSettings);
                    if(result.IsSuccessfull == true)
                    {
                        var socketMessage = WebSocketMessageType.SettingsUpdated;
                        var message = "Settings Has Been Updated";

                        _SocketService.SendToAllUserTerminals(userId, socketMessage, message, null);

                        await _VerRepo.OnVersionUpdate(userId, "Settings");
                        
                        return Ok(result);
                    }
                    return BadRequest(new ClientResponseManager
                    {
                        IsSuccessfull = false,
                        Message = "Settings Update Failed"
                    });
                }
                return BadRequest(new ClientResponseManager
                {
                    IsSuccessfull = false,
                    Message = "The login Token is not valid"
                });
            }
            return BadRequest(new ClientResponseManager
            {
                IsSuccessfull = false,
                Message = "The Model is invalid"
            });
        }

        [HttpGet("")]
        [Authorize]
        public ActionResult<ClientResponseManager<UserSettingsReadDto>>GetSettingsConfiguration() //actually adds a new row
        {
            if (ModelState.IsValid)
            {
                if (User != null)
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var result = _UserSettings.GetUserDisplaySettings(userId);

                    return new ClientResponseManager<UserSettingsReadDto>
                    {

                        IsSuccessfull = true,
                        ResponseObject = result,
                        Message = "Process Successful"
                    };
                }
                return BadRequest(new ClientResponseManager<UserSettingsReadDto>
                {
                    IsSuccessfull = false,
                    Message = "The login Token is not valid"
                });
            }
            return BadRequest(new ClientResponseManager<UserSettingsReadDto>
            {
                IsSuccessfull = false,
                Message = "The input is not correct"
            });
        }


    }
}