using BackAgain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Service
{
    public interface IPOSAuthService
    {
        ClientResponseManager<string> POSLogin(string serial); //Send a jwt token with user credentials, updates the pos state to connected 
        ClientResponseManager POSLogout(string serial); // updates the pos state to disconnected 
    }
}
