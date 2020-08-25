using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEducationApp.Domain.Core.Models
{
    public class PageViewModel<TEntity> where TEntity : class
    {
        public List<TEntity> Data;
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }
    
        public PageViewModel(int count, int pageNumber, int pageSize, List<TEntity> Data)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.Data = Data;
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
}
