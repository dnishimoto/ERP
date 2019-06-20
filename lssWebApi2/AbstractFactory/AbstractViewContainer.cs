using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.AbstractFactory
{
    public class AbstractViewContainer
    {
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
            public int TotalItemCount { get; set; }
    }
}
