using Banking.Data.Context;
using Banking.Domain.Interfaces;
using Banking.Domain.Models;
using System.Collections.Generic;

namespace Banking.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankingDbContext _context;
        public AccountRepository(BankingDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Account> GetAccounts()
        {
            return _context.Accounts;
        }
    }
}
