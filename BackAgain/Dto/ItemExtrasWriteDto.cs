using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{
    public class ItemExtrasWriteDto
    {
        public string Id { get; set; }

        public string ItemId { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }

        public string Image { get; set; } //base 64 string

        public bool Display { get; set; }

        public string Code { get; set; }
    }
}
