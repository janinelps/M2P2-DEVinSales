using DevInSales.Context;
using DevInSales.DTOs;
using DevInSales.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DevInSales.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly SqlContext _context;
        private readonly ITokenService _tokenService;

        public AutenticacaoController(SqlContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] UserLoginDTO user)
        {
           var userLogin= _context.User.Where(x => x.Email.ToLower() == user.Email.ToLower() && x.Password == user.Password)?.FirstOrDefault();

            if (userLogin == null) return NotFound();

            var token = _tokenService.GenerateToken(userLogin);

            return Ok(token);

        }

        [HttpPost]
        [Route("refresh")]
        [AllowAnonymous]
        public ActionResult<dynamic> RefreshToken([FromQuery] string token, [FromQuery] string refreshToken)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(token);
            var username = principal.Identity.Name;
            var savedRefreshToken = _tokenService.GetRefreshToken(username);

            if (savedRefreshToken != refreshToken)
                throw new SecurityTokenException("Invalid refresh token");

            var newToken = _tokenService.GenerateToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            _tokenService.DeleteRefreshToken(username, refreshToken);
            _tokenService.SaveRefreshToken(username, newRefreshToken);

            return new ObjectResult(new
            {
                token = newToken,
                refreshToken = newRefreshToken

            });

        }

    }
}
