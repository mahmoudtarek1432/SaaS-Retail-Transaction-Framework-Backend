using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{
    public class UserSettingsReadDto
    {

        public string PrimaryColor { get; set; }

        public string SecondaryColor { get; set; }

        public string AccentColor { get; set; }

        public string LabelColor { get; set; }

        public string MainTextColor { get; set; }

        [Required]
        public string BrandName { get; set; }

        [Required]
        public string Icon { get; set; }

        [Required]
        public int TerminalMode { get; set; }

        [Required]
        public string ThemeName { get; set; }

        [Required]
        public int ThemeId { get; set; }
    }
}
