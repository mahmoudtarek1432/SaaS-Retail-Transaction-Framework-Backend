using System.ComponentModel.DataAnnotations;

namespace BackAgain.Model
{
    public class UpdateType
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string UpdateAble { get; set; }
    }
}