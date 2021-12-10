using Icarus.Model;
using Icarus.Model.Product;
using Icarus.Service.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Icarus.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }

        [HttpGet]
        public General<ListDeleteViewModel> GetProducts()
        {
            return productService.GetProducts();
        }

        [HttpPost]
        public General<ProductViewModel> Insert([FromBody] ProductViewModel newProduct)
        {
            return productService.Insert(newProduct);
        }

        [HttpPut("{id}")]
        public General<ProductViewModel> Update(int id, [FromBody] ProductViewModel product)
        {
            return productService.Update(id, product);
        }

        [HttpDelete("{id}")]
        public General<ListDeleteViewModel> Delete(int id)
        {
            return productService.Delete(id);
        }
    }
}
