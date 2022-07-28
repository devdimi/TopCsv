using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopCsvProject
{
    public class DateConverterYYYY_MM_DD_HH_mm_SS : IConverter<DateTime>
    {
        public object Default => default(DateTime);

        public object FromString(ReadOnlySpan<char> input) => FromStringTyped(input);

        public DateTime FromStringTyped(ReadOnlySpan<char> input)
        {
            DateTime result;
            if(!DateTime.TryParse(input, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out result))
            {
                /// log
            }

            return result;
        }
    }
}
