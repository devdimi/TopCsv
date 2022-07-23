using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopCsvProject
{
    public class StringConverter : IConverter<String>
    {
        public String FromStringTyped(ReadOnlySpan<char> input) => input.ToString();

        object IConverter.FromString(ReadOnlySpan<char> input) => input.ToString();

        object IConverter.Default => String.Empty;
    }
}
