using DevInSales.Enums;
using DevInSales.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace DevInSales.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AcessoController : ControllerBase
    {
        [Route("listar")]
        [Authorize]
        [HttpGet]
        public IActionResult Listar()
            => User.IsInRole(Permissoes.Funcionario.GetDisplayName())
                ? Ok(UserRepository.Obter().Select(x => new { x.Name, x.DescricaoPermissao }))
                : Ok(UserRepository.Obter());
        
        [HttpGet]
        [Route("funcionario")]
        [Authorize(Roles = "administrador")]
        public IActionResult AcessoFuncionario()
        {
            return Ok($"Bem-vindo {User.Identity.Name}, à pagina de funcionários");
        }
    }
}
