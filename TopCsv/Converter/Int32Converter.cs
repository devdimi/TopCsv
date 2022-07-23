using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopCsvProject
{
    public class Int32Converter : IConverter<Int32>
    {
        public Int32 FromStringTyped(ReadOnlySpan<char> input)
        {
            return Int32.Parse(input);
        }

        object IConverter.FromString(ReadOnlySpan<char> input)
        {
            return this.FromStringTyped(input);
        }

        object IConverter.Default => 0;
    }
}
