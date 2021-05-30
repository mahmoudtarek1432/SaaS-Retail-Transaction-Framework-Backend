using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{
    public class TerminalReadDto
    {
        public int Id { get; set; }

        // a serial is a guid generate first time at the client
        public string Serial { get; set; }

        public int state { get; set; }

        public int Table { get; set; }

        public string PosSerial { get; set; }

        public string TerminalState { get; set; } //got from state table
    }
}
