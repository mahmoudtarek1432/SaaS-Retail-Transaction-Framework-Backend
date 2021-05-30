using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackAgain.Model
{
    public class OrderStatus
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [ForeignKey("Order")]
        [Required]
        public string OrderId { get; set; }
        public Order Order { get; set; }

        [ForeignKey("OrderState")]
        [Required]
        public int State { get; set; }
        public OrderStateEnum OrderState { get; set; }

        public DateTime Date { get; set; }
    }
}