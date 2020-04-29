using Microsoft.EntityFrameworkCore;
using MyFirstMicroService.Banking.Data.Context;
using MyFirstMicroService.Banking.Domain.Interfaces;
using MyFirstMicroService.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMicroService.Banking.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private BankingDbContext _ctx;
        public AccountRepository(BankingDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<List<Account>> GetAccountsAsync()
        {
            return await _ctx.Accounts.ToListAsync();
        }
    }
}
