using BackAgain.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace BackAgain.Service
{
    public class WebSocketConnectionManager
    {
        private ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        public WebSocketConnectionManager()
        {
           
        }

        public string AddSocket(WebSocket socket)
        {
            string ConnId = Guid.NewGuid().ToString();
            _sockets.TryAdd(ConnId, socket);
            return ConnId;
        }

        public bool RemoveSocket(WebSocket socket)
        {
            
            var obj = _sockets.Where(e => e.Value == socket).FirstOrDefault();
            return _sockets.TryRemove(obj.Key,out _);
        }

        public ConcurrentDictionary<string, WebSocket> getAllSockets()
        {
            return _sockets;
        }
    }
}
