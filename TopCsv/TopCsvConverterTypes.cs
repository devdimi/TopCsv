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
        DecimalConverterComma,

        /// <summary> 123.45 </summary>
        DecimalConverterDot,

        /// <summary> "USD 124.45" or "EUR 123.32" </summary>
        MoneyConverterCurrencyDot,

        /// <summary> Convert Date in format '29-06-2022' </summary>
        DateConverterDD_MM_YYYY,

        /// <summary> Convert Date in format '2021-07-05 14:04:38' </summary>
        DateConverterYYYY_MM_DD_HH_mm_SS,

        TimeConverter,

        CurrencyEnumConverter
    }
}
