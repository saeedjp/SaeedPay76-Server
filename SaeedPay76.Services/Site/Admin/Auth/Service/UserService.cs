using SaeedPay76.Common.Helper;
using SaeedPay76.Data.Infrastructure;
using SaeedPay76.Data.Models;
using SaeedPay76.Services.Site.Admin.Auth.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SaeedPay76.Services.Site.Admin.Auth.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _db;

        public UserService(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task<UserEntity> GetUserForPassChange(string id, string passWord, CancellationToken cancellationToken)
        {
            var user = await _db.userRepository.GetByIdAsync(id);
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

        public async Task<bool> UpdateUserPass(UserEntity user, string passWord, CancellationToken cancellationToken)
        {
            byte[] passWordHash, passWordSalt;
            Utilities.CreatePassWordHash(passWord, out passWordHash, out passWordSalt);
            user.PasswordHash = passWordHash;
            user.PasswordSalt = passWordSalt;

            _db.userRepository.Update(user);

            if (await _db.SaveChangeAsync(cancellationToken: cancellationToken) > 0)
            {
                return true;
            }
            else
            {
                return false;

            }

        }
    }
}
