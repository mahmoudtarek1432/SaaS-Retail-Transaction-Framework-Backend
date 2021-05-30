using BackAgain.Data.Inteface;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Data.Implementation
{
    public class TransactionRepo : ITransactionRepo
    {
        const string POS = "POS";
        const string TERMINAL = "Terminal";


        private readonly ProjContext _ctx;

        public TransactionRepo(ProjContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Transaction> CreateTransaction(Transaction transaction)
        {
            try
            {
                var trans = (await _ctx._Transaction.AddAsync(transaction)).Entity;
                return trans;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddTransactionAffiliate(TransactionAffiliates model)
        {
            try
            {
                await _ctx._TransactionAffiliate.AddAsync(model);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool DeleteTransaction(Transaction model)
        {
            try
            {
                _ctx._Transaction.Remove(model);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool DeleteTransactionAffiliate(TransactionAffiliates model)
        {
            try
            {
                _ctx._TransactionAffiliate.Remove(model);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public IEnumerable<Transaction> getAllTransactions()
        {
            return _ctx._Transaction;
        }

        public IEnumerable<TransactionAffiliates> GetTransactionAffiliatesByTransactionId(string TransactionId)
        {
            return _ctx._TransactionAffiliate.Where(TA => TA.TransactionID == TransactionId);
        }

        public Transaction GetTransactionById(string TransactionId)
        {
            return _ctx._Transaction.Where(t => t.Id == TransactionId).FirstOrDefault();
        }

        public bool UpdateTransaction( Transaction transaction)
        {
            try
            {
                _ctx._Transaction.Update(transaction);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task SaveChanges()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
