using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Model
{
    public class CustomIdentityUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string PairingId { get; set; }

        public string Role { get; set; }

        public DateTime SubDate { get; set; }

        [ForeignKey("SubscriptionType")]
        public int SubType { get; set; }

       // public string PublicKey { get; set; }
    }
}
