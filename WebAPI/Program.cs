
using Aplicacao.Aplicacoes;
using Aplicacao.Interfaces;
using Aplicacao.Interfaces.Genericos;
using Dominio.Interfaces;
using Dominio.Interfaces.Genericos;
using Dominio.Interfaces.InterfaceServices;
using Dominio.Servicos;
using Entidades.Entidades;
using Infraestrutura.Configuracoes;
using Infraestrutura.Repositorio;
using Infraestrutura.Repositorio.Genericos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<MyContext>(options =>
             options.UseSqlServer(
                 builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<MyContext>();

            //Interfaces e Repositórios
            builder.Services.AddScoped(typeof(IGenericos<>), typeof(RepositorioGenerico<>));
            builder.Services.AddScoped<INoticia, RepositorioNoticia>();
            builder.Services.AddScoped<IUsuario, RepositorioUsuario>();

            //Interfaces e Serviços - Poderemos posteriormente elaborar um IUsuarioService para utilizar o IUsuario
            builder.Services.AddScoped<INoticiaService, ServicoNoticia>();

            //Interfaces da camada Aplicação(Utiliza Serviços e Repositórios do Domínio/Infra)
            builder.Services.AddScoped<IAplicacaoNoticia, AplicacaoNoticia>();
            builder.Services.AddScoped<IAplicacaoUsuario, AplicacaoUsuario>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}