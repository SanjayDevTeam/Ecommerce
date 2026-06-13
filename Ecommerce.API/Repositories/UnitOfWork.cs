using Ecommerce.API.Data;
using Ecommerce.API.Entities;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly EcommerceDbContext _context;

    public IGenericRepository<Product> Products { get; }

    public IGenericRepository<User> Users { get; }

    public UnitOfWork(EcommerceDbContext context)
    {
        _context = context;

        Products =
            new GenericRepository<Product>(_context);

        Users =
            new GenericRepository<User>(_context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}