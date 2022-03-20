using System.Collections.Concurrent;
using Microsoft.Azure.Cosmos;
using TransactionsCore.Interfaces;
using TransactionsData.Entities;
using TransactionsData.Mapping;

namespace TransactionsData
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly ConcurrentDictionary<Guid, Payment> _payments = new ();

        private readonly ConcurrentDictionary<Guid, Transaction> _transactions = new();

        private readonly Container _container;

        public TransactionsRepository(
            CosmosClient cosmosClient,
            string databaseName,
            string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task AddPaymentAsync(TransactionsCore.Models.Payment payment, CancellationToken ct)
        {
            var entity = payment.ToEntity();
            entity.Id = Guid.NewGuid();

            await _container.CreateItemAsync(entity, cancellationToken: ct);

            //if (!_payments.TryAdd(entity.Id, entity))
            //{
            //    throw new Exception("Failed to persist payment.");
            //}
        }

        public async Task<IEnumerable<TransactionsCore.Models.Payment>> GetPaymentsAsync(CancellationToken ct)
        {
            return _container.GetItemLinqQueryable<Payment>()
                .OrderByDescending(x => x.CreateDate)
                .Select(x => x.ToModel())
                .AsEnumerable();

            // return _payments.Values.OrderByDescending(x => x.CreateDate).Select(x => x.ToModel());
        }

        public async Task<TransactionsCore.Models.Payment> GetPaymentAsync(Guid id, CancellationToken ct)
        {
            return _container.GetItemLinqQueryable<Payment>()
                .First(x => x.Id == id)
                .ToModel();

            // return _payments.Values.Single(x => x.Id == id).ToModel();
        }

        public async Task AddTransactionAsync(TransactionsCore.Models.Transaction transaction, CancellationToken ct)
        {
            var entity = transaction.ToEntity();
            entity.Id = Guid.NewGuid();

            await _container.CreateItemAsync(entity, cancellationToken: ct);

            //if (!_transactions.TryAdd(entity.Id, entity))
            //{
            //    throw new Exception("Failed to persist transaction.");
            //}
        }

        public async Task<IEnumerable<TransactionsCore.Models.Transaction>> GetTransactionsAsync(CancellationToken ct)
        {
            return _container.GetItemLinqQueryable<Transaction>()
                .OrderByDescending(x => x.ExecutionDate)
                .Select(x => x.ToModel())
                .AsEnumerable();

            // return _transactions.Values.OrderByDescending(x => x.ExecutionDate).Select(x => x.ToModel());
        }
    }
}