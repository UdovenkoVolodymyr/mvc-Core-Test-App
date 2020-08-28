using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MvcEducation.Domain.Interfaces
{
    public interface IQuery<TEntity,TResult>
    {
        Expression<Func<TEntity, bool>> Criteria { get; }
        Expression<Func<TEntity, TResult>> Selector { get; }
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> Include { get; }
    }
}
