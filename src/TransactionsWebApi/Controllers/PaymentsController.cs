using Microsoft.AspNetCore.Mvc;
using TransactionsCore.Interfaces;
using TransactionsCore.Models;

namespace TransactionsWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly ILogger<PaymentsController> _logger;
        private readonly ITransactionsRepository _transactionsRepository;

        public PaymentsController(
            ILogger<PaymentsController> logger,
            ITransactionsRepository transactionsRepository)
        {
            _logger = logger;
            _transactionsRepository = transactionsRepository;
        }

        [HttpGet]
        public async Task<List<Payment>> Get(CancellationToken ct = default)
        {
            return (await _transactionsRepository.GetPaymentsAsync(ct)).ToList();
        }

        [HttpPost]
        public async Task Add([FromBody] PaymentRequest payment, CancellationToken ct = default)
        {
            await _transactionsRepository.AddPaymentAsync(payment, ct);
        }
    }
}