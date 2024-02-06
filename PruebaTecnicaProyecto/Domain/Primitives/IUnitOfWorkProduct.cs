namespace Domain.Primitives;

public interface IUnitOfWorkProduct{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}