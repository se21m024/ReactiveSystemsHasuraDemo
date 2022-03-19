﻿namespace TransactionsCore.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public Guid PaymentId { get; set; }

        public string FromIban { get; set; }

        public string ToIban { get; set; }

        public double Amount { get; set; }

        public DateTimeOffset ExecutionDate { get; set; }
    }
}