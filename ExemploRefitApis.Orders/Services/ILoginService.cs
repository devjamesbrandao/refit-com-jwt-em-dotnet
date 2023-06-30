using Refit;
using web_teste.Models;

namespace web_teste.Services
{
    public interface ILoginService
    {
        [Post("/api/login/login")]
        Task<ApiResponse<string>> Login(UserInputModel login);
    }
}