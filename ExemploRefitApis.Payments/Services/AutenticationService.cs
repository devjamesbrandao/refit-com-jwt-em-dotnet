using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace web_teste.Services
{
    public class AutenticationService : IAutenticationService
    {
        private static readonly JwtSecurityTokenHandler tokenHandler = new();
        
        public string GenerateJWTToken(string name)
        {
            var tokenDescriptor = GenerateTokenDescriptor(name);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenWrite = tokenHandler.WriteToken(token);
            return tokenWrite;
        }

        private static SecurityTokenDescriptor GenerateTokenDescriptor(string name)
        {
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim("Name", name)
                    }),
                Expires = (DateTime.Now.AddHours(12).ToUniversalTime()),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Utils.Settings.Key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
        }
    }
}