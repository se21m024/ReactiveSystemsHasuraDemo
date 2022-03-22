﻿using System.Collections.Concurrent;
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

            var payment1 = new TransactionsCore.Models.Payment
            {
                FromIban = "Iban1",
                ToIban = "Iban2",
                Amount = 23.4,
            };

            var payment2 = new TransactionsCore.Models.Payment
            {
                FromIban = "Iban2",
                ToIban = "Iban4",
                Amount = 89.75,
            };

            this.AddPaymentAsync(payment1, CancellationToken.None).GetAwaiter().GetResult();
            this.AddPaymentAsync(payment2, CancellationToken.None).GetAwaiter().GetResult();
        }

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
            return _payments.Values.OrderByDescending(x => x.CreateDate).Select(x => x.ToModel());
        }

        public async Task<TransactionsCore.Models.Payment> GetPaymentAsync(Guid id, CancellationToken ct)
        {
            return _payments.Values.Single(x => x.Id == id).ToModel();
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
            return _transactions.Values.OrderByDescending(x => x.ExecutionDate).Select(x => x.ToModel());
        }
    }
}