using System.Collections.Generic;

namespace Checkout.Kata
{
    public interface IOfferProvider
    {
        IEnumerable<Discount> GetOffers();
    }
}