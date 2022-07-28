using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopCsvProject
{
    public class ActivityTrading212 : CsvBaseRecord
    {
        [CsvField(Header = "Action", AllowEmpty = false, Converter = TopCsvConverterTypes.StringConverter)]
        public String Action { get; set; }

        [CsvField(Header = "Time", AllowEmpty = false, Converter = TopCsvConverterTypes.DateConverterYYYY_MM_DD_HH_mm_SS)]
        public DateTime Time { get; set; }

        [CsvField(Header = "ISIN", AllowEmpty = true, Converter = TopCsvConverterTypes.StringConverter)]
        public String ISIN { get; set; }

        [CsvField(Header = "Ticker", AllowEmpty = true, Converter = TopCsvConverterTypes.StringConverter)]
        public String Ticker { get; set; }

        [CsvField(Header = "Name", AllowEmpty = true, Converter = TopCsvConverterTypes.StringConverter)]
        public String Name { get; set; }

        [CsvField(Header = "No. of shares", AllowEmpty = true, Converter = TopCsvConverterTypes.DecimalConverterDot)]
        public String NumShares { get; set; }

        [CsvField(Header = "Price / share", AllowEmpty = true, Converter = TopCsvConverterTypes.DecimalConverterDot)]
        public String PricePerShare { get; set; }

        
        [CsvField(Header = "Currency (Price / share)", AllowEmpty = true, Converter = TopCsvConverterTypes.CurrencyEnumConverter)]
        public Currency CurrencyPricePerShare { get; set; }

        [CsvField(Header = "Exchange rate", AllowEmpty = true, Converter = TopCsvConverterTypes.CurrencyEnumConverter)]
        public Currency ExchangeRate { get; set; }

        [CsvField(Header = "Total (EUR)", AllowEmpty = true, Converter = TopCsvConverterTypes.DecimalConverterDot)]
        public Currency TotalEUR { get; set; }


    }
}
