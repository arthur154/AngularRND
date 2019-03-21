using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularApp.Models.Search
{
    public class SearchRequest
    {
        public List<Column> columns;
        public int draw;
        public int length;
        public List<OrderParameter> order;
        public SearchParameter search;
        public int start;
    }
}
