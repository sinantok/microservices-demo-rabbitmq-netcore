using Banking.Domain.Models;
using System.Collections.Generic;

namespace Banking.Domain.Interfaces
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAccounts();
    }
}
