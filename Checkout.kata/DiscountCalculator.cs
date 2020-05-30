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
            var offers = _offerProvider.GetOffers();

            return products.GroupBy(g => g.SKU)
                .Select(g => ApplyDiscount(offers, g.Select(x => x).First(), g.Count())).Sum();
        }

        private decimal ApplyDiscount(IEnumerable<Discount> offers, Item item, int count)
        {
            decimal total = 0;
            var selectedOffer = offers.FirstOrDefault(x => x.SKU == item.SKU && count >= x.Quantity);

            total += selectedOffer != null
                ? selectedOffer.OfferPrice + ((count % selectedOffer.Quantity) * item.UnitPrice)
                : (item.UnitPrice * count);

            return total;
        }
    }
}

