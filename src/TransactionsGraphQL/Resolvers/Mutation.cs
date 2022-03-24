using TransactionsCore.Interfaces;
using TransactionsCore.Models;

namespace TransactionsGraphQL.Resolvers
{
    public class Mutation
    {
        private readonly ITransactionsRepository _transactionsRepository;

        public Mutation(ITransactionsRepository transactionsRepository)
        {
            _transactionsRepository = transactionsRepository;
        }

        public async Task<Payment> AddPaymentAsync(PaymentRequest payment, CancellationToken ct)
        {
            return await _transactionsRepository.AddPaymentAsync(payment, ct);
        }
    }
}
