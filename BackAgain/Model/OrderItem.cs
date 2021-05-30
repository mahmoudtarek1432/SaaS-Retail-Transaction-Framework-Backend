using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Model
{
    public class OrderItem
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [ForeignKey("Order")]
        [Required]
        public string OrderId { get; set; }
        public Order Order { get; set; }

        [Required]
        public string ItemCode { get; set; } //menu item code+option+extra

        [Required]
        [ForeignKey("MenuItem")]
        public string ItemId { get; set; }
        public MenuItem MenuItem { get; set; }

        //if null then no options were picked
        [ForeignKey("ItemOption")]
        public string ItemOptionId { get; set; } 
        public ItemOption MyProperty { get; set; }

        [Required]
        public int Quantity { get; set; }

        public List<OrderItemExtra> OrderExtras { get; set; }

    }
}
