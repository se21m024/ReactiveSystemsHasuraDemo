﻿using Microsoft.Azure.Cosmos;
using TransactionsCore.Interfaces;
using TransactionsData.Entities;
using TransactionsData.Mapping;

namespace TransactionsData
{
    public class TransactionsRepositoryCosmosDb : ITransactionsRepository
    {
        private readonly Container _container;

        public TransactionsRepositoryCosmosDb(
            CosmosClient cosmosClient,
            string databaseName,
            string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task<TransactionsCore.Models.Payment> AddPaymentAsync(TransactionsCore.Models.PaymentRequest payment, CancellationToken ct)
        {
            var entity = payment.CreateNewEntity();
            entity.Id = Guid.NewGuid();

            await _container.CreateItemAsync(entity, cancellationToken: ct);
            return entity.ToModel();
        }

        public async Task<IEnumerable<TransactionsCore.Models.Payment>> GetPaymentsAsync(CancellationToken ct)
        {
            return _container.GetItemLinqQueryable<Payment>()
                .OrderByDescending(x => x.CreateDate)
                .Select(x => x.ToModel())
                .AsEnumerable();
        }

        public async Task<TransactionsCore.Models.Payment> GetPaymentAsync(Guid id, CancellationToken ct)
        {
            return _container.GetItemLinqQueryable<Payment>()
                .First(x => x.Id == id)
                .ToModel();
        }

        public async Task<TransactionsCore.Models.Transaction> AddTransactionAsync(TransactionsCore.Models.Transaction transaction, CancellationToken ct)
        {
            var entity = transaction.CreateNewEntity();
            entity.Id = Guid.NewGuid();

            await _container.CreateItemAsync(entity, cancellationToken: ct);
            return entity.ToModel();
        }

        public async Task<TransactionsCore.Models.Transaction> AddTransactionFromPaymentAsync(
            TransactionsCore.Models.Payment payment,
            CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TransactionsCore.Models.Transaction>> GetTransactionsAsync(CancellationToken ct)
        {
            return _container.GetItemLinqQueryable<Transaction>()
                .OrderByDescending(x => x.ExecutionDate)
                .Select(x => x.ToModel())
                .AsEnumerable();
        }
    }
}