using Microsoft.EntityFrameworkCore;
using Domain.Products;

namespace Infrastucture.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;

    public ProductRepository(ProductDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(Product product) => _context.Products.Add(product);
    public void Delete(Product product) => _context.Products.Remove(product);
    public void Update(Product product) => _context.Products.Update(product);
    public async Task<bool> ExistsAsync(int id) => await _context.Products.AnyAsync(product => product.Id == id);
    public async Task<Product?> GetProductByIdAsync(int id) => await _context.Products.SingleOrDefaultAsync(c => c.Id == id);
    public async Task<List<Product>> GetAll() => await _context.Products.ToListAsync();
}
    

