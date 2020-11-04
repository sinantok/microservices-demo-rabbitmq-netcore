using Banking.Application.Models;
using Banking.Domain.Models;
using System.Collections.Generic;

namespace Banking.Application.Interfaces
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAccounts();
        void Transfer(AccountTransfer accountTransfer);
    }
}
