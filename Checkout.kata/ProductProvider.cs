using System.Collections.Generic;
using System.Linq;

namespace Checkout.Kata
{
    public class ProductProvider : IProductProvider
    {
        private IEnumerable<Item> products => new Item[]
        {
           new Item {SKU = "A99", UnitPrice =0.50m } ,
           new Item {SKU = "B15", UnitPrice =0.30m } ,
           new Item {SKU = "C40", UnitPrice =0.60m } ,
           new Item {SKU = "D15", UnitPrice =0.90m } ,
        };

        public ProductProvider() { }

        public Item Get(string sku)
        {
            return products.FirstOrDefault(x => x.SKU == sku);
        }
    }
}

