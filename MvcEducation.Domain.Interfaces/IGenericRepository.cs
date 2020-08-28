using MvcEducationApp.Domain.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MvcEducation.Domain.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Create(TEntity item);
        TEntity FindById(int id);
        TEntity FindById(string id);
        IEnumerable<TEntity> Get();
        void Remove(TEntity item);
        void RemoveById(int id);
        void Update(TEntity item);
        IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity GetById<TResult>(IQuery<TEntity, TResult> query);
        TResult GetByIdWithSelect<TResult>(IQuery<TEntity, TResult> query);
        IEnumerable<TEntity> GetList<TResult>(IQuery<TEntity, TResult> query);
        IEnumerable<TResult> GetListWithSelect<TResult>(IQuery<TEntity, TResult> query);
        PageViewModel<TEntity> GetPaginatedList<TResult>(IPaginatedQuery<TEntity, TResult> query);
        PageViewModel<TResult> GetPaginatedListWithSelect<TResult>(IPaginatedQuery<TEntity, TResult> query) where TResult : class;
    }
}
