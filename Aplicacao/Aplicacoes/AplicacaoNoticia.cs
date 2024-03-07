using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServices;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Aplicacoes
{
    public class AplicacaoNoticia : IAplicacaoNoticia
    {
         INoticia _INoticia; // Dominio repositorio
         INoticiaService _IServicoNoticia; //Dominio servico
        public AplicacaoNoticia(INoticia INoticia, INoticiaService INoticiaService)
        {
            _INoticia = INoticia;
            _IServicoNoticia = INoticiaService;
        }

        public async Task AdicionarNoticia(Noticia noticia)
        {
           await _IServicoNoticia.AdicionarNoticia(noticia);
        }

        public async Task AtualizarNoticia(Noticia noticia)
        {
            await _IServicoNoticia.AtualizarNoticia(noticia);
        }

        public async Task<IEnumerable<Noticia>> ObterTodasNoticiasAtivas()
        {
           return await _IServicoNoticia.ObterTodasNoticiasAtivas();
        }

        public async Task Adicionar(Noticia obj)
        {
            await _INoticia.Adicionar(obj);
        }

      
        public async Task Atualizar(Noticia obj)
        {
            await _INoticia.Atualizar(obj);
        }

        

        public async Task<Noticia> ObterPorId(int id)
        {
            return await _INoticia.ObterPorId(id);
        }   

        public async Task<IEnumerable<Noticia>> ObterTodos()
        {
           return await _INoticia.ObterTodos();
        }

        public async Task Remover(Noticia obj)
        {
            await _INoticia.Remover(obj);
        }
    }
}
