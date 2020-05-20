using System.Collections.Generic;

namespace Checkout.Kata
{
    public class OfferProvider : IOfferProvider
    {
        private IEnumerable<Discount> Offers => new Discount[]
        {
           new Discount { SKU ="A99",  Quantity = 3 , OfferPrice = 1.30m },
           new Discount { SKU ="B15", Quantity = 2 , OfferPrice = 0.45m }
        };

        public OfferProvider() { }

        public IEnumerable<Discount> GetOffers()
        {
            return this.Offers;
        }
    }
}
