using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Model
{
    public class Theme
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string DefaultPrimary { get; set; }

        [Required]
        public string DefaultSecondary { get; set; }

        [Required]
        public string DefaultAccent { get; set; }

    }
}
