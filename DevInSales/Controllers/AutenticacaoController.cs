using DevInSales.Models;
using DevInSales.Repositories;
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

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] User user)
        {
            UserRepository.CheckNameAndPassword(user.Name, user.Password);

            if (user == null) return NotFound();

            var token = TokenService.GenerateToken(user);

            return Ok(token);

        }

        [HttpPost]
        [Route("refresh")]
        [AllowAnonymous]
        public ActionResult<dynamic> RefreshToken([FromQuery] string token, [FromQuery] string refreshToken)
        {

            var principal = TokenService.GetPrincipalFromExpiredToken(token);
            var username = principal.Identity.Name;
            var savedRefreshToken = TokenService.GetRefreshToken(username);

            if (savedRefreshToken != refreshToken)
                throw new SecurityTokenException("Invalid refresh token");

            var newToken = TokenService.GenerateToken(principal.Claims);
            var newRefreshToken = TokenService.GenerateRefreshToken();
            TokenService.DeleteRefreshToken(username, refreshToken);
            TokenService.SaveRefreshToken(username, newRefreshToken);

            return new ObjectResult(new
            {
                token = newToken,
                refreshToken = newRefreshToken

            });

        }

    }
}
