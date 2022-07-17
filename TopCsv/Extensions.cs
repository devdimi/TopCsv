using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopCsvProject
{
    public static class Extensions
    {
        public static void Swap<T>(this IList<T> collection, int x, int y)
        {
            if(x == y)
            {
                return;
            }

            T replace = collection[x];
            collection[x] = collection[y];
            collection[y] = replace;
        }
    }
}
