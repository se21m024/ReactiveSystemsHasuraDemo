namespace TransactionsCore.Models
{
    public class PaymentRequest
    {
        public string FromIban { get; set; }

        public string ToIban { get; set; }

        public double Amount { get; set; }
    }
}
