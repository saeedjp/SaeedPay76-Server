using SaeedPay76.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SaeedPay76.Services.Site.Admin.Auth.Interface
{
    public interface IUserService
    {
        Task<UserEntity> GetUserForPassChange(string id, string passWord, CancellationToken cancellationToken);
        Task<bool> UpdateUserPass(UserEntity user, string passWord, CancellationToken cancellationToken);
    }
}
