﻿using Wallet.Domain.Entities;

namespace Wallet.Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User> 
    {
        IQueryable<User> GetAllWithCollections();
        Task<User?> GetUserByEmailAsync(string email);
    }
}
