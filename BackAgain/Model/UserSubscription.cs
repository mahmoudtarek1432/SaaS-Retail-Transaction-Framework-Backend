using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Model
{
    public class UserSubscription
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public CustomIdentityUser User { get; set; }

        [ForeignKey("Sub")]
        public int SubscriptionId { get; set; }
        public Subscription Sub { get; set; }

        public DateTime SubscriptionDate { get; set; }
    }
}
