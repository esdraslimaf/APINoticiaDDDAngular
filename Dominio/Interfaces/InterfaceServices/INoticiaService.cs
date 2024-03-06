using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServices
{
        public interface INoticiaService
        {
            Task AdicionarNoticia(Noticia noticia);
            Task<Noticia> ObterNoticiaPorId(int id);
            Task<IEnumerable<Noticia>> ObterTodasNoticias();
            Task AtualizarNoticia(Noticia noticia);
            Task RemoverNoticia(Noticia noticia);
        }
   
}
