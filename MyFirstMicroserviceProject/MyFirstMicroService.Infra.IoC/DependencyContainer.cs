using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MyFirstMicroservice.Domain.Core.Bus;
using MyFirstMicroservice.Infra.Bus;
using MyFirstMicroService.Banking.Application.Interfaces;
using MyFirstMicroService.Banking.Application.Services;
using MyFirstMicroService.Banking.Data.Context;
using MyFirstMicroService.Banking.Data.Repository;
using MyFirstMicroService.Banking.Domain.CommandHanlers;
using MyFirstMicroService.Banking.Domain.Commands;
using MyFirstMicroService.Banking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFirstMicroService.Infra.IoC
{
   public class DependencyContainer
    {

        public static void Register(IServiceCollection service) 
        {
            // bus register
            service.AddTransient<IEventBus , RabbitMqBus>();

            // bankingCommand
            service.AddTransient<IRequestHandler<CreateTransferCommand,bool> , CommandTransferHandler>();

            // application register

            service.AddTransient<IAccountService, AccountService>();


            // domain register 

            service.AddTransient<IAccountRepository, AccountRepository>();


            service.AddTransient<BankingDbContext>();
        }

    }
}
