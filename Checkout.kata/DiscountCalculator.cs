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
            return products.GroupBy(g => g.SKU)
                .Select(g => ApplyDiscount(g.Key, g.Select(x => x.UnitPrice).First(), g.Count())).Sum();

        }

        private decimal ApplyDiscount(string sku, decimal price, int count)
        {
            decimal total = 0;
            var offers = _offerProvider.GetOffers();
            var selectedOffer = offers.FirstOrDefault(x => x.SKU == sku && count >= x.Quantity);

            total += selectedOffer != null
                ? selectedOffer.OfferPrice + ((count % selectedOffer.Quantity) * price)
                : (price * count);

            return total;
        }
    }
}

