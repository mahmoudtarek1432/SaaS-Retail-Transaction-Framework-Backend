using BackAgain.Dto;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Service.Interface
{
    public interface IVersionUpdateService
    {
        //user version returns a state code in which the state of the terminal
        //is returned

        ClientResponseManager<int> CheckTerminalVersion(string userId, VersionReadDto CurrentVersion);

        TerminalMenuDto MenuUpdate(string userId, MenuReadDto menu);

        TerminalSettingsDto SettingsUpdate(string userId, UserSettingsReadDto settings);

        Task OnVersionUpdate(string userId, string type);


    }
}
