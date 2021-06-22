using BackAgain.Data;
using BackAgain.Service;
using BackAgain.Service.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackAgain.Middleware
{
    public class WebSocketTransactionHostedService : BackgroundService
    {
        private IServiceProvider _Service;
        private readonly ITransactionService _TransactionServcie;
        private readonly IWebSocketService _WebsocketService;

        public WebSocketTransactionHostedService(IServiceProvider service)
        {
            _Service = service;
            //the services are provided from the IServiceProvider as the HostedServices are
            //considered Singleton instances. a scope that must be destroyed after instantiation should be made too.
            using(var scope = service.CreateScope())
            {
                _TransactionServcie = (ITransactionService)service.CreateScope().ServiceProvider.GetRequiredService(typeof(ITransactionService));
                _WebsocketService = (IWebSocketService)service.CreateScope().ServiceProvider.GetRequiredService(typeof(IWebSocketService));
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //get all the transactions
                var transactions = _TransactionServcie.GetAllTransactions();
                transactions.ToList().ForEach(T =>
               {
                   if (T.FailedTries >= 3)
                   {
                       _TransactionServcie.DeleteTransaction(T.Id);
                       if (T.OrderTransaction != null)
                       {
                           T.transactionAffiliates.Where(TA => TA.Affiliation == "Issuer" && TA.PosSerial != null)
                                                  .Select( async TA =>
                                                (TA.PosSerial != null) ?
                                                await _WebsocketService.SendToPOSBySerial(T.UserId, TA.PosSerial, Model.WebSocketMessageType.TerminalDisconnected, T.Message, T.Id) :
                                                _WebsocketService.SendToTerminalBySerial(TA.TerminalSerial, Model.WebSocketMessageType.POSNotConnected, T.Message, T.Id)
                                            );
                       }
                   }
                   else
                   {
                       T.transactionAffiliates.Where(TA => TA.Affiliation == "Reciver" && TA.PosSerial != null)
                                              .Select(async TA =>
                                                   (TA.PosSerial != null) ?
                                                   await _WebsocketService.SendToPOSBySerial(T.UserId, TA.PosSerial, (Model.WebSocketMessageType)T.Type, T.Message, T.Id) :
                                                   _WebsocketService.SendToTerminalBySerial( TA.TerminalSerial, (Model.WebSocketMessageType)T.Type, T.Message, T.Id)
                                              );

                   }
               });
                await Task.Delay(new TimeSpan(0, 5, 0));
            }
        }
    }
}
