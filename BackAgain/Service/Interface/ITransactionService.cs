using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Service.Interface
{
    public interface ITransactionService
    {
        /// <summary>
        /// This function creates a new transaction, A transaction is an open relation between two devices through websocket connection
        /// Device type 1 == POS, 
        /// Device type 2 == Terminal
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="reciverId"></param>
        /// <param name="IssuerId"></param>
        /// <param name="IssuerDeviceType"></param>
        /// <param name="reciverDeviceType"></param>
        /// <param name="transactionType"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<Transaction> CreateTransaction(string UserId, string reciverId, string IssuerId, int IssuerDeviceType, int reciverDeviceType, int transactionType, string orderId);

        void DeleteTransaction(string TransactionId);
         
        IEnumerable<Transaction> GetAllTransactions();
        
        Transaction GetTransactionById(string TransactionId);

        int IncreaseTransactionFailedTries(string transactionId); //integer returned is the current failed tries

        void UpdateTransaction(Transaction model);
    }
}
