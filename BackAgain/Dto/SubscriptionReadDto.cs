using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{
    public class SubscriptionReadDto
    {
        public string Duration { get; set; }

        public string Name { get; set; }

        public DateTime SubscriptionDate { get; set; }

        public string UserId { get; set; }

        public int SubscriptionId { get; set; }

        public int Cost { get; set; }
    }
}
