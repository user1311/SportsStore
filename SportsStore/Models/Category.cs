using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        [Required(ErrorMessage ="You must provide name of category"),MaxLength(15)]
        public string CategoryName { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
