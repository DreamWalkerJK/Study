using APIStudy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace APIStudy.Controllers
{
    public class ProductsController : ApiController
    {
        Product[] products = new Product[]
        {
            new Product
            {
                Id = 1,
                Name = "HP Laptop",
                Category = "Computer",
                Price = 4999
            },
            new Product
            {
                Id = 2,
                Name = "Honor 30s",
                Category = "Mobile",
                Price = 2399
            },
            new Product
            {
                Id = 3,
                Name = "C++",
                Category = "Book",
                Price = 75.99M
            }
        };

        /// <summary>
        /// get all products.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        /// <summary>
        /// get product by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        public IEnumerable<Product> GetProductByCategory(string category)
        {
            return products.Where(
                (p) => string.Equals(p.Category, category,
                StringComparison.OrdinalIgnoreCase));
        }
    }
}