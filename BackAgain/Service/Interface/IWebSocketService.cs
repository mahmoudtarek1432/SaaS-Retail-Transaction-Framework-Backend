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

        bool SendToAllUserTerminals(string userId, Model.WebSocketMessageType MessageType, string Message, string TransactionId);
        bool SendToAllUserPOSs(string UserId, Model.WebSocketMessageType MessageType, string Message, string TransactionId);
        Task<bool> SendToPOSBySerial(string UserId, string POSSerial, Model.WebSocketMessageType MessageType, string Message, string TransactionId);
        bool SendToTerminalBySerial(string TerSerial, Model.WebSocketMessageType MessageType, string Message, string TransactionId);
    }
}
