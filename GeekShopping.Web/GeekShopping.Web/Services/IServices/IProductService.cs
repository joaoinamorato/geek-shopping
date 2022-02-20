using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>?> FildAllProductsAsync(string token);
        Task<ProductModel?> FindProductByIdAsync(long id, string token);
        Task<ProductModel?> CreateProductAsync(ProductModel product, string token);
        Task<ProductModel?> UpdateProductAsync(ProductModel product, string token);
        Task<bool> DeleteProductAsync(long id, string token);
    }
}
