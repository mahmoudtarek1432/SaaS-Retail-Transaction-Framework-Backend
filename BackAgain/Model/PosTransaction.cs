using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Model
{
    public class PosTransaction
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Transaction")]
        public int TransactionID { get; set; }
        public Transaction Transaction { get; set; }

        [ForeignKey("Pos")]
        public int PosId { get; set; }
        public POS Pos { get; set; }

        public string Affiliation { get; set; } //can be issuer or reciver
        //the reciver transaction is used to 
    }
}
