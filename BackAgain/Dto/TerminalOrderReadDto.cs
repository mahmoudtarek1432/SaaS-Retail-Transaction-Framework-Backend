using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{
    public class TerminalOrderReadDto
    {
        public int Id { get; set; }

        public string POSSerial { get; set; }

        public int Table { get; set; }

        public DateTime Date { get; set; }

        public string AdditionalInfo { get; set; }

        public OrderStatus CurrentOrderStatus { get; set; }

        public List<OrderItem> OrderItem { get; set; }
    }
}
