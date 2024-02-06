using System.Diagnostics.CodeAnalysis;
using Domain.Organizations;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Persistence.Repositories;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly ApplicationDbContext _context;

    public OrganizationRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(Organization organization) => _context.Organizations.Add(organization);
    public void Delete(Organization organization) => _context.Organizations.Remove(organization);
    public void Update(Organization organization) => _context.Organizations.Update(organization);
    public async Task<bool> ExistsAsync(int id) => await _context.Organizations.AnyAsync(organization => organization.Id == id);
    public async Task<List<Organization>> GetAll() => await _context.Organizations.ToListAsync();
    public async Task<Organization?> GetByIdAsync(int id) => await _context.Organizations.SingleOrDefaultAsync(c => c.Id == id);

}