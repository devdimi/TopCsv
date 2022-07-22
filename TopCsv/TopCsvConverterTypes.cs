using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopCsvProject
{
    public enum TopCsvConverterTypes
    {
        None,

        IntConverter,

        StringConverter,

        /// <summary> 123,45 </summary>
        MoneyConverterNoCurrencyComma,

        /// <summary> "USD 124.45" or "EUR 123.32" </summary>
        MoneyConverterCurrencyDot,

        /// <summary> Convert Date in format '29-06-2022' </summary>
        DateConverterDD_MM_YYYY,
    }
}
