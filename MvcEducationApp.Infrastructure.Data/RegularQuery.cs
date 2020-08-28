using Microsoft.EntityFrameworkCore.Query;
using MvcEducation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MvcEducationApp.Infrastructure.Data
{
    public class RegularQuery<T, TResult> : IQuery<T, TResult>
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public Func<IQueryable<T>, IIncludableQueryable<T, object>> Include { get; set; }
        public Expression<Func<T, TResult>> Selector { get; set; }

        public RegularQuery(
            Expression<Func<T, bool>> Criteria,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> Include,
            Expression<Func<T, TResult>> Selector
        )
        {
            this.Criteria = Criteria;
            this.Include = Include;
            this.Selector = Selector;
        }
    }
}
