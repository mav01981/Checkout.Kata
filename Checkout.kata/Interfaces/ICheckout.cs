namespace Checkout.Kata
{
    public interface ICheckout
    {
        decimal Total { get; }
        void Scan(Item product);
    }
}