using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace BackAgain.Service
{
    public interface IWebSocketService
    {
        WebSocket GetSocketConnection(SocketConnection socketConnection);
    }
}
