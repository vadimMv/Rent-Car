using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAL
{
    /// <summary>
    /// repository interface 
    /// </summary>
    /// <typeparam name="T">model object</typeparam>
    public interface IRepository<T>: IDisposable
        where T : class
    {
         void Create(T entity);
         void Delete(T entity);
         void Update(T entity);
         void Save();
         IEnumerable<T> SearchFor(Expression<Func<T, bool>> predicate);
         IEnumerable<T> GetAll();
     

    }
}
