using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Model
{
    public class Order
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [ForeignKey("User")]
        [Required]
        public string UserId { get; set; }
        public CustomIdentityUser User { get; set; }

        [ForeignKey("pos")]
        public string POSSerial { get; set; }
        public POS pos { get; set; }

        public int Table { get; set; }

        public DateTime Date { get; set; }

        public string AdditionalInfo { get; set; }

        public List<OrderStatus> OrderStatus { get; set; }

        public List<OrderComment> OrderComment { get; set; }

        public List<OrderItem> OrderItem { get; set; }
    }
}
