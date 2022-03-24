namespace TransactionsData.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid PaymentId { get; set; }

        public string FromIban { get; set; }

        public string ToIban { get; set; }

        public double Amount { get; set; }

        public DateTimeOffset ExecutionDate { get; set; } = DateTimeOffset.Now;
    }
}
