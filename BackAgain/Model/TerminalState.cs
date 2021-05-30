using System.ComponentModel.DataAnnotations;

namespace BackAgain.Model
{
    public class TerminalState
    {
        [Required]
        [Key]
        public int Id { get; set; }

        public string State { get; set; }
    }
}