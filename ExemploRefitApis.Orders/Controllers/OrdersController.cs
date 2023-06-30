using ExemploRefitApis.Orders.Models;
using ExemploRefitApis.Orders.Services;
using Microsoft.AspNetCore.Mvc;
using Refit;
using web_teste.Enums;
using web_teste.Models;

namespace ExemploRefitApis.Orders.Controllers
{
    [ApiController]
    [Route("api/Orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        
        private readonly IConfiguration _configuration;

        public OrdersController(IPaymentService paymentService, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _paymentService = RestService.For<IPaymentService>(httpClientFactory.CreateClient(HttpClientConfigName.ApiWithToken));;
            _configuration = configuration;
        }

        [HttpGet("{orderId}/payments/{id}")]
        public async Task<ActionResult<PaymentDataViewModel>> GetPayment(int orderId, int id)
        {
            return await _paymentService.GetData(id);
        }

        [HttpPost("{orderId}/payments")]
        public async Task<IActionResult> Create(int orderId, [FromBody] ProcessPaymentInputModel processPaymentInputModel)
        {
            var result = await _paymentService.Process(processPaymentInputModel);

            if(!result.Success)
            {
                return BadRequest(result.Errors);
            }

            return CreatedAtAction(nameof(GetPayment), new { orderId, id = result.IdPayment }, result);
        }
    }
}
