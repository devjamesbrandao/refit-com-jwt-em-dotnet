namespace web_teste.Services
{
    public interface IAutenticationService
    {
       string GenerateJWTToken(string name);
    }
}