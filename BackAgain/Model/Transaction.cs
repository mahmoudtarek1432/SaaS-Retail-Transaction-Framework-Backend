using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Model
{
    public class Transaction
    {
        [Key]
        [Required]
        public string Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public CustomIdentityUser User { get; set; }

        public string Message { get; set; }

        [ForeignKey("TransactionType")]
        public int Type { get; set; } // of type WebsocketMessageType enum
        public TransactionType MyProperty { get; set; }

        public int FailedTries { get; set; }

        [ForeignKey("TransactionStatus")]
        public int State { get; set; }
        public TransactionState TransactionState { get; set; }

        [NotMapped]
        public OrderTransaction? OrderTransaction { get; set; }

        public IEnumerable<TransactionAffiliates> transactionAffiliates { get; set; }

    }
}
