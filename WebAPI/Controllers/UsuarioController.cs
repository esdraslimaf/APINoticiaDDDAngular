using Aplicacao.Aplicacoes;
using Aplicacao.Interfaces;
using Entidades.Entidades;
using Entidades.Enums;
using Infraestrutura.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using WebAPI.Models;
using WebAPI.Token;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IAplicacaoUsuario _aplicacaoUsuario;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UsuarioController(IAplicacaoUsuario aplicacaoUsuario, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _aplicacaoUsuario = aplicacaoUsuario;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [Produces("application/json")] //Declarando que o método CriarTokenJWT() retorna respostas no formato JSON

        [HttpPost("CriarToken")]
        public async Task<IActionResult> CriarTokenJWT([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Senha))
                return Unauthorized();

            var result = await _aplicacaoUsuario.ExisteUsuario(login.Email, login.Senha);

            if (result)
            {
                var token = new TokenJWTBuilder()
                     .AddSecurityKey(JwtSecurityKey.Create("43443FDFDF34DF34343fdf344SDFSDFSDFSDFSDF4545354345SDFGDFGDFGDFGdffgfdGDFGDGR%"))
                 .AddSubject("Aprendizado API DDD para Angular")
                 .AddIssuer("Esdras.Securiry.Bearer")
                 .AddAudience("Publico.Securiry.Bearer")
                 .AddClaim("UsuarioAPI", "1")
                 .AddExpiry(5)
                 .Builder();

                return Ok(token.value);
            }

            else return Unauthorized();

        }


        //Senha do usuário não vai criptografada "naturalmente", mas se usar Identity vai(Analise o AdicionarUsuarioIdentity pois a senha vai pro banco já criptografada)
        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("AdicionarUsuario")]
        public async Task<IActionResult> AdicionarUsuario([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Senha))
                return Unauthorized();

            var result = await _aplicacaoUsuario.AdicionarUsuario(login.Email, login.Senha, login.Idade, login.Celular);

            if (!result) return Ok("Erro ao adicionar o usuário!");
            else return Ok("Usuário adicionado com sucesso!");
        }


        // ACIMA, CRIAMOS MÉTODOS "MANUAIS" PARA CRIAR TOKEN E ADICIONAR USUÁRIO, EMBORA ISSO POSSA SER FEITO PELO PRÓPRIO IDENTITY
        // ABAIXO VAMOS CRIAR OS MÉTODOS ACIMA, MAS APROVEITANDO O PRÓPRIO IDENTITY PARA TAL, POR MEIO DO UserManager e SingInManager

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CriarTokenIdentity")]
        public async Task<IActionResult> CriarTokenIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Senha))
                return Unauthorized();

            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Senha, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("43443FDFDF34DF34343fdf344SDFSDFSDFSDFSDF4545354345SDFGDFGDFGDFGdffgfdGDFGDGR%"))
                .AddSubject("Aprendizado API DDD para Angular")
                .AddIssuer("Esdras.Securiry.Bearer")
                .AddAudience("Publico.Securiry.Bearer")
                .AddClaim("UsuarioAPI", "1")
                .AddExpiry(5)
                .Builder();

                return Ok(token.value);
            }
            else return Unauthorized();
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("AdicionarUsuarioIdentity")]
        public async Task<IActionResult> AdicionarUsuarioIdentity([FromBody] Login login) {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Senha))
                return Unauthorized();

            var user = new ApplicationUser
            {
                UserName = login.Email,
                Email = login.Email,
                Celular = login.Celular,
                TipoUsuario = ETipoUsuario.Comum,
            };
            //Cria o usuário no banco com senha já criptografada
            var result = await _userManager.CreateAsync(user, login.Senha);
            

            if (result.Errors.Any())
            {
                return Ok(result.Errors);
            }


            // Geração de Confirmação caso precise(Opcional - Para se ter a confirmação que o usuário foi criado)
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // retorno email 
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var resultado2 = await _userManager.ConfirmEmailAsync(user, code);

            if (resultado2.Succeeded)
                return Ok("Usuário Adicionado com Sucesso");
            else
                return Ok("Erro ao confirmar usuários");



        }

    }
}
