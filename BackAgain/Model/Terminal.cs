using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Model
{
    public class Terminal
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public CustomIdentityUser User { get; set; }

        // a serial is a guid generate first time at the client
        [Required]
        public string Serial { get; set; }

        [ForeignKey("TerminalState")]
        public int state { get; set; }
        public TerminalState TerminalState { get; set; }

        public int Table { get; set; }

        [ForeignKey("pos")]
        public string PosSerial { get; set; }
        public POS pos { get; set; }

        public SocketConnection TerminalConnection { get; set; }

        public IEnumerable<TransactionAffiliates> Transaction { get; set; }
    }
}
