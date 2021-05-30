using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{
    public class UserRegisterDto
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 8)]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
     
        public string PublicKey { get; set; }

    }
}
