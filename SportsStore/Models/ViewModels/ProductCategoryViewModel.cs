using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.ViewModels
{
    public class ProductCategoryViewModel
    {
        //[Required(ErrorMessage = "You must provide product name"), MinLength(3), MaxLength(40)]
        //public string ProductName { get; set; }

        //[MaxLength(20, ErrorMessage = "Maximum of 20 characters"), MinLength(5, ErrorMessage = "Minumum 5 charachters")]
        //public string QuantityPerUnit { get; set; }
        //[Column(TypeName = "Money")]
        //public decimal UnitPrice { get; set; }
        //[Required]
        //public int? UnitsInStock { get; set; }
        //[Required]
        //public int? UnitsOnOrder { get; set; }
        //[Required]
        //public int? ReorderLevel { get; set; }
        //[Required]
        //public bool Discounted { get; set; }

        //[Required(ErrorMessage ="Category must be selected")]
        //public int CategoryID { get; set; }
      

        public Product Product { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
