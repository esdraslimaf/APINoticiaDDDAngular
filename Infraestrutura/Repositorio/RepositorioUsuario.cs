using Dominio.Interfaces;
using Entidades.Entidades;
using Entidades.Enums;
using Infraestrutura.Configuracoes;
using Infraestrutura.Repositorio.Genericos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
        } // Esse código seria desnecessário devido ao OnConfiguring do MyContext, além de que o OptionsBuilder criado acima é sem configs
        /* Estamos passando um objeto DbContextOptions<MyContext> vazio para o construtor MyContext. Isso significa que estamos criando um contexto sem configurações específicas.. Essa abordagem pode ser usada se quisermos usar as configurações padrão definidas no construtor de DbContext da sua classe MyContext. */

        public async Task<bool> AdicionarUsuario(string email, string senha, int idade, string celular)
        {
            try
            {
                using (var db = new MyContext(_OptionsBuilder))
                {
                    await db.ApplicationUsers.AddAsync(new ApplicationUser
                    {
                        Email = email,
                        PasswordHash = senha,
                        Idade = idade,
                        Celular = celular,
                        TipoUsuario = ETipoUsuario.Comum
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

        public async Task<bool> ExisteUsuario(string email, string senha)
        {
            try
            {
                using (var db = new MyContext(_OptionsBuilder))
                {
                    return await db.ApplicationUsers
                        .AsNoTracking()
                        .AnyAsync(obj => obj.Email == email && obj.PasswordHash == senha);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> RetornaIdUsuario(string email)
        {
            try
            {
                using (var db = new MyContext(_OptionsBuilder))
                {
                    var result = await db.ApplicationUsers
                        .AsNoTracking()
                        .FirstOrDefaultAsync(obj => obj.Email == email);

                    return result.Id;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
