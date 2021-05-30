using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackAgain.Dto;
using BackAgain.Model;

namespace BackAgain.Data
{
    public class SettingsRepo : ISettingsRepo
    {
        private readonly ProjContext _ctx;

        public SettingsRepo(ProjContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<ClientResponseManager> CreateUserSettings(UserSettings userSettings)
        {
            try
            {
               await _ctx._UserSettings.AddAsync(userSettings);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
                /*return new ClientResponseManager
                {
                    Message = e.Message,
                    IsSuccessfull = false
                };*/
            }
            return new ClientResponseManager
            {
                IsSuccessfull = true,
                Message = "Settings created succesfully"
            };
        }

        public UserSettings GetUserSettings(string UserId)
        {
            return _ctx._UserSettings.Where(e => e.UserId == UserId).AsEnumerable().LastOrDefault();
        }

        public TerminalMode GetModeById(int ModeId)
        {
            return _ctx._TerminalModes.Where(e => e.ModeId == ModeId).FirstOrDefault();
        }

        public Theme GetDefaultThemeSettings(int themeId)
        {
            return _ctx._Theme.Where(e => e.Id == themeId).FirstOrDefault();
        }

        // A new row is add for each new update
        public async Task<bool> UpdateUserSettings(UserSettings userSettings)
        {
            userSettings.Id = 0;
            try
            {
                await _ctx._UserSettings.AddAsync(userSettings);
            }
            catch (Exception c)
            {
                return false;
            }
            return true;
        }

        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }
    }
}
