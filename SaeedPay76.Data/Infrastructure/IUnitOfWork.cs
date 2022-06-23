using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SaeedPay76.Data.Infrastructure
{
    public interface IUnitOfWork<TContext> :IDisposable where TContext : DbContext
    {
        void SaveChange();
        Task<int> SaveChangeAsync(CancellationToken cancellationToken);

    }
}
