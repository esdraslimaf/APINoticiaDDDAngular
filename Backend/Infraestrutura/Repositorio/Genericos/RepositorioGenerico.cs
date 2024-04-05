using Dominio.Interfaces.Genericos;
using Infraestrutura.Configuracoes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infraestrutura.Repositorio.Genericos
{
    public class RepositorioGenerico<T> : IGenericos<T> where T : class
    {
        private readonly DbContextOptions<MyContext> _OptionsBuilder;

        /* No construtor estamos criando uma instância de DbContextOptions<MyContext>,
         que contém as opções de configuração necessárias para criar um novo contexto de banco de dados.
        ele será criado mais tarde, quando uma operação que requer acesso ao banco de dados for executada.
        */
        public RepositorioGenerico()
        {
            //O contexto será criado com as configurações abaixo, diferentemente de quando passamos via IoC o contexto já criado
            _OptionsBuilder = new DbContextOptions<MyContext>();
        }

        public async Task Adicionar(T obj)
        {
            using (var data = new MyContext(_OptionsBuilder))
            {
                await data.Set<T>().AddAsync(obj);
                await data.SaveChangesAsync();
            }
        }

        public async Task Atualizar(T obj)
        {
            using (var data = new MyContext(_OptionsBuilder))
            {
                data.Set<T>().Update(obj);
                await data.SaveChangesAsync();
            }
        }

        public async Task<T> ObterPorId(int id)
        {
            using (var data = new MyContext(_OptionsBuilder))
            {
                return await data.Set<T>().FindAsync(id);
            }
        }

        public async Task<IEnumerable<T>> ObterTodos()
        {
            using (var data = new MyContext(_OptionsBuilder))
            {
                return await data.Set<T>().AsNoTracking().ToListAsync();
            }

            /* Quando você chama o método AsNoTracking, você está instruindo o Entity Framework Core a não rastrear as entidades recuperadas da consulta. Isso significa que o contexto não manterá essas entidades em seu estado rastreado e não as monitorará para detectar mudanças. Isso pode resultar em uma redução significativa na quantidade de memória utilizada pelo contexto e na melhoria do desempenho da consulta. */
        }
    

        public async Task Remover(T obj)
        {
            using (var data = new MyContext(_OptionsBuilder))
            {
                data.Set<T>().Remove(obj);
                await data.SaveChangesAsync();
            }
        }
    }
}
