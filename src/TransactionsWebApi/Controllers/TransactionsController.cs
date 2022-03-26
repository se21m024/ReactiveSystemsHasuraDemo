using System.Reactive.Linq;
using Microsoft.AspNetCore.Mvc;
using TransactionsGraphQLClient;

namespace TransactionsWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;
        private IGraphQlClient _graphQlClient;

        public TransactionsController(
            ILogger<TransactionsController> logger,
            IGraphQlClient graphQlClient)
        {
            _logger = logger;
            _graphQlClient = graphQlClient;
        }

        [HttpPost]
        public async Task<int> CreateTransactions(CancellationToken ct = default)
        {
            try
            {
                _logger.LogInformation("CreateTransactions called.");

                var processedPaymentIds = await this.GetProcessedPaymentIdsAsync(ct);

                var paymentsToProcess = await this.GetPaymentsToProcessAsync(processedPaymentIds, ct);

                foreach (var payment in paymentsToProcess)
                {
                    await _graphQlClient.AddTransaction.ExecuteAsync(
                        payment.FromIban,
                        payment.ToIban,
                        payment.Amount,
                        DateTimeOffset.Now.ToString("o"),
                        payment.Id,
                        ct);
                }

                _logger.LogInformation($"Created transactions for <{paymentsToProcess.Count}> new payments.");

                return paymentsToProcess.Count;
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to create transactions: {e}");
                throw;
            }
        }

        private async Task<List<int>> GetProcessedPaymentIdsAsync(CancellationToken ct)
        {
            return (await _graphQlClient
                       .GetTransactions
                       .ExecuteAsync(ct))
                   .Data?
                   .Transactions
                   .Select(x => x.PaymentId)
                   .Distinct()
                   .ToList()
                   ?? new List<int>();
        }

        private async Task<List<IGetPayments_Payments>> GetPaymentsToProcessAsync(
            List<int> processedPaymentIds,
            CancellationToken ct)
        {
            return (await _graphQlClient
                       .GetPayments
                       .ExecuteAsync(ct))
                   .Data?
                   .Payments
                   .Where(x => !processedPaymentIds.Contains(x.Id))
                   .ToList()
                   ?? new List<IGetPayments_Payments>();
        }
    }
}