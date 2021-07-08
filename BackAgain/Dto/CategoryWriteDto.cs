using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{
    public class CategoryWriteDto
    {
        public string Id { get; set; }
        [Required]
        public string MenuId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool Display { get; set; }
    }
}
