using Aplicacao.Interfaces.Genericos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IAplicacaoNoticia:IGenericoAplicacao<Noticia>
    {
        Task AdicionarNoticia(Noticia noticia);
        Task<IEnumerable<Noticia>> ObterTodasNoticiasAtivas();
        Task AtualizarNoticia(Noticia noticia);
        //Assinatura de metodos de servico e repositórios a serem utilizados na aplicação
    }
}
