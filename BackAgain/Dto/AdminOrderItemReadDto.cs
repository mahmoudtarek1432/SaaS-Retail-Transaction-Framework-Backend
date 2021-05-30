using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{
    public class AdminOrderItemReadDto
    {
        public string ItemId { get; set; }

        public string ItemCode { get; set; } //menu item code+option+extra

        /*Menu Item params*/

        public string Name { get; set; }

        public float Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public bool Display { get; set; }

        public int Quantity { get; set; }

        public List<AdminOrderItemExtraReadDto> OrderExtras { get; set; }
    }
}
