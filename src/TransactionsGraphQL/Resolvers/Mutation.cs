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

        public Payment AddPayment(PaymentRequest payment)
        {
            var newPayment = _transactionsRepository
                .AddPaymentAsync(payment, CancellationToken.None)
                .GetAwaiter()
                .GetResult();

            _transactionsRepository
                .AddTransactionFromPaymentAsync(newPayment, CancellationToken.None)
                .GetAwaiter()
                .GetResult();

            return newPayment;
        }
    }
}
