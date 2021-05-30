using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackAgain.Model
{
    public class MenuItem
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        [ForeignKey("Category")]
        public string CategoryId { get; set; }
        public Category Category { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public CustomIdentityUser User { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public bool Display { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public bool HasOptions { get; set; }

        public List<ItemExtra> ItemExtras { get; set; }

        public List<ItemOption> ItemOptions { get; set; }
    }
}