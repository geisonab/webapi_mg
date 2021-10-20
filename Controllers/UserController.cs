using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI_MG.Models;
using WebAPI_MG.Repositories;
using WebAPI_MG.Services;

namespace WebAPI_MG.Controllers
{
    public class UserController : Controller
    {
        private UserRepository _repository;

        public UserController(UserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("V1/login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            // Recupera o usuário
            var user = await _repository.GetUser(model.Username, model.Password);

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenService.GenerateToken(user);

            // Oculta a senha
            user.Password = "";

            // Retorna os dados
            return new
            {
                user = user,
                token = token
            };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => $"Autenticado - {User.Identity.Name}, {User.Identity.AuthenticationType} - {User.Identity.IsAuthenticated}";

        [HttpGet]
        [Route("Funcionario")]
        [Authorize(Roles = "Funcionario,Gerente")]
        public string Employee() => "Funcionario";

        [HttpGet]
        [Route("Gerente")]
        [Authorize(Roles = "Gerente")]
        public string Manager() => "Gerente";
    }
}