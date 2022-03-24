using TransactionsData.Entities;

namespace TransactionsData.Mapping
{
    public static class ModelExtensions
    {
        public static Payment ToEntity(this TransactionsCore.Models.Payment payment)
        {
            return new Payment
            {
                Id = payment.Id,
                FromIban = payment.FromIban,
                ToIban = payment.ToIban,
                Amount = payment.Amount,
                CreateDate = payment.CreateDate,
            };
        }

        public static Payment ToEntity(this TransactionsCore.Models.PaymentRequest payment)
        {
            return new Payment
            {
                FromIban = payment.FromIban,
                ToIban = payment.ToIban,
                Amount = payment.Amount,
            };
        }

        public static Transaction ToEntity(this TransactionsCore.Models.Transaction transaction)
        {
            return new Transaction
            {
                Id = transaction.Id,
                PaymentId = transaction.PaymentId,
                FromIban = transaction.FromIban,
                ToIban = transaction.ToIban,
                Amount = transaction.Amount,
                ExecutionDate = transaction.ExecutionDate,
            };
        }
    }
}
