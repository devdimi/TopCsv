using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopCsvProject
{
    public class PortfolioEntryDegiro : CsvBaseRecord
    {
        [CsvField(Header = "Produkt", Converter = TopCsvConverterTypes.StringConverter)]
        public String? Product { get; set; }

        [CsvField(Header = "Symbol/ISIN", Converter = TopCsvConverterTypes.StringConverter)]
        public String? SymbolISIN { get; set; }

        [CsvField(Header = "Anzahl", Converter = TopCsvConverterTypes.IntConverter, AllowEmpty = true)]
        public Int32 Number { get; set; }

        [CsvField(
            Header = "Schlußkurs",
            Converter = TopCsvConverterTypes.DecimalConverterComma,
            AllowEmpty = true)]
        public decimal ClosingPrice { get; set; }

        [CsvField(
            Header = "Wert",
            Converter = TopCsvConverterTypes.MoneyConverterCurrencyDot,
            AllowEmpty = true)]
        public Money MoneyAmount { get; set; }

        [CsvField(
            Header = "Wert in EUR",
            Converter = TopCsvConverterTypes.DecimalConverterComma,
            AllowEmpty = true)]
        public decimal ValueInEUR { get; set; }
    }
}
