using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort);
    Task<Product?> GetProductByIdAsync(int id);
    Task<IReadOnlyList<string>> GetProductByBrandsAsync();
    Task<IReadOnlyList<string>> GetProductByTypesAsync();
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
    bool ProductExist(int id);
    Task<bool> SaveChangesAsync();
}
