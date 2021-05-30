using BackAgain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Service.Interface
{
    public interface ITerminalAuthService
    {
        ClientResponseManager<string> TerminalLogin(string serial); //Send a jwt token with user credentials, updates the pos state to connected 
        ClientResponseManager TerminalLogout(string serial); // updates the Terminal state to disconnected 
    }
}
