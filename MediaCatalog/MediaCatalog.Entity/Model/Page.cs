using System;
using System.Collections.Generic;
using System.Text;

namespace MediaCatalog.Entity.Model
{
    public class Page<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Content { get; set; }
    }
}
