using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{
    public class TerminalWriteDto
    {

        [Required]
        public string Serial { get; set; }

        [Required]
        public int state { get; set; }

        [Required]
        public int Table { get; set; }

        [Required]
        public int PosId { get; set; }
    }
}
