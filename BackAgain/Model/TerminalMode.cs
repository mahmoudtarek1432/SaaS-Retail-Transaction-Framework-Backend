using System.ComponentModel.DataAnnotations;

namespace BackAgain.Model
{
    public class TerminalMode
    {
        [Required]
        [Key]
        public int ModeId { get; set; }

        [Required]
        public string Mode { get; set; }
    }
}