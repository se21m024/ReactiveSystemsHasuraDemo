using TransactionsData.Entities;

namespace TransactionsData.Mapping
{
    public static class EntityExtensions
    {
        public static TransactionsCore.Models.Payment ToModel(this Payment payment)
        {
            return new TransactionsCore.Models.Payment
            {
                Id = payment.Id,
                FromIban = payment.FromIban,
                ToIban = payment.ToIban,
                Amount = payment.Amount,
                CreateDate = payment.CreateDate,
            };
        }

        public static TransactionsCore.Models.Transaction ToModel(this Transaction transaction)
        {
            return new TransactionsCore.Models.Transaction
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