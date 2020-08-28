using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MvcEducation.Domain.Interfaces
{
    public interface IPaginatedQuery<TSource, DTO> : IQuery<TSource, DTO>
    {
        int? PageNumber { get; set; }
        int PageSize { get; set; }
        int Skip { get; set; }
    }
}
