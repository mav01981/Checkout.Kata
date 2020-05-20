using System.Collections.Generic;

namespace Checkout.Kata
{
    public interface IDiscountCalculator
    {
        decimal Calculate(List<Item> products);
    }
}