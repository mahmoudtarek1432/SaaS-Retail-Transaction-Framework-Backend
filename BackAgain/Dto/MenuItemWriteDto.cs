using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{
    public class MenuItemWriteDto
    {
        public string Id { get; set; }
        [Required]
        public string CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public string Description { get; set; }

        public string Image { get; set; } //string url

        [Required]
        public bool Display { get; set; }

        [Required]
        public string Code { get; set; }

    }
}
