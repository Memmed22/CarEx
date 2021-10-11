using CarEx.Core.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CarEx.Business.Repository
{
    public interface IRepository<T> where T:class, IEntity
    {
        T Get(int id);

        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null);

        T FirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null);

        void Add(T entity);
        void Remove(T entity);

        void Remove(int id);
        void Update(T entity);
    }
}
