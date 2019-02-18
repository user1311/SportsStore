using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public virtual void AddItem(Product product, int quantity)
        {
            CartLine cartLine = lineCollection.FirstOrDefault(c => c.Product.ProductID == product.ProductID);

            if (cartLine == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                cartLine.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Product product) => lineCollection.RemoveAll(c => c.Product.ProductID == product.ProductID);

        public virtual decimal ComputeTotalValue() => lineCollection.Sum(c => (c.Product.UnitPrice * c.Quantity));

        public virtual void Clear() => lineCollection.Clear();

        public virtual IEnumerable<CartLine> CartLines => lineCollection;

        public class CartLine
        {
            public int CartlineID { get; set; }
            public Product Product { get; set; }
            public int Quantity { get; set; }
        }
    }
}
