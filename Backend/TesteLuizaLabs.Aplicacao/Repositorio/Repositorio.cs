using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TesteLuizaLabs.Aplicacao.Entidades;
using TesteLuizaLabs.Aplicacao.Repositorio.Contexto;

namespace TesteLuizaLabs.Aplicacao.Repositorio
{
    public abstract class Repositorio<T> where T : Entidade
    {
        protected EntityFrameworkContext _contexto;

        public Repositorio(EntityFrameworkContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<T> Adicionar(T entity)
        {
            _contexto.Set<T>().Add(entity);
            await _contexto.SaveChangesAsync();
            return entity;
        }

        public IEnumerable<T> PesquisarTodos()
        {
            return _contexto.Set<T>();
        }

        public async Task<T> Alterar(T entity)
        {
            var entry = _contexto.Entry(entity);
            _contexto.Set<T>().Attach(entity);
            entry.State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<IQueryable<T>> PesquisarPor(Expression<Func<T, bool>> predicado, params Expression<Func<T, object>>[] inclusoes)
        {
            IQueryable<T> query = _contexto.Set<T>();

            foreach (var item in inclusoes)
            {
                query = query.Include(item);
            }

            query = query.Where(predicado);

            return query;
        }
    }
}
