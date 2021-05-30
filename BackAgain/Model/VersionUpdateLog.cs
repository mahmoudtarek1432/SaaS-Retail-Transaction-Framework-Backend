using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Model
{
    public class VersionUpdateLog
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public CustomIdentityUser User { get; set; }

        [ForeignKey("UpdateType")]
        public int UpdateIn { get; set; }
        public UpdateType UpdateType { get; set; }

        public float SettingsVersion { get; set; }

        public float MenuVersion { get; set; }
    }
}
