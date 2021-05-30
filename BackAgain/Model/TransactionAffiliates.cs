using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Model
{
    public class TransactionAffiliates
    {
        //the transaction can be sender or reciver
        [Key]
        public int Id { get; set; }

        [ForeignKey("Transaction")]
        public string TransactionID { get; set; }
        public Transaction Transaction { get; set; }

        [ForeignKey("Pos")]
        public string PosSerial { get; set; }
        public POS Pos { get; set; }

        [ForeignKey("Terminal")]
        public string TerminalSerial { get; set; }
        public Terminal Terminal { get; set; }

        public string Affiliation { get; set; } //can be issuer or reciver
        //the reciver transaction is used to 
    }
}
