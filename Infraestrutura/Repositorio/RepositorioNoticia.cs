using Dominio.Interfaces;
using Entidades.Entidades;
using Infraestrutura.Configuracoes;
using Infraestrutura.Repositorio.Genericos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorio
{
    public class RepositorioNoticia : RepositorioGenerico<Noticia>, INoticia
    {
        private readonly DbContextOptions<MyContext> _OptionsBuilder;
        public RepositorioNoticia()
        {
            _OptionsBuilder = new DbContextOptions<MyContext>();
        }
        /* Estamos passando um objeto DbContextOptions<MyContext> vazio para o construtor MyContext. Isso significa que estamos criando um contexto sem configurações específicas.. Essa abordagem pode ser usada se quisermos usar as configurações padrão definidas no construtor de DbContext da sua classe MyContext. */

        public async Task<IEnumerable<Noticia>> ListarNoticias(Expression<Func<Noticia, bool>> exNoticia)
        {
            using (var db = new MyContext(_OptionsBuilder))
            {
                return await db.Noticias.Where(exNoticia).AsNoTracking().ToListAsync(); 
            }
        }
    }
}
