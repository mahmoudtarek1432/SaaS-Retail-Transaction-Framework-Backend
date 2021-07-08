using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Model
{
    public class UserSettings
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public CustomIdentityUser User { get; set; }

        public string PrimaryColor { get; set; }

        public string SecondaryColor { get; set; }

        public string AccentColor { get; set; }

        public string LabelColor { get; set; }

        public string MainTextColor { get; set; }

        [ForeignKey("Theme")]
        public int ThemeId { get; set; }
        public Theme CurrentTheme { get; set; }

        public string BrandName { get; set; }

        public string Icon { get; set; }

        [ForeignKey("Termode")]
        public int TerminalModeId { get; set; }
        public TerminalMode Termode { get; set; }

        public int PaymentMethod { get; set; }
    }
}
