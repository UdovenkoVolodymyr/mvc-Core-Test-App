using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using MvcEducation.Domain.Interfaces;
using MvcEducationApp.Domain.Core.Models;

namespace MvcEducationApp.Infrastructure.Data
{
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        MvcEducationDBContext _context;
        DbSet<TEntity> _dbSet;

        public EFGenericRepository(MvcEducationDBContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public TEntity FindById(int id)
        {
            var entity = _dbSet.Find(id);
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public TEntity FindById(string id)
        {
            var entity = _dbSet.Find(id);
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public void Create(TEntity item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remove(TEntity item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }

        public void RemoveById(int id)
        {
            var itemToRemove = _dbSet.Find(id);
            _dbSet.Remove(itemToRemove);
            _context.SaveChanges();
        }

        public TEntity GetById<TResult>(IQuery<TEntity, TResult> query)
        {
            IQueryable<TEntity> returnValue = _dbSet.AsQueryable();

            if (query.Criteria == null)
            {
                throw new Exception("GetById 'query' param must have Criteria !");
            }

            returnValue = query.Include != null ? query.Include(returnValue) : returnValue;
            returnValue = returnValue.Where(query.Criteria);

            return returnValue.FirstOrDefault();
        }
        
        public TResult GetByIdWithSelect<TResult>(IQuery<TEntity, TResult> query)
        {
            IQueryable<TEntity> transitionalResults = _dbSet.AsQueryable();
            IQueryable<TResult> returnValue;

            if (query.Criteria == null)
            {
                throw new Exception("GetById 'query' param must have Criteria !");
            }

            transitionalResults = query.Include != null ? query.Include(transitionalResults) : transitionalResults;
            transitionalResults = transitionalResults.Where(query.Criteria);
            returnValue = transitionalResults.Select(query.Selector);

            return returnValue.FirstOrDefault();
        }

        public IEnumerable<TEntity> GetList<TResult>(IQuery<TEntity, TResult> query)
        {
            IQueryable<TEntity> returnValue = _dbSet.AsQueryable();

            returnValue = query.Include != null ? query.Include(returnValue) : returnValue;
            returnValue = query.Criteria != null ? returnValue.Where(query.Criteria) : returnValue;

            return returnValue.ToList();
        }

        public IEnumerable<TResult> GetListWithSelect<TResult>(IQuery<TEntity, TResult> query)
        {
            IQueryable<TEntity> transitionalResults = _dbSet.AsQueryable();
            IQueryable<TResult> queryResults;

            transitionalResults = query.Include != null ? query.Include(transitionalResults) : transitionalResults;
            transitionalResults = query.Criteria != null ? transitionalResults.Where(query.Criteria) : transitionalResults;
            queryResults = transitionalResults.Select(query.Selector);

            return queryResults;
        }

        public PageViewModel<TEntity> GetPaginatedList<TResult>(IPaginatedQuery<TEntity, TResult> query)
        {
            IQueryable<TEntity> returnValue;

            returnValue = _dbSet.Skip(query.Skip).Take(query.PageSize);
            returnValue = query.Include != null ? query.Include(returnValue) : returnValue;
            returnValue = query.Criteria != null ? returnValue.Where(query.Criteria) : returnValue;

            var entityCount = _dbSet.Count();
            var coursePageView = new PageViewModel<TEntity>(entityCount, query.PageNumber ?? 1, query.PageSize, returnValue.ToList());

            return coursePageView;
        }

        public PageViewModel<TResult> GetPaginatedListWithSelect<TResult>(IPaginatedQuery<TEntity, TResult> query) where TResult : class
        {
            IQueryable<TResult> queryResults;
            IQueryable<TEntity> transitionalResults;

            if (query.Selector == null)
            {
                throw new Exception($"GetPaginatedListWithSelect 'query' param must have Selector !");
            }

            transitionalResults = _dbSet.Skip(query.Skip).Take(query.PageSize);
            transitionalResults = query.Include != null ? query.Include(transitionalResults) : transitionalResults;

            transitionalResults = query.Criteria != null ? transitionalResults.Where(query.Criteria) : transitionalResults;
            queryResults = transitionalResults.Select(query.Selector);

            var entityCount = _dbSet.Count();
            var coursePageView = new PageViewModel<TResult>(entityCount, query.PageNumber ?? 1, query.PageSize, queryResults.ToList());

            return coursePageView;

        }

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
