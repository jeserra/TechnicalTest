using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechnicalTest.Data;
using TechnicalTest.Data.Entities;

namespace TechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ProductsContext productsContext;
        public CategoriesController(ProductsContext productsContext)
        {
            this.productsContext = productsContext;
        }

        [HttpGet]
        public IActionResult Get(int? PageNumber = 1, int? PageSize = 10, int? code = 0)
        {
            return Ok( PagedList<Category>.ToPagedList(productsContext.Categories.Where(x=>x.Code == code || code == 0).AsQueryable<Category>(),
                      PageNumber.Value,
                      PageSize.Value));
        }
    }
}
