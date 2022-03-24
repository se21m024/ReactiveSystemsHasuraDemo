using TransactionsData.Entities;

namespace TransactionsData.Mapping
{
    public static class ModelExtensions
    {
        public static Payment CreateNewEntity(this TransactionsCore.Models.Payment payment)
        {
            return new Payment
            {
                FromIban = payment.FromIban,
                ToIban = payment.ToIban,
                Amount = payment.Amount,
            };
        }

        public static Payment CreateNewEntity(this TransactionsCore.Models.PaymentRequest payment)
        {
            return new Payment
            {
                FromIban = payment.FromIban,
                ToIban = payment.ToIban,
                Amount = payment.Amount,
            };
        }

        public static Transaction CreateNewTransactionEntity(this TransactionsCore.Models.Payment payment)
        {
            return new Transaction
            {
                PaymentId = payment.Id,
                FromIban = payment.FromIban,
                ToIban = payment.ToIban,
                Amount = payment.Amount,
            };
        }

        public static Transaction CreateNewEntity(this TransactionsCore.Models.Transaction transaction)
        {
            return new Transaction
            {
                PaymentId = transaction.PaymentId,
                FromIban = transaction.FromIban,
                ToIban = transaction.ToIban,
                Amount = transaction.Amount,
            };
        }
    }
}
