using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{
    public class ItemOptionWriteDto
    {
        public string Id { get; set; }
        [Required]
        public string ItemId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public bool Display { get; set; }

        [Required]
        public string Code { get; set; }
    }
}
