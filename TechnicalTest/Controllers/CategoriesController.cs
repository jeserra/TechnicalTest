using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Get(bool sortAsc = true, string SortBy = "Code", int? PageNumber = 1, int? PageSize = 10, int? code = 0)
        {

            var result = sortAsc ? SortByAscending(productsContext.Categories
                          .Where(x => x.Code == code || code  == 0)                          
                          .AsNoTracking()
                          .ToList(), SortBy) :
                          SortByDescending(productsContext.Categories
                          .Where(x => x.Code == code || code  == 0)
                          .AsNoTracking()
                          .ToList(), SortBy);

            return Ok( PagedList<Category>.ToPagedList(result,
                      PageNumber.Value,
                      PageSize.Value));
        }

        public static IQueryable<Category> SortByDescending(List<Category> categories, string sortBy)
        {
            switch (sortBy)
            {
                case "Code":
                    return categories.OrderByDescending(c => c.Code).AsQueryable();
                case "Name":
                    return categories.OrderByDescending(c => c.Name).AsQueryable();
                // Add more cases for other properties if needed
                default:
                    return categories.OrderByDescending(c => c.ID).AsQueryable();
            }
        }

        public static IQueryable<Category> SortByAscending(List<Category> categories, string sortBy)
        {
            switch (sortBy)
            {
                case "Code":
                    return categories.OrderBy(c => c.Code).AsQueryable();
                case "Name":
                    return categories.OrderBy(c => c.Name).AsQueryable();
                // Add more cases for other properties if needed
                default:
                    return categories.OrderBy(c => c.ID).AsQueryable();
            }
        }
    }
}
