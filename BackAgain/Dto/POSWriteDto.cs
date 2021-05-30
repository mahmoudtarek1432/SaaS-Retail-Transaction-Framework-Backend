using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{
    public class POSWriteDto
    {

        [Required]
        public int state { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
