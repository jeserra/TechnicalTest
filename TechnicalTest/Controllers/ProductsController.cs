using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechnicalTest.Data;
using TechnicalTest.Data.Entities;

namespace TechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private ProductsContext productsContext;
        public ProductsController(ProductsContext productsContext)
        {
            this.productsContext = productsContext;
        }

        [HttpGet]
        public IActionResult Get(int? PageNumber = 1, int? PageSize = 10, int? CategoryCode = 0, int? code = 0)
        {
            return Ok(PagedList<Product>.ToPagedList(productsContext.Products
                            .Where(x => x.Code == code || code  == 0)
                            .Where(x => x.CategoryCode == CategoryCode || CategoryCode == 0)
                            .AsQueryable<Product>()
                            .AsNoTracking(),
                      PageNumber.Value,
                      PageSize.Value));
        }
    }
}
