using TechnicalTest.Data.Entities;

namespace TechnicalTest.Console
{
    public interface IProductsSourceService
    {
        public List<Category> GetCategories(string path);
        public List<Product> GetProducts(string path);
    }
}