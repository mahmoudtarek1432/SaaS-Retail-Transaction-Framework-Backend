using BackAgain.Dto;
using BackAgain.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService TransactionService)
        {
            _transactionService = TransactionService;
        }

        [HttpDelete("{TransactionId}")]
        public ActionResult<ClientResponseManager> EndTransaction(string TransactionId)
        {
            if (ModelState.IsValid)
            {
                _transactionService.DeleteTransaction(TransactionId);
                return Ok(new ClientResponseManager
                {
                    IsSuccessfull = true,
                    Message = "transaction Ended Successfully"
                });
            }
            return BadRequest(new ClientResponseManager
            {
                IsSuccessfull = false,
                Message = "Model not valid"
            });
        }
    }
}
