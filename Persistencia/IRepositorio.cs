using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Persistencia
{
    public interface IRepository<T> where T : class
    {
        IList<T> Listar();

        T Obter(Expression<Func<T, bool>> predicate);

        T Obter(object id);

        T Criar(T entity);

        T Atualizar(T entity);

        void Remover(T entity);

    }
}
