using BackAgain.Dto;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Profiles
{
    public class TerminalProfile : AutoMapper.Profile
    {
        public TerminalProfile()
        {
            CreateMap<TerminalWriteDto, Terminal>();
        }
    }
}
