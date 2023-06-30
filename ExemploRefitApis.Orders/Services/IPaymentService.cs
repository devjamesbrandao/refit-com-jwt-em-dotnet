using ExemploRefitApis.Orders.Models;
using Refit;

namespace ExemploRefitApis.Orders.Services
{
    public interface IPaymentService
    {
        [Post("/api/payments")]
        [Headers("Authorization: Bearer")]
        Task<ProcessPaymentResultViewModel> Process(ProcessPaymentInputModel processPaymentInputModel);

        [Get("/api/payments/{id}")]
        [Headers("Authorization: Bearer")]
        Task<PaymentDataViewModel> GetData([AliasAs("id")]int paymentId);
    }
}
