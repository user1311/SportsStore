using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        [Required(ErrorMessage ="You must provide product name"),MinLength(3),MaxLength(40)]
        public string ProductName { get; set; }

        [MaxLength(20,ErrorMessage ="Maximum of 20 characters"),MinLength(5,ErrorMessage ="Minumum 5 charachters")]
        public string QuantityPerUnit { get; set; }
        [Column(TypeName ="Money")]
        public decimal UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
        public int? UnitsOnOrder { get; set; }
        public int? ReorderLevel { get; set; }
        public bool Discounted { get; set; }


        public int CategoryID { get; set; }
        public Category Category { get; set; }

    }
}
