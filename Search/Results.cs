using System;
using System.Collections.Generic;
using System.Text;

namespace Svinx.FindMe.Libraries.Search
{
    public class Results
    {
        public string Term;
        public long Count;
        public IEnumerable<Result> Items;
    }
}
