using Desorganizze.Dtos;
using Desorganizze.Models;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Desorganizze.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ISession _session;
        public TransactionsController(ISession session)
        {
            _session = session;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionDto transactionDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var accountPersited = await _session.Query<Account>()
                .FirstAsync(a => a.Id == transactionDto.AccountId);

            if (accountPersited == null) return NotFound($"Account not found.");

            var transactionCreated = accountPersited.NewTransaction(transactionDto.Amount, (TransactionType)transactionDto.Type);

            using var transaction = _session.BeginTransaction();
            await _session.SaveOrUpdateAsync(accountPersited);
            await transaction.CommitAsync();

            return Created($"transactions/{transactionCreated.Id}", transactionDto);
        }

        [HttpGet]
        [Route("{walletId}")]
        public async Task<IActionResult> GetAllTransactionsFromUser(Guid walletId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var transactionsFromAccount = await _session.Query<Transaction>()
                .Where(t => t.Account.Wallet.Id == walletId)
                .Select(x => new TransactionQueryDto (x.TotalAmount.Amount, x.Type, x.CreatedDate, x.Account.Name.Valor))
                .ToListAsync();

            return Ok(transactionsFromAccount);
        }
    }
}
