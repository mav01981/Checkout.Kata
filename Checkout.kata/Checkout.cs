using System;
using System.Collections.Generic;

namespace Checkout.Kata
{
    public class Checkout : ICheckout
    {
        private readonly IProductValidator _productValidator;
        private readonly IDiscountCalculator _discountCalculator;
        private List<Item> Products = new List<Item>();

        public decimal Total { get; set; }

        public Checkout(IProductValidator productValidator, IDiscountCalculator discountCalculator)
        {
            _productValidator = productValidator;
            _discountCalculator = discountCalculator;
        }

        public void Scan(Item product)
        {
            if (!_productValidator.IsProductValid(product.SKU))
            {
                throw new ArgumentNullException(nameof(product));
            }

            Products.Add(product);
            this.Total = _discountCalculator.Calculate(Products);
        }
    }
}
