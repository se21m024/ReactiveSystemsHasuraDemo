using TransactionsCore.Interfaces;
using TransactionsCore.Models;

namespace TransactionsGraphQL.Resolvers
{
    public class Query
    {
        private readonly ITransactionsRepository _transactionsRepository;

        public Query(ITransactionsRepository transactionsRepository)
        {
            _transactionsRepository = transactionsRepository;
        }

        public List<Payment> GetPayments() =>
            _transactionsRepository.GetPaymentsAsync(CancellationToken.None).GetAwaiter().GetResult().ToList();

        public Payment GetPaymentById(Guid id) =>
            _transactionsRepository.GetPaymentAsync(id, CancellationToken.None).GetAwaiter().GetResult();

        public List<Transaction> GetTransactions() =>
            _transactionsRepository.GetTransactionsAsync(CancellationToken.None).GetAwaiter().GetResult().ToList();
    }
}