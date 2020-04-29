using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyFirstMicroService.Banking.Api.Contract;
using MyFirstMicroService.Banking.Application.Interfaces;
using MyFirstMicroService.Banking.Application.Models;
using MyFirstMicroService.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstMicroService.Banking.Api.Controllers
{
    [ApiController]
    [Route(ApiRoutes.Base + ApiRoutes.Banking.Base)]
    public class BankingController : ControllerBase
    {
        private readonly IAccountService _accountService;
     
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<BankingController> _logger;

        public BankingController(ILogger<BankingController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;

        }

        [HttpGet]
        public async Task<List<Account>> Get()
        {
            return await _accountService.GetAccountsAsync();
        }
        [HttpPost]
        public async Task<IActionResult> Post(AccountTransferModel model)
        {
            _accountService.TransferAccount(model);
            return Ok(model);
        }
    }
}
