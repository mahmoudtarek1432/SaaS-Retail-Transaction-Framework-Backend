using System.ComponentModel.DataAnnotations;

namespace BackAgain.Model
{
    public class TransactionType
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string state { get; set; }
    }
}