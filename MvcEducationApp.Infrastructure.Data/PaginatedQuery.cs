using Microsoft.EntityFrameworkCore.Query;
using MvcEducation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MvcEducationApp.Infrastructure.Data
{
    public class PaginatedQuery<T, TResult> : IPaginatedQuery<T, TResult>
    {
        public int? PageNumber { get; set; }
        public int PageSize { get; set; }

        public Expression<Func<T, bool>> Criteria { get; set; }
        public Func<IQueryable<T>, IIncludableQueryable<T, object>> Include { get; set; }
        public Expression<Func<T, TResult>> Selector { get; set; }
        public int Skip { get; set; }


        public PaginatedQuery(int? PageNumber, int PageSize,
            Expression<Func<T, bool>> Criteria,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> Include,
            Expression<Func<T, TResult>> Selector)
        {
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
            this.Criteria = Criteria;
            this.Include = Include;
            this.Selector = Selector;
            Skip = ((PageNumber ?? 1) - 1) * PageSize;
        }
    }
}
