using System.ComponentModel.DataAnnotations;

namespace BackAgain.Model
{
    public class OrderStateEnum
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string State { get; set; }
    }
    /*
     * pending
     * confirmed
     * finished
     */
}