using BackAgain.Data;
using BackAgain.Service;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackAgain.Middleware
{
    public class WebSocketServerMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly WebSocketConnectionManager _manager;

        public WebSocketServerMiddleware(RequestDelegate next, WebSocketConnectionManager manager)
        {
            _next = next;
            _manager = manager;
        }

        public async Task InvokeAsync(HttpContext Context, ITerminalRepo terminalrepo, IPOSRepo POSRepo)
        {

            if (Context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await Context.WebSockets.AcceptWebSocketAsync();
       
                string ConnId = _manager.AddSocket(webSocket);

                await SendConnIDAsync(webSocket, ConnId);

                await Receive(webSocket, async (result, buffer) =>
                {
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        Console.WriteLine($"recive -> text");
                        Console.WriteLine($"message: {Encoding.UTF8.GetString(buffer, 0, result.Count)}");
                        return;
                    }
                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        _manager.RemoveSocket(webSocket);
                        return;
                    }
                });
            }
            else
            {
                await _next(Context);
            }
        }

        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer),
                                                       cancellationToken: CancellationToken.None);

                handleMessage(result, buffer);
            }
        }

        private async Task SendConnIDAsync(WebSocket socket, string connID)
        {
            var buffer = Encoding.UTF8.GetBytes("ConnID:" + connID);
            await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public void AddTerminalSocket(string ConnId, string terminalGuID, TerminalRepo _terminalRepo)
        {
            _terminalRepo.updateTerminalConnId(terminalGuID, ConnId);

        }

        public void AddPosSocket(string ConnId, string PosGuID, POSRepo _POSRepo)
        {
            _POSRepo.updatePOSConnId(PosGuID, ConnId);

        }
    }
}
