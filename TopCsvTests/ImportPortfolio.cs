using MinimalFileSystemApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopCsvProject;

namespace TopCsvTests
{
    public class PortfolioEntry : CsvBaseRecord
    {
        [CsvField(Header = "Produkt", Converter = TopCsvConverterTypes.StringConverter)]
        public String Product { get; set; }

        [CsvField(Header = "Symbol/ISIN", Converter = TopCsvConverterTypes.StringConverter)]
        public String SymbolISIN { get; set; }

        [CsvField(Header = "Anzahl", Converter = TopCsvConverterTypes.IntConverter, AllowEmpty = true)]
        public Int32 Number { get; set; }

        [CsvField(
            Header = "Schlußkurs", 
            Converter = TopCsvConverterTypes.MoneyConverterNoCurrencyComma, 
            AllowEmpty = true)]
        public decimal ClosingPrice { get; set; }

        [CsvField(
            Header = "Wert",
            Converter = TopCsvConverterTypes.MoneyConverterCurrencyDot,
            AllowEmpty = true)]
        public Money Value { get; set; }

        [CsvField(
            Header = "Wert in EUR",
            Converter = TopCsvConverterTypes.MoneyConverterNoCurrencyComma,
            AllowEmpty = true)]
        public decimal ValueInEUR { get; set; }
    }

    [TestFixture]
    public class ImportPortfolio
    {
        String[] lines = new[]
        {
            "Produkt,Symbol/ISIN,Anzahl,Schlußkurs,Wert,Wert in EUR",
            "CASH & CASH FUND & FTX CASH (EUR),,,,EUR -0.56,\"-0,56\"",
            "ADOBE SYSTEMS INCORPOR,US00724F1012,3,\"123,45\",USD 1234.12,\"1234,12\"",
            "AIRBNB INC-CLASS A,US0090661010,6,\"12,05\",USD 123.30,\"123,21\""
        };


        [Test]
        public void Import()
        {
            TopCsv topCsv = new TopCsv();
            var results = topCsv.Get<PortfolioEntry>(new ReaderForTests(lines)).ToList();
            Assert.That(results.Count(), Is.EqualTo(3));
            Assert.That("US00724F1012", Is.EqualTo(results[0].SymbolISIN));
            Assert.That(123.45M, Is.EqualTo(results[0].ClosingPrice));
            Assert.That(123.45M, Is.EqualTo(results[0].Value.Amount));
        }

    }
}
