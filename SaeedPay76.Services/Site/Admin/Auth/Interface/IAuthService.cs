using SaeedPay76.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SaeedPay76.Services.Auth.Interface
{
    public interface IAuthService
    {
        Task<UserEntity> Register(UserEntity user, string passWord , CancellationToken cancellationToken);
        Task<UserEntity> Login(string userName, string passWord , CancellationToken cancellationToken);
    }
}
