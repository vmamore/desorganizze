using Desorganizze.Infra.CQRS.Queries;
using System;

namespace Desorganizze.Application.Queries.Wallets.Parameters
{
    public class GetWalletWithAccountsFromUser : IQuery
    {
        public GetWalletWithAccountsFromUser(int userId) => UserId = userId;

        public int UserId { get; }
    }
}
