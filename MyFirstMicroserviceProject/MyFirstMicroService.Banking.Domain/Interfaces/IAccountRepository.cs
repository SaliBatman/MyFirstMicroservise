using MyFirstMicroService.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMicroService.Banking.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAccountsAsync();
    }
}
