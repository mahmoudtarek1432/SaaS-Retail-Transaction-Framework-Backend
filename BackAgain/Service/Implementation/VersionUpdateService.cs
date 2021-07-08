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

        public ClientResponseManager<int> CheckTerminalVersion(string userId, VersionReadDto CurrentVersion)
        {
            
            var version = _VersionUpdateRepo.GetNotUpdatedVersions(userId, CurrentVersion.MenuVersion, CurrentVersion.SettingsVersion).ToList();
            bool menuUpToDate = true;
            bool settingsUpToDate = true;
            version.ForEach( v => { 
                menuUpToDate = (menuUpToDate && v.MenuVersion > CurrentVersion.MenuVersion)? false : menuUpToDate;
                settingsUpToDate = (settingsUpToDate && v.SettingsVersion > CurrentVersion.SettingsVersion) ? false : settingsUpToDate;
                if (menuUpToDate == false & settingsUpToDate == false) return;
            });

            if(menuUpToDate == true && settingsUpToDate == true)
            {
                return new ClientResponseManager<int>
                {
                    IsSuccessfull = true,
                    Message = "terminal up to date",
                    ResponseObject = (int)VersionUpdateTypes.UpToDate
                };
            }
            if (menuUpToDate == false && settingsUpToDate == true)
            {
                return new ClientResponseManager<int>
                {
                    IsSuccessfull = true,
                    Message = "Terminal Menu OutDated",
                    ResponseObject = (int)VersionUpdateTypes.MenuOutDated
                };
            }
            if (menuUpToDate == true && settingsUpToDate == false)
            {
                return new ClientResponseManager<int>
                {
                    IsSuccessfull = true,
                    Message = "Terminal settings OutDated",
                    ResponseObject = (int)VersionUpdateTypes.SettingsOutdated
                };
            }
            if (menuUpToDate == false && settingsUpToDate == false)
            {
                return new ClientResponseManager<int>
                {
                    IsSuccessfull = true,
                    Message = "Terminal Settings and Menu are OutDated",
                    ResponseObject = (int)VersionUpdateTypes.MenuAndSettingsOutdated
                };
            }
            return new ClientResponseManager<int>
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

        public async Task OnVersionUpdate(string userId, string type)
        {
            var version = new VersionUpdateLog();
            try
            {
                version = _VersionUpdateRepo.GetLastVersioning(userId);
            }
            catch(Exception e)
            {
                throw new Exception();
            }

            if (version != null)
            {
                if (type == "Menu")
                {
                    version.UpdateIn = 1;
                    version.MenuVersion += 1;
                }
                else if (type == "Settings")
                {
                    version.UpdateIn = 2;
                    version.SettingsVersion += 1;
                }
            }
            version.ID = 0;
            await _VersionUpdateRepo.CreateVersion(version);
            _VersionUpdateRepo.SaveChanges();
        }
    }
}
