using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopCsvProject
{
    public class CurrencyEnumConverter : IConverter<Currency>
    {
        public object Default => Currency.Unknown;

        public object FromString(ReadOnlySpan<char> input) => FromStringTyped(input);

        public Currency FromStringTyped(ReadOnlySpan<char> input)
        {

            object currencyObj;
            if (!Enum.TryParse(typeof(Currency), input, out currencyObj))
            {
                // log
            }

            Currency currency = (Currency) currencyObj;
            return currency;
        }
    }
}
