using System.ComponentModel.DataAnnotations;

namespace BackAgain.Model
{
    public class TransactionState
    {
        // let state 1-unfulfilled 2-fulfilled
        [Key]
        [Required]
        public int Id { get; set; }

        public string state { get; set; }
    }
}