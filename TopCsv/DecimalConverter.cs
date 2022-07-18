using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopCsvProject
{
    public class DecimalConverterComma : IConverter<decimal>
    {
        CultureInfo ci = new CultureInfo("de-DE");

        public object FromString(string input) => this.FromStringTyped(input);

        public decimal FromStringTyped(string input)
        {
            decimal result;
            var style = NumberStyles.Any;
            if (!decimal.TryParse(input, style, this.ci, out result))
            {
                //// logging
            }

            return result;
        }

        object IConverter.Default => 0.0m;
    }
}
