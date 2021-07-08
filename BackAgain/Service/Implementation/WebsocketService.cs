using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BackAgain.Dto;
using BackAgain.Data;
using WebSocketMessageType = BackAgain.Model.WebSocketMessageType;
using Microsoft.AspNetCore.Identity;

namespace BackAgain.Service
{
    public class WebsocketService : IWebSocketService
    {
        private readonly WebSocketConnectionManager _Manager;
        private readonly ITerminalRepo _TerminalRepo;
        private readonly IPOSRepo _PosRepo;
        private readonly IUserRepo _UserRepo;

        public WebsocketService(WebSocketConnectionManager Manager, ITerminalRepo TerminalRepo, IPOSRepo PosRepo, IUserRepo UserRepo)
        {
            _Manager = Manager;
            _TerminalRepo = TerminalRepo;
            _PosRepo = PosRepo;
            _UserRepo = UserRepo;
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

        public bool SendToAllUserTerminals(string userId, WebSocketMessageType MessageType, string Message, string TransactionId)
        {
            var WSResponse = new WebSocketClientResponse { type = (int)MessageType, message = Message, transactionId = TransactionId };
            try
            {
                var terminals = _TerminalRepo.GetTerminalsConnIDByUserId(userId).ToList();
                var SocketConnections = GetSocketConnection(terminals[0]);

                if (SocketConnections != null)
                {
                    WebsocketService.SendSocketMessage(SocketConnections,
                                 WebsocketService.JSONSerialize(WSResponse));
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool SendToAllUserPOSs(string UserId , WebSocketMessageType MessageType, string Message, string TransactionId)
        {
            var WSResponse = new WebSocketClientResponse { type = (int) MessageType, message = Message, transactionId = TransactionId };
            try
            {
                _PosRepo.GetPOSConnIDByUserId(UserId)
                             .Select(GetSocketConnection)
                             .Select(sc => SendSocketMessage(sc,JSONSerialize(WSResponse)
                    )
                );
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<bool> SendToPOSBySerial(string UserId, string POSSerial, WebSocketMessageType MessageType, string Message, string TransactionId)
        {
            var WSResponse = new WebSocketClientResponse { type = (int)MessageType, message = Message, transactionId = TransactionId };
         
                var posConId = _PosRepo.GetPOSConnIDByPOSSerial(POSSerial);
                WebSocket Socket = GetSocketConnection(posConId);
                if(Socket != null)
                {
                WebsocketService.SendSocketMessage(Socket, WebsocketService.JSONSerialize(WSResponse));
                }

                var user = (await _UserRepo.GetUserById(UserId)).ResponseObject;
                var userSocket = _Manager.getAllSockets().Where(Sc => Sc.Key == user.WebSocketConnectionId);
                if (userSocket.FirstOrDefault().Key != null)
                {
                    WebsocketService.SendSocketMessage(userSocket.FirstOrDefault().Value, WebsocketService.JSONSerialize(WSResponse));
                }
           
                return false;
           
        }

        public bool SendToTerminalBySerial(string TerSerial, WebSocketMessageType MessageType, string Message, string TransactionId)
        {
            var WSResponse = new WebSocketClientResponse { type = (int)MessageType, message = Message, transactionId = TransactionId };
            try
            {
                WebSocket Socket = GetSocketConnection(_TerminalRepo.GetConnIDByTerminalSerial(TerSerial));
                if(Socket != null)
                {
                    WebsocketService.SendSocketMessage(Socket, WebsocketService.JSONSerialize(WSResponse));
                }
               
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
