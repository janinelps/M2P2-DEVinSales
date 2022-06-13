using DevInSales.Models;
using System.Security.Claims;

namespace DevInSales.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        string GenerateToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        void SaveRefreshToken(string username, string refreshToken);
        string GetRefreshToken(string username);
        void DeleteRefreshToken(string username, string refreshToken);
    }
}
