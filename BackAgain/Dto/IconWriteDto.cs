using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{
    public class IconWriteDto
    {
        [Required]
        public string ImageName { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
