using BackAgain.Dto;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Data
{
    public interface ISettingsRepo
    {
        Task<ClientResponseManager> CreateUserSettings(UserSettings userSettings);
        TerminalMode GetModeById(int ModeId);
        Theme GetDefaultThemeSettings(int themeId);
        UserSettings GetUserSettings(string UserId);
        Task<bool> UpdateUserSettings(UserSettings userSettings);
        void SaveChanges();
    }
}
