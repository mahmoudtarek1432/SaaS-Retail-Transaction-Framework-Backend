using BackAgain.Data.Inteface;
using BackAgain.Model;
using BackAgain.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Service.Implementation
{
    public class TransactionService : ITransactionService
    {
        public const int POS = 1;
        public const int TERMINAL = 2;
        private readonly ITransactionRepo _TransactionRepo;

        public TransactionService(ITransactionRepo TransactionRepo)
        {
            _TransactionRepo = TransactionRepo;
        }

        public async Task<Transaction> CreateTransaction(string UserId, string reciverId, string IssuerId, int IssuerDeviceType, int reciverDeviceType, int transactionType, string orderId)
        {
            var transaction = new Transaction
            {
                State = 1,
                FailedTries = 0,
                Id = Guid.NewGuid().ToString(),
                Type = transactionType,
                UserId = UserId,
            };

            var CommitedTransaction = await _TransactionRepo.CreateTransaction(transaction);
            await _TransactionRepo.SaveChanges();

            if(CommitedTransaction != null)
            {
                var issuer = new TransactionAffiliates
                {
                    Affiliation = "Issuer",
                    PosSerial = (IssuerDeviceType == POS) ? IssuerId : null,
                    TerminalSerial = (IssuerDeviceType == TERMINAL) ? IssuerId : null,
                    TransactionID = CommitedTransaction.Id,
                };

                var reciver = new TransactionAffiliates
                {
                    Affiliation = "Reciver",
                    PosSerial = (reciverDeviceType == POS) ? reciverId : null,
                    TerminalSerial = (reciverDeviceType == TERMINAL) ? reciverId : null,
                    TransactionID = CommitedTransaction.Id
                };
               
                await _TransactionRepo.AddTransactionAffiliate(reciver);
                await _TransactionRepo.AddTransactionAffiliate(issuer);

                if(orderId != null)
                {
                    var orderTrans = new OrderTransaction
                    {
                        OrderId = orderId,
                        TransactionID = CommitedTransaction.Id
                    };
                    _TransactionRepo.addOrderTransaction(orderTrans);
                }

                await _TransactionRepo.SaveChanges();

                //add orderId Later
                
            }
            return CommitedTransaction;
        }

        public void DeleteTransaction(string TransactionId)
        {
            var Transaction = _TransactionRepo.GetTransactionById(TransactionId);
            var Affiliates = _TransactionRepo.GetTransactionAffiliatesByTransactionId(TransactionId);
            Affiliates.ToList().ForEach(TA => _TransactionRepo.DeleteTransactionAffiliate(TA));
            _TransactionRepo.DeleteTransaction(Transaction);
            _TransactionRepo.SaveChanges();
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            var transactions = _TransactionRepo.getAllTransactions();
            var fullTransactions = transactions.Select(AddAffiliates).Select(AddOrderTransaction);
            return fullTransactions;
        }

        public Transaction GetTransactionById(string TransactionId)
        {
            return _TransactionRepo.GetTransactionById(TransactionId);
        }

        public int IncreaseTransactionFailedTries(string transactionId) //integer returned is the current failed tries
        {
            var transaction = GetTransactionById(transactionId);
            transaction.FailedTries += 1;
            UpdateTransaction(transaction);
            return transaction.FailedTries;
        }

        public void UpdateTransaction(Transaction model)
        {
            var transaction = _TransactionRepo.GetTransactionById(model.Id);
            transaction.State = model.State;
            transaction.FailedTries = model.FailedTries;

            _TransactionRepo.UpdateTransaction( transaction);
            _TransactionRepo.SaveChanges();
        }

        Transaction AddAffiliates(Transaction T)
        {
            T.transactionAffiliates = _TransactionRepo.GetTransactionAffiliatesByTransactionId(T.Id);
            return T;
        }

        Transaction AddOrderTransaction(Transaction T)
        {
            var order = _TransactionRepo.GetOrderTransaction(T.Id);
            if(order != null)
            {
                T.OrderTransaction = order;
            }
            return T;
        }
    }
}
