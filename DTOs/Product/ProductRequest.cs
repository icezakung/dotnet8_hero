using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace workshop.DTOs.Product
{
    public class ProductRequest
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Name, Maxinum length 10")]
        public string Name { get; set; }
        [Range(0, 100, ErrorMessage = "Stock, Range must between 0 - 100")]
        public int Stock { get; set; }
         [Range(0, 1000, ErrorMessage = "Stock, Range must between 0 - 1000")]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public List<IFormFile>? FormFiles { get; set; }
    }

}
