using System.Collections.Concurrent;
using TransactionsCore.Interfaces;
using TransactionsData.Entities;
using TransactionsData.Mapping;

namespace TransactionsData
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly ConcurrentDictionary<Guid, Payment> _payments = new ();

        private readonly ConcurrentDictionary<Guid, Transaction> _transactions = new();

        public async Task AddPaymentAsync(TransactionsCore.Models.Payment payment, CancellationToken ct)
        {
            var entity = payment.ToEntity();
            entity.Id = Guid.NewGuid();

            if (!_payments.TryAdd(entity.Id, entity))
            {
                throw new Exception("Failed to persist payment.");
            }
        }

        public async Task<IEnumerable<TransactionsCore.Models.Payment>> GetPaymentsAsync(CancellationToken ct)
        {
            return _payments.Values.OrderBy(x => x.CreateDate).Select(x => x.ToModel());
        }

        public async Task AddTransactionAsync(TransactionsCore.Models.Transaction transaction, CancellationToken ct)
        {
            var entity = transaction.ToEntity();
            entity.Id = Guid.NewGuid();

            if (!_transactions.TryAdd(entity.Id, entity))
            {
                throw new Exception("Failed to persist transaction.");
            }
        }

        public async Task<IEnumerable<TransactionsCore.Models.Transaction>> GetTransactionsAsync(CancellationToken ct)
        {
            return _transactions.Values.OrderBy(x => x.ExecutionDate).Select(x => x.ToModel());
        }
    }
}