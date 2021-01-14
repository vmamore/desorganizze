using Desorganizze.Domain;
using Desorganizze.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Desorganizze.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/{walletId}")]
    public class WalletsController : ControllerBase
    {
        private readonly ISession _session;
        public WalletsController(ISession session)
        {
            _session = session;
        }

        [HttpPost("accounts/{accountId}")]
        public async Task<IActionResult> CreateTransaction(
            [FromRoute] Guid accountId,
            [FromBody] TransactionDto transactionDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var accountPersited = await _session.Query<Account>()
                .FirstAsync(a => a.Id == accountId);

            if (accountPersited == null) return NotFound($"Account not found.");

            var createdTransaction = accountPersited.NewTransaction(transactionDto.Amount, (TransactionType)transactionDto.Type);

            using var transaction = _session.BeginTransaction();
            await _session.SaveOrUpdateAsync(createdTransaction);
            await transaction.CommitAsync();

            return Created($"transactions/{createdTransaction.Id}", new
            {
                accountId = accountPersited.Id,
                accountName = accountPersited.Name.Valor,
                transactionType = transactionDto.Type,
                transactionAmount = createdTransaction.TotalAmount.Amount
            });
        }

        [HttpPost("accounts")]
        public async Task<IActionResult> CreateAccount(
            [FromRoute] string walletId,
            [FromBody] CreateAccountDto createAccountDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var wallet = await _session.Query<Wallet>()
                .FirstAsync(a => a.Id == Guid.Parse(walletId));

            if (wallet == null) return NotFound($"Wallet not found.");

            var newAccount = wallet.NewAccount(createAccountDto.Name);

            using var transaction = _session.BeginTransaction();
            await _session.SaveOrUpdateAsync(newAccount);
            await transaction.CommitAsync();

            return Created(string.Empty, new
            {
                id = newAccount.Id,
                name = newAccount.Name.Valor
            });
        }

        [HttpPut("accounts")]
        public async Task<IActionResult> TransferBetweenAccounts(
            [FromRoute] string walletId,
            [FromBody] TransferBetweenAccountsDto dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var wallet = await _session.Query<Wallet>()
                .FirstAsync(a => a.Id == Guid.Parse(walletId));

            if (wallet == null) return NotFound($"Wallet not found.");

            wallet.TransferBetweenAccounts(dto.RecipientAccountId, dto.SourceAccountId, dto.Amount);

            using var transaction = _session.BeginTransaction();
            await _session.SaveOrUpdateAsync(wallet);
            await transaction.CommitAsync();

            return Ok();
        }

        [HttpGet("accounts")]
        public async Task<IActionResult> GetAllAccountsFromUser(Guid walletId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var allAccountsFromUser = await _session.Query<Account>()
                .Where(acc => acc.Wallet.Id == walletId)
                .Select(x => new AllAccountsFromWalletQueryDto(x.GetBalance.Amount, x.Name.Valor))
                .ToListAsync();

            return Ok(allAccountsFromUser);
        }

        [HttpGet("transactions")]
        public async Task<IActionResult> GetAllTransactionsFromUser(Guid walletId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var transactionsFromAccount = await _session.Query<Transaction>()
                .Where(t => t.Account.Wallet.Id == walletId)
                .Select(x => new TransactionQueryDto(x.TotalAmount.Amount, x.Type, x.CreatedDate, x.Account.Name.Valor))
                .ToListAsync();

            return Ok(transactionsFromAccount);
        }

        [HttpGet("~/wallets/{userId}/user")]
        public async Task<IActionResult> GetWalletFromUser(int userId)
        {
            if (userId == default) return BadRequest();

            var wallet = await _session.Query<Wallet>()
                .Where(wallet => wallet.User.Id == userId)
                .Select(wallet => new GetWalletByUserId
                {
                    WalletId = wallet.Id,
                    Accounts = wallet.Accounts.Select(account => new AccountDto { AccountId = account.Id, Namme = account.Name.Valor })
                })
                .FirstOrDefaultAsync();

            if (wallet is null) return NotFound();

            return Ok(wallet);
        }
    }
}
