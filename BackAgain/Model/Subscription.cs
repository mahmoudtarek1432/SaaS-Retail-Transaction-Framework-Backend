using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Model
{
    public class Subscription
    {
        public int Id { get; set; }

        public string Duration { get; set; }

        public string Name { get; set; }

        public int Cost { get; set; }
    }
}
