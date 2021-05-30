using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BackAgain.Service
{
    public class WebsocketService : IWebSocketService
    {
        private readonly WebSocketConnectionManager _Manager;

        public WebsocketService(WebSocketConnectionManager Manager)
        {
            _Manager = Manager;
        }

        public static WebSocket SendSocketMessage(WebSocket socket, string Message)
        {
            var bytes = Encoding.UTF8.GetBytes(Message);
            socket.SendAsync(bytes, System.Net.WebSockets.WebSocketMessageType.Text, true, CancellationToken.None);
            return socket;
        }

        public static string JSONSerialize (object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public WebSocket GetSocketConnection(SocketConnection socketConnection)
        {
            return _Manager.getAllSockets().Where(sc => sc.Key == socketConnection.ConnectionID).FirstOrDefault().Value;
        }
    }
}
