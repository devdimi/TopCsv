using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopCsvProject.Converter
{
    public class DateTimeLineConverter : IFormatProvider
    {
        public object? GetFormat(Type? formatType)
        {
            if(formatType.Name == "DateTimeFormatInfo")
            {
                return "dd-MM-YYYY";
            }

            throw new NotSupportedException();
        }
    }

    public interface ILogger
    {
        void Log(LogFlags flags, String msg);
    }

    [Flags]
    public enum LogFlags
    {
        ParseError,
        DateError
    }

    public class ConsoleLogger : ILogger
    {
        public void Log(LogFlags flags, String msg)
        {
            Console.WriteLine(msg);
        }
    }

    public class DateConverterDD_MM_YYYY : IConverter<DateTime>
    {
        public object Default => default(DateTime);

        CultureInfo cultureInfo;
        ILogger logger;

        public DateConverterDD_MM_YYYY(ILogger logger)
        {
            this.cultureInfo = new CultureInfo("de-DE");
            this.logger = logger;
        }

        public object FromString(ReadOnlySpan<char> input)
        {
            return FromStringTyped(input);
        }

        public DateTime FromStringTyped(ReadOnlySpan<char> input)
        {
            ////"dd-MM-YYYY";
            var daySlice = input.Slice(0, 2);
            var monthSlice = input.Slice(3, 2);
            var yearSlice = input.Slice(6, 4);
            int day, month, year;
            if(!Int32.TryParse(daySlice, out day))
            {
                logger.Log(LogFlags.DateError | LogFlags.ParseError, $"Errror parsing {daySlice.ToString()} from {input}");
                return default(DateTime);
            }

            if(!Int32.TryParse(monthSlice, out month))
            {
                logger.Log(LogFlags.DateError | LogFlags.ParseError, $"Errror parsing {monthSlice.ToString()} from {input}");
                return default(DateTime);
            }

            if (!Int32.TryParse(yearSlice, out year))
            {
                logger.Log(LogFlags.DateError | LogFlags.ParseError, $"Errror parsing {yearSlice.ToString()} from {input}");
                return default(DateTime);
            }
            
            DateTime dateTime = new DateTime(year, month, day);
            return dateTime;
        }
    }
}
