using System.Collections;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext contex;

        public ProductsController(StoreContext contex)
        {
            this.contex = contex;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await contex.Products.ToListAsync();
        }

        [HttpGet("{id:int}")] // api/products/2
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await contex.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            contex.Products.Add(product);

            await contex.SaveChangesAsync();

            return product;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (product.Id != id || !ProductExists(id))
                return BadRequest("Cannot update this product");

                contex.Entry(product).State = EntityState.Modified;
                await contex.SaveChangesAsync();

                return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await contex.Products.FindAsync(id);

            if(product == null)
            return NoContent();

            contex.Products.Remove(product);

            await contex.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return contex.Products.Any(x => x.Id == id);
        }

    }
}
