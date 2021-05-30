using BackAgain.Dto;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Profiles
{
    public class SettingsProfile : AutoMapper.Profile
    {
        public SettingsProfile()
        {
            CreateMap<UserSettings, UserSettingsWriteDto>();
            CreateMap<UserSettingsWriteDto,UserSettings>();
            CreateMap<UserSettings, UserSettingsReadDto>();
            CreateMap<UserSettingsReadDto, UserSettings>();
        }
    }
}
