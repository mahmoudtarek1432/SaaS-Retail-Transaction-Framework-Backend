using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{
    public class POSReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Serial { get; set; }

        public int state { get; set; }

        public string PosState { get; set; } //got from state DBtable

    }
}
