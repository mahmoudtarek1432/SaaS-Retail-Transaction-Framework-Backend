using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Model
{
    public class PosState
    {
        [Required]
        [Key]
        public int Id { get; set; }

        public string State { get; set; }
    }
}
