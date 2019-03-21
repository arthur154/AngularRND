using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularApp.Models.Search
{
    public class SearchResults
    {
        public int draw;
        public int recordsTotal;
        public int recordsFiltered;
        public object[] data;
    }
}
