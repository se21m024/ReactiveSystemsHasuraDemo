using TransactionsCore.Models;

namespace TransactionsCore.Interfaces
{
    public interface ITransactionsRepository
    {
        Task<Payment> AddPaymentAsync(PaymentRequest payment, CancellationToken ct);

        Task<IEnumerable<Payment>> GetPaymentsAsync(CancellationToken ct);

        Task<Payment> GetPaymentAsync(Guid id, CancellationToken ct);

        Task<Transaction> AddTransactionAsync(Transaction transaction, CancellationToken ct);

        Task<IEnumerable<Transaction>> GetTransactionsAsync(CancellationToken ct);
    }
}
