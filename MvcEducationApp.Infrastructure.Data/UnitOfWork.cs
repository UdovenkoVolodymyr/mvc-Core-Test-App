using MvcEducation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MvcEducationApp.Infrastructure.Data
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private bool _disposed;
        private Dictionary<string, object> _repositories;
        private MvcEducationDBContext _context;

        public UnitOfWork(MvcEducationDBContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed == false)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity: class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var entityName = typeof(TEntity).Name;

            if (_repositories.TryGetValue(entityName, out var repository) == false) {
                EFGenericRepository<TEntity> repositoryInstance = new EFGenericRepository<TEntity>(_context);
                _repositories.Add(entityName, repositoryInstance);
                return (IGenericRepository<TEntity>)_repositories[entityName];
            } 
            return (IGenericRepository<TEntity>) repository;
        }
    }
}
