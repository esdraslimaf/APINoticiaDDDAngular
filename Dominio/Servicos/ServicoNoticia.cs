using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServices;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicos
{
    public class ServicoNoticia : INoticiaService
    {
        private readonly INoticia _reponoticia;
        public ServicoNoticia(INoticia reponoticia)
        {
            _reponoticia = reponoticia;
        }
        public async Task AdicionarNoticia(Noticia noticia)
        {
            var validarTitulo = noticia.ValidarPropriedadeString(noticia.Titulo, "Titulo");
            var validarInformacao = noticia.ValidarPropriedadeString(noticia.Informacao, "Informacao");
            if(validarTitulo && validarInformacao)
            {
                noticia.DataCadastro = DateTime.Now;
                noticia.DataAlteracao = DateTime.Now;
                noticia.Ativo = true;
                await _reponoticia.Adicionar(noticia);
            }
        }

        public async Task AtualizarNoticia(Noticia noticia)
        {
            var validarTitulo = noticia.ValidarPropriedadeString(noticia.Titulo, "Titulo");
            var validarInformacao = noticia.ValidarPropriedadeString(noticia.Informacao, "Informacao");
            if (validarTitulo && validarInformacao)
            {
             // noticia.DataCadastro = DateTime.Now;
                noticia.DataAlteracao = DateTime.Now;
                noticia.Ativo = true;
                await _reponoticia.Atualizar(noticia);
            }
        }

        public Task<Noticia> ObterNoticiaPorId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Noticia>> ObterTodasNoticias()
        {
            return await _reponoticia.ListarNoticias(n=>n.Ativo);
        }

        public Task RemoverNoticia(Noticia noticia)
        {
            throw new NotImplementedException();
        }
    }
}
