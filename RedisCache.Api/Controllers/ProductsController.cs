using Microsoft.AspNetCore.Mvc;
using RedisCache.Api.Models;
using RedisCache.Api.Services.CacheService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisCache.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        private List<Product> _products;
        private ICacheService _cacheService;
        public ProductsController(ICacheService cacheService)
        {
            _cacheService = cacheService;
            CreateProducts();
        }

        [HttpGet("/createproducts")]
        public IActionResult CreateProductList()
        {
            _cacheService.Add("products4",_products,10);
            return Ok();
        }


        [HttpGet]
        public IActionResult Get()
        {
            var cacheData = _cacheService.Get<List<Product>>("products4");
            if (cacheData == null)
            {
                return BadRequest("cache data is null");
            }
            return Ok(cacheData);
        }


        private void CreateProducts()
        {
            if (_products == null)
            {
                _products = new List<Product>
            {
                new Product{ Id=1, Category = "Computers", Name = "Macbook", UnitPrice = 1200, UnitsInStock = 50 },
                new Product{ Id=2, Category = "Electronics", Name = "Camera", UnitPrice = 1700, UnitsInStock = 5 },
                new Product{ Id=3, Category = "Books", Name = "Romeo&Juliet", UnitPrice = 10, UnitsInStock = 80 },
                new Product{ Id=4, Category = "Gift Cards", Name = "Google Play Store Gift 50$ Card", UnitPrice = 49, UnitsInStock = 750 },
                new Product{ Id=5, Category = "Cell Phones", Name = "Iphone 12 Pro", UnitPrice = 1000, UnitsInStock = 90 },

            };
            }
        }
    }



}
