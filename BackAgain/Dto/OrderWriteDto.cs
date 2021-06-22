using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{
    public class OrderWriteDto
    {
        public string UserId { get; set; }

        [Required]
        public string POSSerial { get; set; }

        [Required]
        public string TerminalSerial { get; set; }

        [Required]
        public int Table { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string AdditionalInfo { get; set; }

        [Required]
        public List<OrderItemWriteDto> OrderItem { get; set; }


    }
}
