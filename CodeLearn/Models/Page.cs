using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Models
{
    public class Page<T>
    {
        public IList<T> Items { get; set; } = new List<T>();

        public int Size { get; set; } = 0;

        public int CurrentPage { get; set; } = 0;

        public int PageCount { get; set; } = 0;
    }
}
