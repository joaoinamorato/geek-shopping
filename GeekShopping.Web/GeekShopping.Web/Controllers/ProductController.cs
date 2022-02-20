using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token") ?? string.Empty;
            var products = await _productService.FildAllProductsAsync(accessToken);
            return View(products);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token") ?? string.Empty;
                var response = await _productService.CreateProductAsync(product, accessToken);

                if (response != null)
                    return RedirectToAction("Index");
            }

            return View(product);
        }

        [Authorize]
        public async Task<IActionResult> Update(long id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token") ?? string.Empty;
            var product = await _productService.FindProductByIdAsync(id, accessToken);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token") ?? string.Empty;
                var response = await _productService.UpdateProductAsync(product, accessToken);

                if (response != null)
                    return RedirectToAction("Index");
            }

            return View(product);
        }

        [Authorize]
        public async Task<IActionResult> Delete(long id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token") ?? string.Empty;
            var product = await _productService.FindProductByIdAsync(id, accessToken);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> Delete(ProductModel product)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token") ?? string.Empty;
            var response = await _productService.DeleteProductAsync(product.Id, accessToken);

            if (response)
                return RedirectToAction("Index");

            return View(product);
        }
    }
}
