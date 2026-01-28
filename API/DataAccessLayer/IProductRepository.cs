using API.Model;

namespace API.DataAccessLayer
{
    // Repositories/IUserRepository.cs
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product user);
        void DeleteProduct(int id);
    }

}

