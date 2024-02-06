namespace Domain.Users;

public interface IUserRepository{
    Task<User?> GetByIdAsync(int id);
    Task<List<User>> GetAll();
    Task<bool> ExistsAsync(int id);
    void Add(User user);
    void Update(User user);
    void Delete(User user);
}