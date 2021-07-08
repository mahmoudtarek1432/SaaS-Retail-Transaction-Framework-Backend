using BackAgain.Dto;
using BackAgain.Model;
using BackAgain.Service;
using BackAgain.Service.Interface;
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
    public class TerminalUpdateController : ControllerBase
    {
        private readonly IMenuService _MenuService;
        private readonly IVersionUpdateService _VersionRepo;
        private readonly IUserSettingsService _SettingsService;

        public TerminalUpdateController(IVersionUpdateService VersionRepo, IMenuService menuService, IUserSettingsService settingsService)
        {
            _MenuService = menuService;
            _VersionRepo = VersionRepo;
            _SettingsService = settingsService;
        }

        [HttpPost("CheckForUpdates")]
        public ClientResponseManager<int> CheckForUpdates([FromBody] VersionReadDto version)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var result = _VersionRepo.CheckTerminalVersion(userId.Value, version);
                return result;
            }
            return new ClientResponseManager<int>
            {
                IsSuccessfull = false,
                Message = "user credentials not correct"
            };
        }

        [HttpGet("Menu")]
        public ClientResponseManager<TerminalMenuDto> GetTerminalMenu()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var Menu = _MenuService.GetFullMenu(userId.Value);
                var result = _VersionRepo.MenuUpdate(userId.Value, Menu.ResponseObject);
                result.menu.Categories.ForEach(C => { if (C.Items == null) C.Items = new List<MenuItemReadDto>(); });
                return new ClientResponseManager<TerminalMenuDto>
                {
                    ResponseObject = result,
                    IsSuccessfull = true
                };
            }
            return new ClientResponseManager<TerminalMenuDto>
            {
                IsSuccessfull = false,
                Message = "user credentials not correct"
            };
        }

        [HttpGet("TerminalSettings")]
        public ClientResponseManager<TerminalSettingsDto> GetTerminalSettings()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var settings = _SettingsService.GetUserDisplaySettings(userId.Value);

                var result = _VersionRepo.SettingsUpdate(userId.Value, settings);
                return new ClientResponseManager<TerminalSettingsDto>
                {
                    ResponseObject = result,
                    IsSuccessfull = true
                };
            }
            return new ClientResponseManager<TerminalSettingsDto>
            {
                IsSuccessfull = false,
                Message = "user credentials not correct"
            };
        }
    }
}
