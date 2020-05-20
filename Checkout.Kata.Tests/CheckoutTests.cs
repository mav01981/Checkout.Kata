using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Checkout.Kata.Tests
{
    public class CheckoutTests
    {
        private readonly IProductValidator productValidator;
        private readonly Mock<IOfferProvider> offerProvider;

        public CheckoutTests()
        {
            productValidator = new Mock<IProductValidator>().Object;
            offerProvider = new Mock<IOfferProvider>();
            offerProvider.Setup(x => x.GetOffers()).Returns(new Discount[]
                {
                    new Discount { SKU ="A99",  Quantity = 3 , OfferPrice = 1.30m },
                    new Discount { SKU ="B15", Quantity = 2 , OfferPrice = 0.45m }
               });
        }

        [Fact(DisplayName = "1 Item checkout no discount.")]
        [Trait("Checkout", "Players")]
        public void ScanItem_AtCheckout_WithNoDiscount()
        {
            var checkout = new Checkout(productValidator, offerProvider.Object);
            var shoppingBasket = new List<Item>
            {
                new Item { SKU ="A99", UnitPrice=0.50m },
            };

            foreach (var item in shoppingBasket)
            {
                checkout.Scan(item);
            }

            Assert.Equal(0.50m, checkout.Total);
        }

        //[Fact(DisplayName = "1 Item checkout unrecognised throws argument null exception.")]
        //[Trait("Checkout", "Players")]
        //public void ScanItem_AtCheckoutWithItemNotRecognised_ReturnsZero()
        //{
        //    var checkout = new Checkout();
        //    var shoppingBasket = new List<Item>
        //    {
        //        new Item { SKU ="zzzzzz", UnitPrice=0.50m },
        //    };

        //    foreach (var item in shoppingBasket)
        //    {
        //        checkout.Scan(item);
        //    }

        //    Assert.Equal(0.0m, checkout.Total);
        //}

        [Fact(DisplayName = "3 Items at checkout with discount applied for SKU A99 at 1.30")]
        [Trait("Checkout", "Players")]
        public void ScanItem_AtCheckout_WithDiscount()
        {
            var checkout = new Checkout(productValidator, offerProvider.Object);
            var shoppingBasket = new List<Item>
            {
                new Item { SKU ="A99", UnitPrice=0.5m },
                new Item { SKU ="A99", UnitPrice=0.5m },
                new Item { SKU ="A99", UnitPrice=0.5m },
            };

            foreach (var item in shoppingBasket)
            {
                checkout.Scan(item);
            }

            Assert.Equal(1.3m, checkout.Total);
        }

        [Fact(DisplayName = "3 Items at checkout in random order discount applied.")]
        [Trait("Checkout", "Players")]
        public void ScanItem_AtCheckout_InRandomOrderApplyDiscount()
        {
            var checkout = new Checkout(productValidator, offerProvider.Object);
            var shoppingBasket = new List<Item>
            {
                new Item { SKU ="B15", UnitPrice=0.3m },
                new Item { SKU ="A99", UnitPrice=0.5m },
                new Item { SKU ="B15", UnitPrice=0.3m },
            };

            foreach (var item in shoppingBasket)
            {
                checkout.Scan(item);
            }

            Assert.Equal(0.95M, checkout.Total);
        }

        [Fact]
        public void ScanItem_AtCheckout_InRandomOrderNoDiscount()
        {
            var checkout = new Checkout(productValidator, offerProvider.Object);
            var shoppingBasket = new List<Item>
            {
                new Item { SKU ="B15", UnitPrice=0.3m },
                new Item { SKU ="A99", UnitPrice=0.5m },
                new Item { SKU ="D15", UnitPrice=0.9m },
            };

            foreach (var item in shoppingBasket)
            {
                checkout.Scan(item);
            }

            Assert.Equal(1.7M, checkout.Total);
        }


    }
}
