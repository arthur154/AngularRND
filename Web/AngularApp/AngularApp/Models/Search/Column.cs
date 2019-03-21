using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularApp.Models.Search
{
    public class Column
    {
        public string data;
        public string name;
        public bool searchable;
        public bool orderable;
        public SearchParameter search;
    }
}
