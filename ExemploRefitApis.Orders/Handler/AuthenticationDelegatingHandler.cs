using System.Net.Http.Headers;
using ExemploRefitApis.Orders.Services;
using Refit;
using web_teste.Enums;
using web_teste.Models;
using web_teste.Services;

namespace web_teste.Handler
{
    public class AuthenticationDelegatingHandler : DelegatingHandler
    {
        private readonly ILoginService _paymentServiceLogin;
        private readonly IHttpContextAccessor _accessor;
        private readonly IConfiguration _configuration;

        public AuthenticationDelegatingHandler(
            IHttpClientFactory httpClientFactory, 
            IHttpContextAccessor accessor, 
            IConfiguration configuration
        )
        {
            _paymentServiceLogin = RestService.For<ILoginService>(httpClientFactory.CreateClient(HttpClientConfigName.ApiWithoutToken));
            _configuration = configuration;
            _accessor = accessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if(_accessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var tokenHeader))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenHeader);

                return await base.SendAsync(request, cancellationToken);
            }
            
            var response = await _paymentServiceLogin.Login(
                new UserInputModel
                {
                    Name = _configuration["UserLogin:Name"],
                    Password = _configuration["UserLogin:Password"]
                }
            );

            if(!response.IsSuccessStatusCode) throw new Exception(response.ReasonPhrase);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", response.Content);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}