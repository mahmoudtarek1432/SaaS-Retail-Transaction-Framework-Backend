using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackAgain.Model
{
    public class ItemExtra
    {

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        [ForeignKey("menuItem")]
        public string ItemId { get; set; }
        public MenuItem MenuItem { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public CustomIdentityUser User { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public bool Display { get; set; }

        [Required]
        public string Code { get; set; }
    }
}