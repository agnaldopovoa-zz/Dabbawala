using Microsoft.EntityFrameworkCore;
using Persistencia.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;


namespace Persistencia
{
    public class DALControl<T> : IDisposable, IRepository<T> where T : class
    {
        private dabbawalaContext Contexto { get; set; }

        public DALControl()
        {
            if (Contexto == null || Contexto.Database.CurrentTransaction == null)
            {
                this.Contexto = new dabbawalaContext();
            }
        }

        public virtual IList<T> Listar()
        {
            VerificarContexto();

            IQueryable<T> dbQuery = Contexto.Set<T>();

            return dbQuery.ToList();
        }

        public virtual IList<T> Listar(Expression<Func<T, bool>> predicate)
        {
            VerificarContexto();

            List<T> list;

            IQueryable<T> dbQuery = Contexto.Set<T>();

            list = dbQuery
                .Where(predicate)
                .ToList<T>();

            return list;
        }

        public virtual IList<T> ListarNoTracking(Expression<Func<T, bool>> predicate)
        {
            VerificarContexto();

            List<T> list;

            IQueryable<T> dbQuery = Contexto.Set<T>();

            list = dbQuery
                .AsNoTracking()
                .Where(predicate)
                .ToList<T>();

            return list;
        }

        public virtual T Obter(Expression<Func<T, bool>> predicate)
        {
            VerificarContexto();

            T item = null;

            IQueryable<T> dbQuery = Contexto.Set<T>();

            //Apply eager loading

            //dbQuery = dbQuery.Include<T, bool>(predicate);

            item = dbQuery //Don't track any changes for the selected item
                .FirstOrDefault(predicate); //Apply where clause

            return item;
        }

        public virtual T ObterNoTracking(Expression<Func<T, bool>> predicate)
        {
            VerificarContexto();

            T item = null;

            IQueryable<T> dbQuery = Contexto.Set<T>();

            item = dbQuery
                .AsNoTracking()//Don't track any changes for the selected item
                .FirstOrDefault(predicate); //Apply where clause

            return item;
        }

        public virtual T Obter(object id)
        {
            VerificarContexto();

            DbSet<T> dbQuery = Contexto.Set<T>();

            return dbQuery.Find(id);
        }

        /// <summary>
        /// Verifica se o contexto está null.
        /// </summary>
        /// <exception cref="ApplicationException: O Contexto está nulo! Verifique se você está instanciando uma 
        /// nova BM antes de chamar seu método."></exception>
        private void VerificarContexto()
        {
            if (Contexto == null)
            {
                throw new ApplicationException("O Contexto está nulo. Verifique se você está instanciando uma nova BM antes de chamar seu método!");
            }
        }

        public virtual T Criar(T entity)
        {
            VerificarContexto();

            Contexto.Set<T>().Add(entity);

            if (Contexto.Database.CurrentTransaction == null)
            {
                this.SaveChanges();
                this.Dispose();
            }

            return entity;
        }

        public virtual T AdcionarNoContexto(T entity)
        {
            return Criar(entity);
        }

        public virtual IList<T> AdcionarNoContexto(List<T> lstEntity)
        {
            return Criar(lstEntity);
        }

        public virtual IList<T> Criar(List<T> lstEntity)
        {
            VerificarContexto();
            //Contexto.Configuration.AutoDetectChangesEnabled = false;

            for (int i = 0; i < lstEntity.Count; i++)
            {
                //Contexto.Entry(item).State = EntityState.Added;
                Contexto.Set<T>().Add(lstEntity[i]);
            }

            //IList<T> itemsCreated = Contexto.GetChanges().GetInserts<T>();

            //Contexto.FlushChanges();

            if (Contexto.Database.CurrentTransaction == null)
            {
                this.SaveChanges();
                this.Dispose();
            }

            return lstEntity;
        }

        public virtual void Remover(T entity)
        {
            VerificarContexto();

            Contexto.Entry(entity).State = EntityState.Deleted;

            if (Contexto.Database.CurrentTransaction == null)
            {
                this.SaveChanges();
                this.Dispose();
            }
        }

        public virtual void RemoverAll(IList<T> lstEntity)
        {
            VerificarContexto();

            foreach (var item in lstEntity)
            {
                Contexto.Entry(item).State = EntityState.Deleted;
            }

            if (Contexto.Database.CurrentTransaction == null)
            {
                this.SaveChanges();
                this.Dispose();
            }
        }

        private void SaveChanges()
        {
            VerificarContexto();

            Contexto.SaveChanges();
        }

        public virtual T Atualizar(T entity)
        {
            VerificarContexto();

            Contexto.Entry(entity).State = EntityState.Modified;

            if (Contexto.Database.CurrentTransaction == null)
            {
                this.SaveChanges();
            }


            return entity;
        }

        public dabbawalaContext db
        {
            get
            {
                return Contexto;
            }
        }

        public void Dispose()
        {
            Contexto.Dispose();
        }

        public void CheckContext()
        {
            if (Contexto == null)
            {
                Contexto = new dabbawalaContext();
            }
        }

        public void ForceSetContext(dabbawalaContext context)
        {
            Contexto = context;
        }

    }
}
