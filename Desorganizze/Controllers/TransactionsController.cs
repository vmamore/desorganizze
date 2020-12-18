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
        [Route("{accountId}")]
        public async Task<IActionResult> GetAllTransactionsFromAccount(Guid accountId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var transactionsFromAccount = await _session.Query<Transaction>()
                .Where(t => t.Account.Id == accountId)
                .Select(x => new TransactionQueryDto (x.TotalAmount.Amount, x.Type, x.CreatedDate))
                .ToListAsync();

            return Ok(transactionsFromAccount);
        }
    }
}
