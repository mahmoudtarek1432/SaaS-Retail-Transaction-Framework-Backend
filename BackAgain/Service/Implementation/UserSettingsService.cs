using BackAgain.Data;
using BackAgain.Dto;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace BackAgain.Service
{
    public class UserSettingsService : IUserSettingsService
    {
        private readonly ISettingsRepo _SettingsRepo;

        public IMapper _mapper { get; }

        public UserSettingsService(ISettingsRepo SettingsRepo,IMapper mapper)
        {
            _SettingsRepo = SettingsRepo;
            _mapper = mapper;
        }

        public async Task<ClientResponseManager> CreateUserSettings(string UserId, UserSettingsWriteDto UserSettings)
        {
            var settings = _mapper.Map<UserSettings>(UserSettings);
            settings.UserId = UserId;
            var result = await _SettingsRepo.CreateUserSettings(settings);
            _SettingsRepo.SaveChanges();
            return result;
        }

        public UserSettings GetUserSettings(string UserId)
        {
            var settings = _SettingsRepo.GetUserSettings(UserId);

            return settings;
        }

        public UserSettingsReadDto GetUserDisplaySettings(string UserId)
        {
            var settings = _SettingsRepo.GetUserSettings(UserId);
            var dto = _mapper.Map<UserSettingsReadDto>(settings);
            dto.TerminalMode = _SettingsRepo.GetModeById(settings.TerminalModeId).ModeId;
            var defaultTheme = _SettingsRepo.GetDefaultThemeSettings(settings.ThemeId);
            if (settings.PrimaryColor == null || settings.SecondaryColor == null)
            {
                dto.PrimaryColor = defaultTheme.DefaultPrimary;
                dto.SecondaryColor = defaultTheme.DefaultSecondary;
                dto.AccentColor = defaultTheme.DefaultAccent;
            }
            dto.ThemeName = defaultTheme.Name;
            return dto;
            
        }


        public async Task<ClientResponseManager> UpdateUserSettings(string UserId, UserSettingsWriteDto UserSettings)
        {
            var settings = GetUserSettings(UserId);

            if (!string.IsNullOrEmpty(UserSettings.BrandName))
            {
                settings.BrandName = UserSettings.BrandName;
            }
            if (!string.IsNullOrEmpty(UserSettings.PrimaryColor))
            {
                settings.PrimaryColor = UserSettings.PrimaryColor;
            }
            if (!string.IsNullOrEmpty(UserSettings.SecondaryColor))
            {
                settings.SecondaryColor = UserSettings.SecondaryColor;
            }
            if (!string.IsNullOrEmpty(UserSettings.AccentColor))
            {
                settings.AccentColor = UserSettings.AccentColor;
            }
            if (!string.IsNullOrEmpty(UserSettings.LabelColor))
            {
                settings.LabelColor = UserSettings.LabelColor;
            }

            if (!string.IsNullOrEmpty(UserSettings.MainTextColor))
            {
                settings.MainTextColor = UserSettings.MainTextColor;
            }
            if (!string.IsNullOrEmpty(UserSettings.Icon))
            {
                settings.Icon = UserSettings.Icon;
            }

            settings.ThemeId = UserSettings.ThemeId;
            settings.TerminalModeId = UserSettings.TerminalModeId;
            
            var result = await _SettingsRepo.UpdateUserSettings(settings);
            _SettingsRepo.SaveChanges();
            if (!result)
            {
                return new ClientResponseManager
                {
                    IsSuccessfull = false,
                    Message = "Update Not Successful"
                };
            }
            return new ClientResponseManager
            {
                IsSuccessfull = true,
                Message = "Update Complete SuccessFully"
            };
        }
    }
}
