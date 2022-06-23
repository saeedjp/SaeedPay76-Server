﻿using Microsoft.EntityFrameworkCore;
using SaeedPay76.Data.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SaeedPay76.Data.Infrastructure
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext, new()
    {
        private readonly SaeedPayDbContext _dbContext;

        public UnitOfWork(SaeedPayDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveChange()
        {
            _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangeAsync(CancellationToken cancellationToken)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync(CancellationToken.None);
            try
            {
                var result = await _dbContext.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(CancellationToken.None);

                return result;
            }

            catch (Exception ex)
            {
                await transaction.RollbackAsync(CancellationToken.None);

                throw new Exception(ex.Message);
            }
        }

        private bool disposed = false;
        protected virtual  void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;

        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
         ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}