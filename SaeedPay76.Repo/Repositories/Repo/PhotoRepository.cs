using Microsoft.EntityFrameworkCore;
using SaeedPay76.Data.DatabaseContext;
using SaeedPay76.Data.Infrastructure;
using SaeedPay76.Data.Models;
using SaeedPay76.Infrastructure.Repositories.Interface;

namespace SaeedPay76.Infrastructure.Repositories.Repo
{
    public class PhotoRepository : Repository<Photo>, IPhotoRepository
    {
        private readonly DbContext _db;
        public PhotoRepository(DbContext db) : base(db)
        {
            _db = (_db ?? (SaeedPayDbContext)_db);
        }
    }
}
