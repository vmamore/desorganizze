using Desorganizze.Infra.CQRS.Queries;
using System;

namespace Desorganizze.Application.Queries.Wallets.Parameters
{
    public class GetAllAccountsFromUser : IQuery
    {
        public GetAllAccountsFromUser(Guid walletId)
        {
            WalletId = walletId;
        }

        public Guid WalletId { get; }
    }
}
