using Microsoft.EntityFrameworkCore;
using SaeedPay76.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaeedPay76.Data.DatabaseContext
{
    public class SaeedPayDbContext : DbContext
    {
        public SaeedPayDbContext( DbContextOptions options) : base(options)
        {
        }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<BankCard> BankCards { get; set; }

    }
}
