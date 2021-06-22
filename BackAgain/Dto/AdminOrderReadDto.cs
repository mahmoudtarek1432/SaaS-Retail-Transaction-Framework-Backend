using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{
    // has all the info including all statuses and comments
    public class AdminOrderReadDto
    {
        public string Id { get; set; }

        public string POSSerial { get; set; }

        public string TerminalSerial { get; set; }

        public int Table { get; set; }

        public DateTime Date { get; set; }

        public string AdditionalInfo { get; set; }

        public List<OrderStateReadDto> OrderStatus { get; set; }

        public List<OrderComment> OrderComment { get; set; }

        public List<AdminOrderItemReadDto> OrderItem { get; set; }
    }
}
