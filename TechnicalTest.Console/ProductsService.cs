using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechnicalTest.Data.Entities;

namespace TechnicalTest.Console;

public class ProductsService : IProductsSourceService
{
    public List<Category> GetCategories(string path)
    {
            var records = GetRecords(path);            
            return records.GroupBy(c => c.CATEGORY_CODE).Select(group => group.First()).Select(r=> new Category()
            {
                Code = r.CATEGORY_CODE,
                Name = r.CATEGORY_NAME,
                Date = DateTime.Now
            }).ToList();
    }

    public List<Product> GetProducts(string path)
    {        
        var records = GetRecords(path);
        return
            records.GroupBy(x=> x.PRODUCT_CODE).Select(group => group.First()).Select (r=> new Product
            {
                Code = r.PRODUCT_CODE,
                Name = r.PRODUCT_NAME,
                CategoryCode = r.PRODUCT_CATEGORY_CODE,
                CreationDate = DateTime.Now
            }).ToList();            
    }

    public List<RecordProduct> GetRecords(string path)
    {
        using (var reader = new StreamReader(path))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {            
            return  csv.GetRecords<RecordProduct>().ToList();
        }
    }

    public record RecordProduct
    {
        public int PRODUCT_CODE { get; set; }
        public string? PRODUCT_NAME { get; set; }
        public int PRODUCT_CATEGORY_CODE { get; set; }
        public int CATEGORY_CODE { get; set; }
        public string? CATEGORY_NAME { get; set; }
    }
}