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
//using workshop.Models;

namespace workshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public DatabaseContext DatabaseContext { get; }
        public ProductsController(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<ProductResponse>> GetProducts()
        {
            var products = this.DatabaseContext.Products
            .Include(p => p.Category)
            .Select(ProductResponse.FormProduct)
            .ToList();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            // var product = this.DatabaseContext.Products.Find(id);
            // return Ok(product);

            // var selectProduct = this.DatabaseContext.Products.Find(id);
            // if (selectProduct != null)
            // {
            //     return Ok(ProductResponse.FormProduct(selectProduct));
            // }
            // return NotFound();

            var selectProduct = this.DatabaseContext.Products
            .Include(p => p.Category)
            .Select(ProductResponse.FormProduct)
            .Where(p => p.ProductId == id).FirstOrDefault();

            return Ok(selectProduct);
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

 
        


    }
}