using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Model
{
    public class OrderTransaction
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Transaction")]
        public string TransactionID { get; set; }
        public Transaction Transaction { get; set; }

        [ForeignKey("Order")]
        public string OrderId { get; set; }
        public Order Order { get; set; }
    }
}
