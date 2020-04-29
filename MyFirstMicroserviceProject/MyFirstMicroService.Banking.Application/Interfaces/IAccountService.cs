using MyFirstMicroService.Banking.Application.Models;
using MyFirstMicroService.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMicroService.Banking.Application.Interfaces
{
    public interface IAccountService
    {
        Task<List<Account>> GetAccountsAsync();
        void TransferAccount(AccountTransferModel model);
    }
}
