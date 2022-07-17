using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopCsvProject
{
    public class Int32Converter : IConverter<Int32>
    {
        public Int32 FromStringTyped(String input)
        {
            return Int32.Parse(input);
        }

        object IConverter.FromString(string input)
        {
            return this.FromStringTyped(input);
        }
    }
}
