using SaeedPay76.Common.Helper;
using SaeedPay76.Data.DatabaseContext;
using SaeedPay76.Data.Infrastructure;
using SaeedPay76.Data.Models;
using SaeedPay76.Services.Auth.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SaeedPay76.Services.Auth.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _db;

        public AuthService(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task<UserEntity> Login(string userName, string passWord, CancellationToken cancellationToken)
        {
            var user = await _db.userRepository.GetAsync(u => u.UserName == userName);
            if (user == null)
            {
                return null;
            }
            if (!(Utilities.VerifyPasswordHash(passWord, user.PasswordHash, user.PasswordSalt)))
            {
                return null;
            }
            return user;
        }
        public async Task<UserEntity> Register(UserEntity user, string passWord , CancellationToken cancellationToken)
        {
            byte[] passWordHash, passWordSalt;
            Utilities.CreatePassWordHash(passWord, out passWordHash, out passWordSalt);
            user.PasswordHash = passWordHash;
            user.PasswordSalt = passWordSalt;
            await _db.userRepository.InsertAsync(user);
            await _db.SaveChangeAsync(cancellationToken: cancellationToken);
            return user;
        }

       

    }
}
