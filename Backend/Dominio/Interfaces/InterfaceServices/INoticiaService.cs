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
            Task<IEnumerable<Noticia>> ObterTodasNoticiasAtivas();
            Task AtualizarNoticia(Noticia noticia);
        }
   
}
