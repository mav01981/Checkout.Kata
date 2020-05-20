using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkout.Kata
{
    public class DiscountCalculator : IDiscountCalculator
    {
        private readonly IOfferProvider _offerProvider;

        public DiscountCalculator(IOfferProvider offerProvider)
        {
            _offerProvider = offerProvider;
        }

        public decimal Calculate(List<Item> products)
        {
            decimal total = 0;

            var offers = _offerProvider.GetOffers();

            foreach (var offer in offers)
            {
                var product = products.FirstOrDefault(x => x.SKU == offer.SKU);
                if (product != null)
                {
                    int productCount = products.Where(x => x.SKU == offer.SKU).Count();
                    var offerCount = productCount / offer.Quantity;
                    if (offerCount >= 1)
                    {
                        total += (offerCount * offer.OfferPrice);
                        total += (productCount % offer.Quantity) * product.UnitPrice;
                    }
                    else
                    {
                        total += product.UnitPrice * productCount;
                    }
                }
            }
            var productsNotInOffer = products.Where(x => !offers.Select(X => X.SKU).ToArray().Contains(x.SKU));
            foreach (var product in productsNotInOffer)
            {
                total += product.UnitPrice;
            }

            return total;
        }
    }
}

