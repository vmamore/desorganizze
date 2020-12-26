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
    public class WalletsController : ControllerBase
    {
        private readonly ISession _session;
        public WalletsController(ISession session)
        {
            _session = session;
        }


        [HttpGet]
        [Route("{walletId}/accounts")]
        public async Task<IActionResult> GetAllAccountsFromUser(Guid walletId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var allAccountsFromUser = await _session.Query<Account>()
                .Where(acc => acc.Wallet.Id == walletId)
                .Select(x => new AllAccountsFromWalletQueryDto(x.GetBalance.Amount, x.Name.Valor))
                .ToListAsync();

            return Ok(allAccountsFromUser);
        }
    }
}
