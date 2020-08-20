using System;
using System.Collections.Generic;
using System.Text;

namespace MvcEducation.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        ICourseRepository GetCourseRepository();
        IUserCourseRepository GetUserCourseRepository();
        void Save();
    }
}
