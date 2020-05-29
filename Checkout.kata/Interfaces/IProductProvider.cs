namespace Checkout.Kata
{
    public interface IProductProvider
    {
        Item Get(string sku);
    }
}