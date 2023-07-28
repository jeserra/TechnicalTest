using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTest.Data;
using TechnicalTest.Data.Entities;

namespace TechnicalTest.Console;

public class ImportService
{
    private ProductsContext _productsContext;
    private IProductsSourceService _productsSourceService;
    public ImportService(ProductsContext context, IProductsSourceService productsSource) 
    {
        _productsContext = context;
        _productsSourceService = productsSource;
    }

    public int ImportData (string path)
    {
        var categories = _productsSourceService.GetCategories(path);
        _productsContext.BulkInsert(categories, options => {
            options.InsertIfNotExists = true;
            options.ColumnPrimaryKeyExpression = category => category.Code;
        });

        var products = _productsSourceService.GetProducts(path);
        _productsContext.BulkInsert(products, options =>
        {
            options.InsertIfNotExists = true;
            options.ColumnPrimaryKeyExpression = products => products.Code;
        });

        return _productsContext.Products
            .Include(x => x.Category)
            .Where(p => p.Category != null).Count();

    }
}
