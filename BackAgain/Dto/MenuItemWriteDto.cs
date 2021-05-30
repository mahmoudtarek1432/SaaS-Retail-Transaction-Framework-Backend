using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{
    public class MenuItemWriteDto
    {
        [Required]
        public string CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Image { get; set; } //base 64 string

        [Required]
        public bool Display { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public bool HasOptions { get; set; }

    }
}
