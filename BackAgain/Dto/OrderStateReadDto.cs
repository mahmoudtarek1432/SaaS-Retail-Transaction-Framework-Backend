using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{
    public class OrderStateReadDto
    {
        public int Id { get; set; }

        public string OrderId { get; set; }

        public int State { get; set; }

        public DateTime Date { get; set; }
    }
}
