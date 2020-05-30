using System;
using System.Collections.Generic;
using Xunit;

namespace Checkout.Kata.Tests
{
    public class CheckoutTests
    {
        private readonly IProductProvider  productProvider;
        private readonly IDiscountCalculator discountCalculator;
        private readonly IOfferProvider offerProvider;

        public CheckoutTests()
        {
            productProvider = new ProductProvider();
            offerProvider = new OfferProvider();
            discountCalculator = new DiscountCalculator(offerProvider);
        }

        [Fact(DisplayName = "1 Item checkout no discount.")]
        [Trait("Checkout", "Scan")]
        public void ScanItem_AtCheckout_WithNoDiscount()
        {
            var checkout = new Checkout(productProvider, discountCalculator);

            checkout.Scan("A99");

            Assert.Equal(0.50m, checkout.Total);
        }

        [Fact(DisplayName = "1 Item checkout unrecognised throws argument null exception.")]
        [Trait("Checkout", "Players")]
        public void ScanItem_AtCheckoutWithItemNotRecognised_ReturnsZero()
        {
            var checkout = new Checkout(productProvider, discountCalculator);

            Exception ex = Assert.Throws<Exception>(() => checkout.Scan("ZZZZ"));

            Assert.Equal("Product not found. ZZZZ", ex.Message);
        }

        [Fact(DisplayName = "3 Items at checkout with discount applied for SKU A99 at 1.30")]
        [Trait("Checkout", "Scan")]
        public void ScanItem_AtCheckout_WithDiscount()
        {
            var checkout = new Checkout(productProvider, discountCalculator);
            var shoppingBasket = new List<string>
            {
                 "A99",
                 "A99",
                 "A99"
            };

            foreach (var item in shoppingBasket)
            {
                checkout.Scan(item);
            }

            Assert.Equal(1.3m, checkout.Total);
        }

        [Fact(DisplayName = "4 Items at checkout with discount applied for SKU A99 at 1.30 discount + 0.50")]
        [Trait("Checkout", "Scan")]
        public void Scan4Items_AtCheckout_WithDiscount()
        {
            var checkout = new Checkout(productProvider, discountCalculator);
            var shoppingBasket = new List<string>
            {
                 "A99",
                 "A99",
                 "A99",
                 "A99",
            };

            foreach (var item in shoppingBasket)
            {
                checkout.Scan(item);
            }

            Assert.Equal(1.8m, checkout.Total);
        }

        [Fact(DisplayName = "3 Items at checkout in random order discount applied.")]
        [Trait("Checkout", "Scan")]
        public void ScanItem_AtCheckout_InRandomOrderApplyDiscount()
        {
            var checkout = new Checkout(productProvider, discountCalculator);
            var shoppingBasket = new List<string>
            {
                 "B15",
                 "A99",
                 "B15"
            };

            foreach (var item in shoppingBasket)
            {
                checkout.Scan(item);
            }

            Assert.Equal(0.95M, checkout.Total);
        }

        [Fact(DisplayName = "3 Items at checkout in random order with no discount applied.")]
        [Trait("Checkout", "Scan")]
        public void ScanItem_AtCheckout_InRandomOrderNoDiscount()
        {
            var checkout = new Checkout(productProvider, discountCalculator);
            var shoppingBasket = new List<string>
            {
                 "B15",
                 "A99",
                 "D15"
            };

            foreach (var item in shoppingBasket)
            {
                checkout.Scan(item);
            }

            Assert.Equal(1.7M, checkout.Total);
        }
    }
}
