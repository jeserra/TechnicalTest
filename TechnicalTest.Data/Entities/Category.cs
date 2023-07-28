using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTest.Data.Entities;
public class Category
{
    [Key]
    public int ID { get; set; }
    public string? Name {get;set;}
    public int Code { get; set; }
    public DateTime? Date { get; set; }
    public ICollection<Product> Products { get; set; }
}
