using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackAgain.Model
{
    public class POS
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public CustomIdentityUser User { get; set; }

        // a serial is a guid generate first time at the client
        [Required]
        public string Serial { get; set; }

        [ForeignKey("PosState")]
        public int state { get; set; }
        public PosState PosState { get; set; }

        public IEnumerable<Terminal> Terminal { get; set; }

        public IEnumerable<Order> Orders { get; set; }

        public SocketConnection PosConnection { get; set; }

        public IEnumerable<TransactionAffiliates> transaction { get; set; }
    }
}