using System.ComponentModel.DataAnnotations;

namespace BackAgain.Dto
{
    public class ItemOptionReadDto
    {
        public string Id { get; set; }

        [Required]
        public string ItemId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public bool Display { get; set; }

        [Required]
        public string Code { get; set; }
    }
}