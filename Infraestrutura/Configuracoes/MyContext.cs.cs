using Entidades.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Configuracoes
{
    public class MyContext : IdentityDbContext<ApplicationUser>
    {
        public MyContext(DbContextOptions<MyContext> options):base(options)
        {
            
        }
        public MyContext() { }

        public DbSet<Noticia> Noticias { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        /* O método OnConfiguring verifica se a cadeia de conexão com o banco de dados foi configurada. Se não estiver configurada previamente, ele a configura automaticamente usando o provedor de banco de dados SQL Server e a cadeia de conexão fornecida. Isso garante que o contexto do banco de dados seja configurado corretamente, mesmo sem configurações explícitas no Program.cs ou Startup.cs. */
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(MinhaStringConexao());
                base.OnConfiguring(optionsBuilder);
            }
        }


        /* protected override void OnModelCreating(ModelBuilder builder)
         {
             builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);

             base.OnModelCreating(builder);
         } */


        public string MinhaStringConexao()
        {
            return "server=.\\SQLEXPRESS2017;Initial Catalog=apidddangular;MultipleActiveResultSets=true;TrustServerCertificate=True;User ID=sa;Password=admin123;";
        }



    }
}
