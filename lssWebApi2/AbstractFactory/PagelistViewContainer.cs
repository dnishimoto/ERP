using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.AbstractFactory
{
    public class PageListViewContainer<T> : AbstractViewContainer where T : class
    {

        public PageListViewContainer()
        {
            Items = new List<T>();
        }
        public List<T> Items { get; set; }

    }
}
