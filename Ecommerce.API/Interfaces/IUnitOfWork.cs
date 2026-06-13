using Ecommerce.API.Entities;
using Ecommerce.API.Data;
using Ecommerce.API.Interfaces;

public interface IUnitOfWork
{
    IGenericRepository<Product> Products { get; }

    IGenericRepository<User> Users { get; }

    Task<int> SaveChangesAsync();
}