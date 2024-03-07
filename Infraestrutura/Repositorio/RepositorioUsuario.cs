using Dominio.Interfaces;
using Entidades.Entidades;
using Infraestrutura.Configuracoes;
using Infraestrutura.Repositorio.Genericos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorio
{
    public class RepositorioUsuario : RepositorioGenerico<ApplicationUser>, IUsuario
    {
        private readonly DbContextOptions<MyContext> _OptionsBuilder;
        public RepositorioUsuario()
        {
            _OptionsBuilder = new DbContextOptions<MyContext>();
        }
        /* Estamos passando um objeto DbContextOptions<MyContext> vazio para o construtor MyContext. Isso significa que estamos criando um contexto sem configurações específicas.. Essa abordagem pode ser usada se quisermos usar as configurações padrão definidas no construtor de DbContext da sua classe MyContext. */

        public async Task<bool> AdicionarUsuario(string email, string senha, int idade, string celular)
        {
            try
            {
                using (var db = new MyContext())
                {
                    await db.ApplicationUsers.AddAsync(new ApplicationUser
                    {
                        Email = email,
                        PasswordHash = senha,
                        Idade = idade,
                        Celular = celular
                    });
                    await db.SaveChangesAsync();
                    
                }           
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
