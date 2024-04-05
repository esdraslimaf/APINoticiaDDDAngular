using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces.Genericos
{
    public interface IGenericoAplicacao<T> where T : class
    {
        Task Adicionar(T obj);
        Task<T> ObterPorId(int id);
        Task<IEnumerable<T>> ObterTodos();
        Task Atualizar(T obj);
        Task Remover(T obj);
    }
}
