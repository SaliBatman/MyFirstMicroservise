using MyFirstMicroservice.Domain.Core.Bus;
using MyFirstMicroService.Banking.Application.Interfaces;
using MyFirstMicroService.Banking.Application.Models;
using MyFirstMicroService.Banking.Domain.Commands;
using MyFirstMicroService.Banking.Domain.Interfaces;
using MyFirstMicroService.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMicroService.Banking.Application.Services
{
    public class AccountService : IAccountService
    {
        private IAccountRepository _accountRepository;
        private IEventBus _bus;
        public AccountService(IAccountRepository accountRepository, IEventBus bus )
        {
            _accountRepository = accountRepository;
            _bus = bus;
        }
        public async Task<List<Account>> GetAccountsAsync()
        {

            return await _accountRepository.GetAccountsAsync();
        }

        public void TransferAccount(AccountTransferModel model)
        {
            var createTransfer = new CreateTransferCommand(model.FromAccount, model.ToAccount, model.TransferAmount);
            _bus.SendCommmand(createTransfer);
          
        }
    }
}
