using Microsoft.EntityFrameworkCore;
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
    }
}
