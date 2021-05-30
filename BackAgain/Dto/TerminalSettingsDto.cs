using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{
    public class TerminalSettingsDto
    {
        public UserSettingsReadDto Settings { get; set; }
        public int version { get; set; }
    }
}
