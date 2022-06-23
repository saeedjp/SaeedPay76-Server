using Microsoft.EntityFrameworkCore;
using SaeedPay76.Data.Repositories.Repo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SaeedPay76.Data.Infrastructure
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        UserRepository userRepository { get; }
        void SaveChange();
        Task<int> SaveChangeAsync(CancellationToken cancellationToken);

    }
}
