using Aplicacao.Interfaces;
using Entidades.Entidades;
using Entidades.Notificacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticiaController : ControllerBase
    {
        private readonly IAplicacaoNoticia _aplicacaoNoticia;
        public NoticiaController(IAplicacaoNoticia aplicacaoNoticia)
        {
                _aplicacaoNoticia = aplicacaoNoticia;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("ListarNoticias")]
        public async Task<IEnumerable<Noticia>> ListarNoticias()
        {
           return await _aplicacaoNoticia.ObterTodasNoticiasAtivas();
        }


        [Authorize]
        [Produces("application/json")]
        [HttpPost("AdicionarNoticia")]
        public async Task<List<Notifica>> AdicionarNoticia(NoticiaModel noticia)
        {
            var noticiaNova = new Noticia();
            noticiaNova.Titulo = noticia.Titulo;
            noticiaNova.Informacao = noticia.Informacao;
            noticiaNova.ApplicationUserId = await RetornarIdUsuarioLogado();
            await _aplicacaoNoticia.AdicionarNoticia(noticiaNova);
            return noticiaNova.Notificacoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("AtualizarNoticia")]
        public async Task<List<Notifica>> AtualizarNoticia(NoticiaModel noticia)
        {
            var noticiaNova = await _aplicacaoNoticia.ObterPorId(noticia.IdNoticia);
            noticiaNova.Titulo = noticia.Titulo;
            noticiaNova.Informacao = noticia.Informacao;
            noticiaNova.ApplicationUserId = await RetornarIdUsuarioLogado();
            await _aplicacaoNoticia.AtualizarNoticia(noticiaNova);
            return noticiaNova.Notificacoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("ExcluirNoticia")]
        public async Task<List<Notifica>> ExcluirNoticia(NoticiaModel noticia)
        {
            var noticiaNova = await _aplicacaoNoticia.ObterPorId(noticia.IdNoticia);

            await _aplicacaoNoticia.Remover(noticiaNova);
            return noticiaNova.Notificacoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("BuscarNoticiaPorId")]
        public async Task<Noticia> BuscarNoticiaPorId(NoticiaModel noticia)
        {
            var noticiaNova = await _aplicacaoNoticia.ObterPorId(noticia.IdNoticia);
            return noticiaNova;
        }

        private async Task<string> RetornarIdUsuarioLogado()
        {
            if (User != null)
            {
                var idUsuario = User.FindFirst("UsuarioId");
                return idUsuario.Value;
            }
            else
            {
               return string.Empty;
            }
        }
    }
}
