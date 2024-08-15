using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using workshop.Data;
using workshop.DTOs.Product;
using workshop.Entities;
using Mapster;
using workshop.Interfaces;
using System.Reflection.Metadata.Ecma335;
//using workshop.Models;

namespace workshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public DatabaseContext DatabaseContext { get; set; }
        public IProductService ProductService { get; }
        public ProductsController(DatabaseContext databaseContext, IProductService productService)
        {
            this.ProductService = productService;
            this.DatabaseContext = databaseContext;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProducts()
        {
            return (await this.ProductService.FindAll()).Select(ProductResponse.FormProduct).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProductById(int id)
        {

            var product = (await this.ProductService.FindById(id));
            if (product == null)
            {
                return NotFound();
            }
            return ProductResponse.FormProduct(product);

        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<ProductResponse>> Search([FromQuery] string name)
        {
            var result = this.DatabaseContext.Products
            .Include(p => p.Category)
            .Where(p => p.Name.ToLower().Contains(name.ToLower()))
            .Select(ProductResponse.FormProduct).ToList();

            return result;
        }

        [HttpPost("")]
        public IActionResult AddProduct([FromForm] ProductRequest productRequest)
        {
            var product = productRequest.Adapt<Product>();
            this.DatabaseContext.Products.Add(product);
            this.DatabaseContext.SaveChanges();
            return StatusCode((int)HttpStatusCode.Created, product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var product = await this.DatabaseContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            this.DatabaseContext.Products.Remove(product);
            await this.DatabaseContext.SaveChangesAsync();
            return NoContent();

        }




    }
}