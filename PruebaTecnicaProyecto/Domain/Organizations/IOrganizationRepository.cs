namespace Domain.Organizations;

public interface IOrganizationRepository{
    Task<Organization?> GetByIdAsync(int id);
    Task<List<Organization>> GetAll();
    Task<bool> ExistsAsync(int id);
    void Add(Organization organization);
    void Update(Organization organization);
    void Delete(Organization organization);
    
}