using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Service.Interface
{
    interface ITransactionService
    {
         Task CreateTransaction(string UserId, string reciverId, string IssuerId, int IssuerDeviceType, int reciverDeviceType, int transactionType, int? orderId);

         void DeleteTransaction(string TransactionId);

         IEnumerable<Transaction> GetAllTransactions();

        Transaction GetTransactionById(string TransactionId);

        int IncreaseTransactionFailedTries(string transactionId); //integer returned is the current failed tries

        void UpdateTransaction(Transaction model);
    }
}
