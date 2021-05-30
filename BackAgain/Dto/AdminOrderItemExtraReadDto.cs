using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{
    public class AdminOrderItemExtraReadDto
    {
         public string OrderExtraId { get; set; }

         public string ItemExtraId { get; set; }

         public string Name { get; set; }

         public float Price { get; set; }

         public string Image { get; set; }

         public bool Display { get; set; }

         public string Code { get; set; }

    }
}
