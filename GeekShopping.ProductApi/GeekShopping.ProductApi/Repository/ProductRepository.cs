using AutoMapper;
using GeekShopping.ProductApi.Data.ValueObjects;
using GeekShopping.ProductApi.Model;
using GeekShopping.ProductApi.Model.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GeekShopping.ProductApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbSet<Product> _dbProduct;
        private readonly SqlContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(SqlContext context, IMapper mapper)
        {
            _context = context;
            _dbProduct = _context.Products ?? throw new ArgumentNullException(nameof(_context.Products));
            _mapper = mapper;
        }

        public async Task<ProductVO> CreateAsync(ProductVO vo)
        {
            var product = _mapper.Map<Product>(vo);
            _context.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            try
            {
                var product = await _dbProduct.FirstOrDefaultAsync(f => f.Id == id);

                if (product == null)
                    return false;

                _dbProduct.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<ProductVO>> FindAllAsync()
        {
            var products = await _dbProduct.ToListAsync();
            return _mapper.Map<List<ProductVO>>(products);
        }

        public async Task<ProductVO?> FindByIdAsync(long id)
        {
            var product = await _dbProduct.FirstOrDefaultAsync(f => f.Id == id);
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<ProductVO> UpdateAsync(ProductVO vo)
        {
            var product = _mapper.Map<Product>(vo);
            _context.Update(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVO>(product);
        }
    }
}
