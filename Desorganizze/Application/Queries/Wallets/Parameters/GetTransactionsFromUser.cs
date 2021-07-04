using Desorganizze.Infra.CQRS.Queries;
using System;

namespace Desorganizze.Application.Queries.Wallets.Parameters
{
    public class GetTransactionsFromUser : IQuery
    {
        public GetTransactionsFromUser(Guid walletId) => WalletId = walletId;

        public Guid WalletId { get; }
    }
}
