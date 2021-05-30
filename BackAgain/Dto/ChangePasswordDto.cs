using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{
    public class ChangePasswordDto
    {
        [Required]
        [StringLength(maximumLength: 50)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        public string NewPassword { get; set; }
    }
}
