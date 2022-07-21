using Microsoft.EntityFrameworkCore;
using SaeedPay76.Data.Repositories.Interface;
using SaeedPay76.Data.Repositories.Repo;
using SaeedPay76.Infrastructure.Repositories.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SaeedPay76.Data.Infrastructure
{
    public interface IUnitOfWork: IDisposable 
    {
        IUserRepository userRepository { get; }
        IPhotoRepository photoRepository { get; }
        void SaveChange();
        Task<int> SaveChangeAsync(CancellationToken cancellationToken);

    }
}
