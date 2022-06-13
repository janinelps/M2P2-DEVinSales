using DevInSales.Context;
using DevInSales.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DevInSales.Services
{
    public class TokenService : ITokenService
    {
        private readonly SqlContext _context;

        public TokenService(SqlContext context)
        {
            _context = context;
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
             {
                    new Claim(ClaimTypes.Name, user.Name.ToString()),
            };

            var nomeProfile = (from userRepo in _context.User
                               join profile in _context.Profile on userRepo.ProfileId equals profile.Id
                               where userRepo.Id == user.Id && userRepo.ProfileId == profile.Id
                               select profile.Name).FirstOrDefault();

            claims.Add(new Claim(ClaimTypes.Role, nomeProfile));

            return GenerateToken(claims);
        }


        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Settings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var secutiryToken);

            if (secutiryToken is not JwtSecurityToken jwtSecurityToken)
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        private List<Tuple<string, string>> _refreshsTokens = new List<Tuple<string, string>>();

        public void SaveRefreshToken(string username, string refreshToken)
            => _refreshsTokens.Add(new Tuple<string, string>(username, refreshToken));

        public string GetRefreshToken(string username)
            => _refreshsTokens.FirstOrDefault(x => x.Item1 == username).Item2;

        public void DeleteRefreshToken(string username, string refreshToken)
        {
            var item = _refreshsTokens.FirstOrDefault(x => x.Item1 == username && x.Item2 == refreshToken);
            _refreshsTokens.Remove(item);
        }

    }

}