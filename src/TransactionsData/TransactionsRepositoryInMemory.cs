using System.Collections.Concurrent;
using TransactionsCore.Interfaces;
using TransactionsData.Entities;
using TransactionsData.Mapping;

namespace TransactionsData
{
    public class TransactionsRepositoryInMemory : ITransactionsRepository
    {
        private readonly ConcurrentDictionary<Guid, Payment> _payments;

        private readonly ConcurrentDictionary<Guid, Transaction> _transactions;

        public TransactionsRepositoryInMemory()
        {
            _payments = new();
            _transactions = new();

            var payment1 = new TransactionsCore.Models.PaymentRequest
            {
                FromIban = "Iban1",
                ToIban = "Iban2",
                Amount = 23.4,
            };

            var payment2 = new TransactionsCore.Models.PaymentRequest
            {
                FromIban = "Iban2",
                ToIban = "Iban4",
                Amount = 89.75,
            };

            this.AddPaymentAsync(payment1, CancellationToken.None).GetAwaiter().GetResult();
            this.AddPaymentAsync(payment2, CancellationToken.None).GetAwaiter().GetResult();
        }

        public async Task<TransactionsCore.Models.Payment> AddPaymentAsync(TransactionsCore.Models.PaymentRequest payment, CancellationToken ct)
        {
            var entity = payment.CreateNewEntity();

            if (!_payments.TryAdd(entity.Id, entity))
            {
                throw new Exception("Failed to persist payment.");
            }

            return entity.ToModel();
        }

        public async Task<IEnumerable<TransactionsCore.Models.Payment>> GetPaymentsAsync(CancellationToken ct)
        {
            return _payments.Values.OrderByDescending(x => x.CreateDate).Select(x => x.ToModel());
        }

        public async Task<TransactionsCore.Models.Payment> GetPaymentAsync(Guid id, CancellationToken ct)
        {
            return _payments.Values.Single(x => x.Id == id).ToModel();
        }

        public async Task<TransactionsCore.Models.Transaction> AddTransactionAsync(TransactionsCore.Models.Transaction transaction, CancellationToken ct)
        {
            var entity = transaction.CreateNewEntity();

            if (!_transactions.TryAdd(entity.Id, entity))
            {
                throw new Exception("Failed to persist transaction.");
            }

            return entity.ToModel();
        }

        public async Task<TransactionsCore.Models.Transaction> AddTransactionFromPaymentAsync(
            TransactionsCore.Models.Payment payment,
            CancellationToken ct)
        {
            var entity = payment.CreateNewTransactionEntity();

            if (!_transactions.TryAdd(entity.Id, entity))
            {
                throw new Exception("Failed to persist transaction from payment.");
            }

            return entity.ToModel();
        }

        public async Task<IEnumerable<TransactionsCore.Models.Transaction>> GetTransactionsAsync(CancellationToken ct)
        {
            return _transactions.Values.OrderByDescending(x => x.ExecutionDate).Select(x => x.ToModel());
        }
    }
}