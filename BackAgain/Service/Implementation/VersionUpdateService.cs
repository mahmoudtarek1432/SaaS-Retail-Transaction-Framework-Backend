using BackAgain.Data.Inteface;
using BackAgain.Dto;
using BackAgain.Model;
using BackAgain.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Service.Implementation
{
    public class VersionUpdateService : IVersionUpdateService
    {
        private readonly IVerisonUpdateRepo _VersionUpdateRepo;

        public VersionUpdateService(IVerisonUpdateRepo vur)
        {
            _VersionUpdateRepo = vur;
        }

        public ClientResponseManager<VersionUpdateTypes> CheckTerminalVersion(string userId, VersionReadDto CurrentVersion)
        {
            
            var version = _VersionUpdateRepo.GetNotUpdatedVersions(userId, CurrentVersion.MenuVersion, CurrentVersion.SettingsVersion).ToList();
            bool menuUpToDate = true;
            bool settingsUpToDate = true;
            version.ForEach( v => { 
                menuUpToDate = (menuUpToDate && v.MenuVersion > CurrentVersion.MenuVersion)? false : true;
                settingsUpToDate = (settingsUpToDate && v.SettingsVersion > CurrentVersion.SettingsVersion) ? false : true;
                if (menuUpToDate == false & settingsUpToDate == false) return;
            });

            if(menuUpToDate == true && settingsUpToDate == true)
            {
                return new ClientResponseManager<VersionUpdateTypes>
                {
                    IsSuccessfull = true,
                    Message = "terminal up to date",
                    ResponseObject = VersionUpdateTypes.UpToDate
                };
            }
            if (menuUpToDate == false && settingsUpToDate == true)
            {
                return new ClientResponseManager<VersionUpdateTypes>
                {
                    IsSuccessfull = true,
                    Message = "Terminal Menu OutDated",
                    ResponseObject = VersionUpdateTypes.MenuOutDated
                };
            }
            if (menuUpToDate == true && settingsUpToDate == false)
            {
                return new ClientResponseManager<VersionUpdateTypes>
                {
                    IsSuccessfull = true,
                    Message = "Terminal settings OutDated",
                    ResponseObject = VersionUpdateTypes.SettingsOutdated
                };
            }
            if (menuUpToDate == false && settingsUpToDate == false)
            {
                return new ClientResponseManager<VersionUpdateTypes>
                {
                    IsSuccessfull = true,
                    Message = "Terminal Settings and Menu are OutDated",
                    ResponseObject = VersionUpdateTypes.MenuAndSettingsOutdated
                };
            }
            return new ClientResponseManager<VersionUpdateTypes>
            {
                IsSuccessfull = true,
                Message = "error occured"
            };
        }

        public TerminalMenuDto MenuUpdate(string userId, MenuReadDto menu)
        {
            var latestVersion = _VersionUpdateRepo.GetLastVersioning(userId);
            var terminalMenu = new TerminalMenuDto
            {
                menu = menu,
                version = (int)latestVersion.MenuVersion
            };
            return terminalMenu;
        }

        public TerminalSettingsDto SettingsUpdate(string userId, UserSettingsReadDto settings)
        {
            var latestVersion = _VersionUpdateRepo.GetLastVersioning(userId);
            var UserSettings = new TerminalSettingsDto
            {
                Settings = settings,
                version = (int)latestVersion.SettingsVersion
            };
            return UserSettings;
        }
    }
}
