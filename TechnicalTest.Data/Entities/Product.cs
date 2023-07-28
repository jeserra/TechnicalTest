using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTest.Data.Entities;
public class Product
{
    [Key]
    public int ID { get; set; }
    public string? Name { get; set; }
    public int Code { get; set; }
    public int CategoryCode { get; set; }
    public virtual Category? Category { get; set; }
    public DateTime CreationDate { get; set; }
}
