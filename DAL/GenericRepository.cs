using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// comman class data accsess realization
    /// </summary>
    /// <typeparam name="C">Context etityframework obj</typeparam>
    /// <typeparam name="T">model object</typeparam>
    public abstract class GenericRepository<C, T> :
    IRepository<T> where T : class where C : DbContext, new()
    {

        private C _entities = new C();
        public C Context
        {

            get { return _entities; }
            set { _entities = value; }
        }
        /// <summary>
        /// get all rows 
        /// </summary>
        /// <returns>rows collection</returns>
        public virtual IEnumerable<T> GetAll()
        {

            IEnumerable<T> query = _entities.Set<T>().AsEnumerable();
            return query;
        }
        /// <summary>
        /// search by filter 
        /// </summary>
        /// <param name="predicate">filter lamda-function/delegete</param>
        /// <returns></returns>
        public IEnumerable<T> SearchFor(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {

            IEnumerable<T> query = _entities.Set<T>().Where(predicate);
            return query;
        }
        /// <summary>
        /// add row to table
        /// </summary>
        /// <param name="entity">model object</param>
        public virtual void Create(T entity)
        {
            _entities.Set<T>().Add(entity);
        }
        /// <summary>
        /// delete row from tale
        /// </summary>
        /// <param name="entity">model object</param>
        public virtual void Delete(T entity)
        {
            _entities.Set<T>().Remove(entity);
        }
        /// <summary>
        /// editing row  
        /// </summary>
        /// <param name="entity">edited model object</param>
        public virtual void Update(T entity)
        {
           
            _entities.Entry(entity).State = EntityState.Modified; 
            Save();
        }
        /// <summary>
        /// sync entity framework with Sql server data base
        /// </summary>
        public virtual void Save()
        {
            _entities.SaveChanges();
        }

       /// <summary>
       /// IDisposable interface realization
       /// </summary>

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                   Context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~GenericRepository()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }

       
        #endregion


    }
}
