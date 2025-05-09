﻿using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Interfaces;
using Wallet.Infrastructure.Data.Context;

namespace Wallet.Infrastructure.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly WalletDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public GenericRepository(WalletDbContext db)
        {
            Db = db;
            DbSet = Db.Set<TEntity>();
        }

        public void Add(TEntity obj)
        {
            DbSet.Entry(obj).State = EntityState.Added;
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public TEntity GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public void Remove(Guid id)
        {
            DbSet.Entry(DbSet.Find(id)).State = EntityState.Deleted;
        }

        public virtual Task<int> SaveChangesAsync()
        {
            return Db.SaveChangesAsync();
        }

        public virtual void Update(TEntity obj)
        {
            DbSet.Entry(obj).State = EntityState.Modified;
        }
    }
}
