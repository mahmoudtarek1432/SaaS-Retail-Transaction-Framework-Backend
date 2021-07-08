using BackAgain.Dto;
using BackAgain.Model;
using BackAgain.Service;
using BackAgain.Service.Implementation;
using BackAgain.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using ITerminalService = BackAgain.Service.Interface.ITerminalService;

namespace BackAgain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _OrderService;
        private readonly ITransactionService _TransactionService;
        private readonly IWebSocketService _webSocketService;
        private readonly ITerminalService _TerminalService;

        public OrdersController(IOrderService OrderService, IAdminOrderService AdminOrderService, ITransactionService TransactionService, IWebSocketService WebsocketService, ITerminalService TerminalService)
        {
            _OrderService = OrderService;
            _TransactionService = TransactionService;
            _webSocketService = WebsocketService;
            _TerminalService = TerminalService;

        }

        [HttpPost("")]
        public async Task<ActionResult<ClientResponseManager<string>>> CreateOrder([FromBody] OrderWriteDto Model)
        {

          /*  using (var http = new HttpClient())
            {
                var returnable = await (http.SendAsync(new HttpRequestMessage().);
            }*/

            if (ModelState.IsValid)
            {
                var user = User.FindFirst(ClaimTypes.NameIdentifier);
                var TerminalId = User.FindFirst(ClaimTypes.SerialNumber);

                if (user != null)
                {
                    var terminalResult = _TerminalService.GetTerminalBySerial(TerminalId.Value, user.Value);
                    Model.UserId = user.Value;
                    Model.POSSerial = terminalResult.ResponseObject.PosSerial;
                    Model.TerminalSerial = terminalResult.ResponseObject.Serial;
                    var result = await _OrderService.CreateOrder(Model);
                    if (result.IsSuccessfull)
                    {
                        

                        if (terminalResult.IsSuccessfull)
                        {
                            var terminal = terminalResult.ResponseObject;
                            var transaction = await _TransactionService.CreateTransaction(terminal.UserId, terminal.PosSerial, terminal.Serial, 2, 1,
                                                                  (int)WebSocketMessageType.NewOrderPlaced, result.ResponseObject.Id);

                            await _webSocketService.SendToPOSBySerial(user.Value, terminal.PosSerial, WebSocketMessageType.NewOrderPlaced, "order is placed", transaction.Id);
                        }

                        return new ClientResponseManager<string>
                        {
                            IsSuccessfull = true,
                            Message = result.Message,
                            ResponseObject = result.ResponseObject.Id
                        };
                    }
                    return new ClientResponseManager<string>
                    {
                        IsSuccessfull = true,
                        Message = "Order Placed, error might have ocurred in pos notification",
                        ResponseObject = result.ResponseObject.Id
                    };
                }
                return new ClientResponseManager<string>
                {
                    IsSuccessfull = false,
                    Message = "User not found"
                };
            }
            return new ClientResponseManager<string>
            {
                IsSuccessfull = false,
                Message = "Model not correct"
            };
        }


        [HttpPost("AddItemToOrder")]
        public async Task<ActionResult<ClientResponseManager>> AddToExistingOrder([FromBody] IEnumerable<OrderItemWriteDto> Model)
        {
            if (ModelState.IsValid)
            {
                var user = User.FindFirst(ClaimTypes.NameIdentifier);
                var TerminalId = User.FindFirst(ClaimTypes.SerialNumber);

                if (user != null)
                {

                    var result = await _OrderService.AddToExistingOrder(user.Value, Model.ToList()[0].OrderId, Model);

                    var terminalResult = _TerminalService.GetTerminalBySerial(user.Value, TerminalId.Value);

                    if (terminalResult.IsSuccessfull)
                    {
                        var terminal = terminalResult.ResponseObject;
                        var transaction = await _TransactionService.CreateTransaction(terminal.UserId, terminal.PosSerial, terminal.Serial, 2, 1,
                                                              (int)WebSocketMessageType.NewOrderPlaced, Model.ToList()[0].OrderId);

                        await _webSocketService.SendToPOSBySerial(user.Value, terminal.PosSerial, WebSocketMessageType.NewItemAddedToOrder, "Item added to order", transaction.Id);
                    }

                    return new ClientResponseManager
                    {
                        IsSuccessfull = true,
                        Message = result.Message
                    };
                }
                return new ClientResponseManager
                {
                    IsSuccessfull = false,
                    Message = "User not found"
                };
            }
            return new ClientResponseManager
            {
                IsSuccessfull = false,
                Message = "Model not correct"
            };
        }

        //used by pos
        [HttpPatch("UpdateState/{OrderId}/{State}")] // confirm or cancel
        public async Task<ActionResult<ClientResponseManager>> UpdateOrderStatus(string OrderId, int State)
        {
            if (ModelState.IsValid)
            {
                var user = User.FindFirst(ClaimTypes.NameIdentifier);
                if (user != null)
                {

                    var result = await _OrderService.UpdateOrderStatus(user.Value, OrderId, State);
                    if (result.IsSuccessfull)
                    {
                        var order = _OrderService.GetOrderByid(OrderId);
                        var messageType = new WebSocketMessageType();
                        switch (State)
                        {
                            case 2:
                                messageType = WebSocketMessageType.OrderComplete;
                                break;
                            case 3:
                                messageType = WebSocketMessageType.OrderCancelled;
                                break;
                        }
                        var transaction = await _TransactionService.CreateTransaction(user.Value, order.TerminalSerial, order.POSSerial, 1, 2, (int)messageType, OrderId);
                        _webSocketService.SendToTerminalBySerial(order.TerminalSerial, messageType, "Order State Updates", transaction.Id);
                    }
                    return result;
                }
                return new ClientResponseManager
                {
                    IsSuccessfull = false,
                    Message = "User not found"
                };
            }
            return new ClientResponseManager
            {
                IsSuccessfull = false,
                Message = "Model not correct"
            };
        }


        [HttpPost("OrderComment")]
        public async Task<ActionResult<ClientResponseManager>> CreateOrderComment([FromBody] OrderCommentWriteDto Model)
        {
            if (ModelState.IsValid)
            {
                var user = User.FindFirst(ClaimTypes.NameIdentifier);

                if (user != null)
                {
                    _OrderService.AddCommentToOrder(Model);
                    var orderInfo = _OrderService.GetOrderByid(Model.OrderId);

                    var transaction = await _TransactionService.CreateTransaction(orderInfo.UserId, orderInfo.POSSerial, orderInfo.TerminalSerial, 2, 1,
                                                          (int)WebSocketMessageType.NewOrderPlaced, Model.OrderId);

                    await _webSocketService.SendToPOSBySerial(user.Value, orderInfo.POSSerial, WebSocketMessageType.CommentAdded, "OrderComment", transaction.Id);
                }
            }
            return Ok(new ClientResponseManager
            {
                IsSuccessfull = true,
                Message = "Comment Added"
            }); 
        }
    }
}
