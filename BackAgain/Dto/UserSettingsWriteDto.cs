using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{
    public class UserSettingsWriteDto
    {
        [Required]
        public string PrimaryColor { get; set; }

        [Required]
        public string SecondaryColor { get; set; }

        [Required]
        public string AccentColor { get; set; }

        [Required]
        public string LabelColor { get; set; }

        [Required]
        public string MainTextColor { get; set; }

        [Required]
        public int ThemeId { get; set; }

        [Required]
        public string BrandName { get; set; }

        [Required]
        public string Icon { get; set; }

        [Required]
        public int TerminalModeId { get; set; }

        [Required]
        public int PaymentMethod { get; set; }
    }
}
