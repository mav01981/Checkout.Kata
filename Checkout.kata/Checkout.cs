using System;
using System.Collections.Generic;

namespace Checkout.Kata
{
    public class Checkout : ICheckout
    {
        private readonly IProductProvider _productService;
        private readonly IDiscountCalculator _discountCalculator;
        private readonly List<Item> products = new List<Item>();

        public decimal Total => _discountCalculator.Calculate(products);

        public Checkout(IProductProvider productService, IDiscountCalculator discountCalculator)
        {
            _productService = productService;
            _discountCalculator = discountCalculator;
        }

        public void Scan(string sku)
        {
            var product = _productService.Get(sku);

            if (product == null)
            {
                throw new Exception($"Product not found. {sku}");
            }

            products.Add(product);
        }
    }
}
