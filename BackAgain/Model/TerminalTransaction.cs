using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Model
{
    public class TerminalTransaction
    {
        //the transaction can be sender or reciver
        [Key]
        public int Id { get; set; }

        [ForeignKey("Transaction")]
        public int TransactionID { get; set; }
        public Transaction Transaction { get; set; }

        [ForeignKey("Pos")]
        public int PosId { get; set; }
        public POS Pos { get; set; }

        [ForeignKey("Terminal")]
        public int TerminalId { get; set; }
        public Terminal Terminal { get; set; }

        public string Affiliation { get; set; } //can be issuer or reciver
        //the reciver transaction is used to 
    }
}
