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
        public async Task AdicionarNoticia(Noticia noticia)
        {
            throw new NotImplementedException();
        }

        public Task AtualizarNoticia(Noticia noticia)
        {
            throw new NotImplementedException();
        }

        public Task<Noticia> ObterNoticiaPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Noticia>> ObterTodasNoticias()
        {
            throw new NotImplementedException();
        }

        public Task RemoverNoticia(Noticia noticia)
        {
            throw new NotImplementedException();
        }
    }
}
