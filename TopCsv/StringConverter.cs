using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopCsvProject
{
    public class StringConverter : IConverter<String>
    {
        public String FromStringTyped(String input) => input.TrimStart('"', ' ').TrimEnd('"', ' ');

        object IConverter.FromString(string input) => input.TrimStart('"', ' ').TrimEnd('"', ' ');

        object IConverter.Default => String.Empty;
    }
}
