using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackAgain.Model
{
    public class OrderItemExtra
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        
        [ForeignKey("OrderItem")]
        [Required]
        public string OrderItemId { get; set; }
        public OrderItem OrderItem { get; set; }

        [ForeignKey("ItemExtras")]
        [Required]
        public string ItemExtraId { get; set; }
        public ItemExtra ItemExtras { get; set; }

    }
}