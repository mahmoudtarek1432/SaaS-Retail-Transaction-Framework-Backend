using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackAgain.Model
{
    public class SocketConnection
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string ConnectionID { get; set; }

        [ForeignKey("pos")]
        public int? PosID { get; set; }
        public POS pos { get; set; }

        [ForeignKey("Terminal")]
        public int? TerminalId { get; set; }
        public Terminal Terminal { get; set; }

    }
}