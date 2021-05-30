using BackAgain.Dto;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Service
{
    public interface IUserSettingsService
    {
        Task<ClientResponseManager> CreateUserSettings(string UserId, UserSettingsWriteDto UserSettings);
        UserSettings GetUserSettings(string UserId);
        Task<ClientResponseManager> UpdateUserSettings(string UserId, UserSettingsWriteDto UserSettings);
        UserSettingsReadDto GetUserDisplaySettings(string UserId);
    }
}
