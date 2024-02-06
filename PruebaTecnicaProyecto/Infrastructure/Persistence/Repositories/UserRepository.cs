using System.Diagnostics.CodeAnalysis;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(User user) => _context.Users.Add(user);
    public void Delete(User user) => _context.Users.Remove(user);
    public void Update(User user) => _context.Users.Update(user);
    public async Task<bool> ExistsAsync(int id) => await _context.Users.AnyAsync(user => user.Id == id);
    public async Task<List<User>> GetAll() => await _context.Users.ToListAsync();
    public async Task<User?> GetByIdAsync(int id) => await _context.Users.SingleOrDefaultAsync(c => c.Id == id);

}
