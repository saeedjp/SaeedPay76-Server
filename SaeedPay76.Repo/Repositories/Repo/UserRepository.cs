using Microsoft.EntityFrameworkCore;
using SaeedPay76.Common.Helper;
using SaeedPay76.Data.DatabaseContext;
using SaeedPay76.Data.Infrastructure;
using SaeedPay76.Data.Models;
using SaeedPay76.Data.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaeedPay76.Data.Repositories.Repo
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        private readonly DbContext _db;
        public UserRepository(DbContext db) : base(db)
        {
            _db = (_db ?? (SaeedPayDbContext)_db);
        }

        public async Task<bool> UserExist(string userName)
        {
            if (await GetAsync(u=>u.UserName == userName) == null)
            {
                return false;
            }
            return true;
        }
    }
}
