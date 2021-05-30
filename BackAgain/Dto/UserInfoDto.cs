using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{
    public class UserInfoDto
    {
        public string FirstName { get; set; }

        public string PairingId { get; set; }

        public string Role { get; set; }

        public DateTime SubDate { get; set; }

        public int SubType { get; set; }

        public string PublicKey { get; set; }
    }
}
