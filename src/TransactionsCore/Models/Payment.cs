namespace TransactionsCore.Models
{
    public class Payment
    {
        public Guid Id { get; set; }

        public string FromIban { get; set; }

        public string ToIban { get; set; }

        public double Amount { get; set; }

        public DateTimeOffset CreateDate { get; set; }
    }
}
