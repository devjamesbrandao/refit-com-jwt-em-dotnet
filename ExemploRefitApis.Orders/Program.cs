using ExemploRefitApis.Orders.Services;
using Refit;
using web_teste.Enums;
using web_teste.Handler;
using web_teste.Services;

var builder = WebApplication.CreateBuilder(args);

var paymentsBaseUrl = builder.Configuration["PaymentsBaseUrl"].ToString();

// Register the HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Register the BearerTokenHandler
builder.Services.AddTransient<AuthenticationDelegatingHandler>();

// Configure the HttpClient with the BearerTokenHandler
builder.Services.AddHttpClient(HttpClientConfigName.ApiWithToken, client =>
{
    client.BaseAddress = new Uri(paymentsBaseUrl);
})
.AddHttpMessageHandler<AuthenticationDelegatingHandler>()
.AddTypedClient(client => RestService.For<IPaymentService>(client));

// Configure a separate HttpClient without the BearerTokenHandler
builder.Services.AddHttpClient(HttpClientConfigName.ApiWithoutToken, client =>
{
    client.BaseAddress = new Uri(paymentsBaseUrl);
})
.AddTypedClient(client => RestService.For<ILoginService>(client));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
