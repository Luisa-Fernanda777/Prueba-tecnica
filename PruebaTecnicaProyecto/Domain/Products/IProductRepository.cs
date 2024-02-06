namespace Domain.Products;

public interface IProductRepository{
    Task<Product?> GetProductByIdAsync (int id);
    Task<List<Product>> GetAll();
    Task<bool> ExistsAsync(int id);
    void Add(Product Product);
    void Update(Product Product);
    void Delete(Product Product);
}