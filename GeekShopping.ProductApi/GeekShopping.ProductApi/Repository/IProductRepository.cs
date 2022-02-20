using GeekShopping.ProductApi.Data.ValueObjects;

namespace GeekShopping.ProductApi.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductVO>> FindAllAsync();
        Task<ProductVO?> FindByIdAsync(long id);
        Task<ProductVO> CreateAsync(ProductVO vo);
        Task<ProductVO> UpdateAsync(ProductVO vo);
        Task<bool> DeleteAsync(long id);
    }
}
