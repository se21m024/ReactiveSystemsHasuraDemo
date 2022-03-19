using Microsoft.AspNetCore.Mvc;
using TransactionsCore.Interfaces;
using TransactionsCore.Models;

namespace TransactionsWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly ITransactionsRepository _transactionsRepository;

        public TransactionsController(
            ILogger<TransactionsController> logger,
            ITransactionsRepository transactionsRepository)
        {
            _logger = logger;
            _transactionsRepository = transactionsRepository;
        }

        [HttpGet]
        public async Task<List<Transaction>> Get(CancellationToken ct = default)
        {
            return (await _transactionsRepository.GetTransactionsAsync(ct)).ToList();
        }
    }
}