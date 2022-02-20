using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/product";

        public ProductService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<ProductModel?> CreateProductAsync(ProductModel product, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJson(BasePath, product);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong when calling the API");

            return await response.ReadContentAsAsync<ProductModel>();
        }

        public async Task<bool> DeleteProductAsync(long id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _client.DeleteAsync($"{BasePath}/{id}");
            return await response.ReadContentAsAsync<bool>();
        }

        public async Task<IEnumerable<ProductModel>?> FildAllProductsAsync(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(BasePath);
            return await response.ReadContentAsAsync<List<ProductModel>>();
        }

        public async Task<ProductModel?> FindProductByIdAsync(long id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"{BasePath}/{id}");
            return await response.ReadContentAsAsync<ProductModel>();
        }

        public async Task<ProductModel?> UpdateProductAsync(ProductModel product, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PutAsJson(BasePath, product);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong when calling the API");

            return await response.ReadContentAsAsync<ProductModel>();
        }
    }
}
