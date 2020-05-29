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

            total += ApplyDiscount(products, offers);
            total += ApplyProductPrice(products.Where(p => offers.All(o => o.SKU != p.SKU)));
            return total;
        }

        private decimal ApplyDiscount(IEnumerable<Item> products, IEnumerable<Discount> offers)
        {
            decimal total = 0;

            foreach (var offer in offers)
            {
                var product = products.FirstOrDefault(x => x.SKU == offer.SKU);
                if (product != null)
                {
                    int productCount = products.Where(x => x.SKU == offer.SKU).Count();
                    var offerCount = productCount / offer.Quantity;
                    bool validOffer = offerCount >= 1;

                    if (validOffer)
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
            return total;
        }

        private decimal ApplyProductPrice(IEnumerable<Item> products)
        {
            decimal total = 0;

            foreach (var product in products)
            {
                total += product.UnitPrice;
            }
            return total;
        }
    }
}

