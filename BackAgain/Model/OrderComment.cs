using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Model
{
    public class OrderComment
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [ForeignKey("Order")]
        [Required]
        public string OrderId { get; set; }
        public Order Order { get; set; }

        public string Comment { get; set; }

        public DateTime Date { get; set; }

    }
}
