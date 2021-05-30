using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Data.Inteface
{
    public interface ITransactionRepo
    {
        public const string POS = "POS";
        public const string TERMINAL = "Terminal";

        Task<Transaction> CreateTransaction(Transaction transaction);

        Task<bool> AddTransactionAffiliate(TransactionAffiliates model);

        bool DeleteTransaction(Transaction model);

        bool DeleteTransactionAffiliate(TransactionAffiliates model);

        IEnumerable<Transaction> getAllTransactions();

        IEnumerable<TransactionAffiliates> GetTransactionAffiliatesByTransactionId(string TransactionId);
        Transaction GetTransactionById(string TransactionId);

        bool UpdateTransaction(Transaction transaction);

        Task SaveChanges();
    }
}
