namespace TransactionsData.Entities
{
    public class Payment
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FromIban { get; set; }

        public string ToIban { get; set; }

        public double Amount { get; set; }

        public DateTimeOffset CreateDate { get; set; } = DateTimeOffset.Now;
    }
}
