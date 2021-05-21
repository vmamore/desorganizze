namespace Desorganizze.Controllers
{
    using Desorganizze.Application.Commands.Wallets;
    using Desorganizze.Application.Queries.Wallets.Parameters;
    using Desorganizze.Application.Queries.Wallets.ReadModel;
    using Desorganizze.Dtos;
    using Desorganizze.Infra.CQRS.Commands;
    using Desorganizze.Infra.CQRS.Queries;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NHibernate;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Authorize]
    [ApiController]
    [Route("api/[controller]/{walletId}")]
    public class WalletsController : ControllerBase
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly ISession _session;
        private readonly ICommandDispatcher _commandDispatcher;
        public WalletsController(ISession session, IQueryProcessor queryProcessor,
            ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryProcessor = queryProcessor;
            _session = session;
        }

        [HttpPost("accounts/{accountId}/transaction")]
        public async Task<IActionResult> CreateTransaction(
            [FromRoute] Guid accountId,
            [FromBody] TransactionDto transactionDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var resultado = await _commandDispatcher.ExecuteAsync(new CreateTransaction(accountId, transactionDto.Amount, transactionDto.Type));

            if (resultado.Failure && resultado.ReturnDto == null) return NotFound($"Account not found.");

            return Ok();
        }

        [HttpPost("accounts")]
        public async Task<IActionResult> CreateAccount(
            [FromRoute] string walletId,
            [FromBody] CreateAccountDto createAccountDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var resultado = await _commandDispatcher.ExecuteAsync(new CreateAccount(walletId, createAccountDto.Name));

            return Created(string.Empty, resultado.ReturnDto);
        }

        [HttpPut("accounts")]
        public async Task<IActionResult> TransferBetweenAccounts(
            [FromRoute] string walletId,
            [FromBody] TransferBetweenAccountsDto dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var resultado = await _commandDispatcher.ExecuteAsync(new TransferBetweenAccounts(walletId, dto.SourceAccountId, dto.RecipientAccountId, dto.Amount));

            return Created(string.Empty, resultado.ReturnDto);
        }

        [HttpGet("accounts")]
        public async Task<IActionResult> GetAllAccountsFromUser(Guid walletId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _queryProcessor.ExecuteQueryAsync<GetAllAccountsFromUser, IEnumerable<AccountFromWalletDto>>(new GetAllAccountsFromUser(walletId));

            return Ok(result);
        }

        [HttpGet("transactions")]
        public async Task<IActionResult> GetAllTransactionsFromUser(Guid walletId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _queryProcessor.ExecuteQueryAsync<GetTransactionsFromUser, IEnumerable<TransactionQueryDto>>(new GetTransactionsFromUser(walletId));

            return Ok(result);
        }

        [HttpGet("~/wallets/{userId}/user")]
        public async Task<IActionResult> GetWalletFromUser(int userId)
        {
            if (userId == default) return BadRequest();

            var result = await _queryProcessor.ExecuteQueryAsync<GetWalletWithAccountsFromUser, WalletWithAcountDto>(new GetWalletWithAccountsFromUser(userId));

            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpPost("transference")]
        public IActionResult RegisterNewTransference([FromBody] NewTransferDto transferDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            return Ok();
        }
    }
}
