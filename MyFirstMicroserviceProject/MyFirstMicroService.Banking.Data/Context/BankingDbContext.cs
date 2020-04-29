using Microsoft.EntityFrameworkCore;
using MyFirstMicroService.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFirstMicroService.Banking.Data.Context
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options)
        {

        }
        public DbSet<Account> Accounts { get; set; }
    }
}
