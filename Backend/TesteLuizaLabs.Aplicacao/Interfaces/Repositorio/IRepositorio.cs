using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TesteLuizaLabs.Aplicacao.Interfaces.Repositorio
{
    public interface IRepositorio<T> where T : IEntidade
    {
        Task<T> Adicionar(T entity);
        IEnumerable<T> PesquisarTodos();
        Task<T> Alterar(T entity);
        Task<IQueryable<T>> PesquisarPor(Expression<Func<T, bool>> predicado, params Expression<Func<T, object>>[] inclusoes);
    }
}