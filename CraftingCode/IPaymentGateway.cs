namespace CraftingCode
{
    public interface IPaymentGateway
    {
        void PayWith(PaymentDetails paymentDetails);
    }
}