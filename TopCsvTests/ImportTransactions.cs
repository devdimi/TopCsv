using MinimalFileSystemApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopCsvProject;
using Csv = TopCsvProject.TopCsvConverterTypes;
namespace TopCsvTests
{
    /* <summary>
Datum,Uhrzeit,Produkt,ISIN,Referenzbörse,Ausführungsort,Anzahl,Kurs,,Wert in Lokalwährung,,Wert,,Wechselkurs,Transaktionsgebühren,,Gesamt,,Order-ID
29-06-2022,00:00,SHOPIFY INC.CLASS A S, CA82509L1076, NSY,,60,35.0260, USD,-2101.56, USD,-2014.13, EUR,1.0434,,,-2014.13, EUR,
29-06-2022,00:00, SHOPIFY INC. CLASS A S, CA82509L1076, NSY,,-6,350.2600, USD,2101.56, USD,2014.13, EUR,1.0434,,,2014.13, EUR,
06-06-2022,00:00, AMAZON.COM INC. - COM, US0231351067, NDQ,,20,122.3500, USD,-2447.00, USD,-2345.20, EUR,1.0434,,,-2345.20, EUR,
06-06-2022,00:00, AMAZON.COM INC. - COM, US0231351067, NDQ,,-1,2447.0000, USD,2447.00, USD,2345.20, EUR,1.0434,,,2345.20, EUR,
20-05-2022,17:25, CONFLUENT INC. - CLASS A COMMON STOCK, US20717M1036, NDQ, ARCX,4,17.9900, USD,-71.96, USD,-68.22, EUR,1.0549,-0.50, EUR,-68.72, EUR,12cb1021-f15d-4fbe-b1e7-46761af52029
20-05-2022,17:24, TAKE-TWO INTERACTIVE S, US8740541094, NDQ, SOHO,2,118.8800, USD,-237.76, USD,-225.39, EUR,1.0549,-0.50, EUR,-225.89, EUR, db34e3e0-629d-4353-8de5-392d084e20b7
*/

    public class Transaction : CsvBaseRecord
    {
        [CsvField(AllowEmpty = false, Converter = TopCsvConverterTypes.DateConverterDD_MM_YYYY, Header = "Datum")]
        public DateTime Date { get; set; }

        [CsvField(AllowEmpty = false, Converter = TopCsvConverterTypes.TimeConverter, Header = "Uhrzeit")]
        public DateTime Time { get; set; }

        [CsvField(AllowEmpty = false, Converter = TopCsvConverterTypes.StringConverter, Header = "Produkt")]
        public String Product { get; set; }

        [CsvField(AllowEmpty = false, Converter = TopCsvConverterTypes.StringConverter, Header = "ISIN")]
        public String ISIN {  get; set; }

        [CsvField(AllowEmpty = false, Converter = TopCsvConverterTypes.StringConverter, Header = "Referenzbörse")]
        public String Exchange { get; set; }

        [CsvField(AllowEmpty = true, Converter = TopCsvConverterTypes.StringConverter, Header = "Ausführungsort")]
        public String Place { get; set; }


        [CsvField(AllowEmpty = false, Converter = TopCsvConverterTypes.IntConverter, Header = "Anzahl")]
        public Int32 NumShares { get; set; }

        [CsvField(AllowEmpty = true, Converter = TopCsvConverterTypes.DecimalConverterDot, Header = "Kurs")]
        public decimal Price { get; set; }

        [CsvField(AllowEmpty = true, Converter = TopCsvConverterTypes.CurrencyEnumConverter, Header = "")]
        public Currency LocalCurrency { get; set; }

        [CsvField(AllowEmpty = true, Converter = TopCsvConverterTypes.DecimalConverterDot, Header = "Wert in Lokalwährung",
            Description = "Value in local currency of the transaction, USD for US exchanges")]
        public decimal ValueInLocalCurrency { get; set; }

        [CsvField(AllowEmpty = true, Converter = TopCsvConverterTypes.CurrencyEnumConverter, Header = "", Description = "")]
        public Currency ValueCurrency { get; set; }

        [CsvField(Converter = Csv.DecimalConverterDot, Header = "Wert", Description = "Wert", AllowEmpty = true)]
        public decimal Value { get; set; }

        [CsvField(AllowEmpty = true, Converter = TopCsvConverterTypes.CurrencyEnumConverter, Header = "", Description = "")]
        public Currency ExchangeRateCurrency { get; set; }

        [CsvField(Converter = Csv.DecimalConverterDot, Header = "Wechselkurs", Description = "Exchange rate of transaction currency and portfolio currency, e.g. USD/EUR", AllowEmpty = true)]
        public decimal ExchangeRate { get; set; }

        [CsvField(Header = "Transaktionsgebühren", Description = "Cost of the transaction", Converter = Csv.DecimalConverterDot, AllowEmpty = true )]
        public decimal TransactionCost { get; set; }

        [CsvField(Header = "", Description = "Currenty of the transaction cost", Converter = Csv.CurrencyEnumConverter, AllowEmpty = true)]
        public Currency TransactionCurrency { get; set; }

        [CsvField(Header = "Gesamt", Description = "Full cost", Converter = Csv.DecimalConverterDot, AllowEmpty = true)]
        public decimal FullCost { get; set; }

        [CsvField(Header = "", Description = "Currenty of full cost", Converter = Csv.CurrencyEnumConverter, AllowEmpty = true)]
        public Currency FullCostCurrency { get; set; }

        [CsvField(Header = "Order-ID", Description = "The Order-ID", Converter = Csv.StringConverter, AllowEmpty = true)]
        public String OrderId { get; set; }
    }
    /*
    Datum,Uhrzeit,Produkt,ISIN,Referenzbörse,Ausführungsort,Anzahl,Kurs,,Wert in Lokalwährung,,Wert,,Wechselkurs,Transaktionsgebühren,,Gesamt,,Order-ID
    29-06-2022,00:00,SHOPIFY INC.CLASS A S, CA82509L1076, NSY,,60,35.0260, USD,-2101.56, USD,-2014.13, EUR,1.0434,,,-2014.13, EUR,
    20-05-2022,17:25, CONFLUENT INC. - CLASS A COMMON STOCK, US20717M1036, NDQ, ARCX,4,17.9900, USD,-71.96, USD,-68.22, EUR,1.0549,-0.50, EUR,-68.72, EUR,12cb1021-f15d-4fbe-b1e7-46761af52029

     */

    [TestFixture]
    public class ImportTransactions
    {

        [Test]
        public void ImportFull()
        {
            TopCsv topCsv = new TopCsv();
            ReaderForTests reader = new ReaderForTests(
                "Datum,Uhrzeit,Produkt,ISIN,Referenzbörse,Ausführungsort,Anzahl,Kurs,,Wert in Lokalwährung,,Wert,,Wechselkurs,Transaktionsgebühren,,Gesamt,,Order-ID",
                "29-06-2022,00:00,SHOPIFY INC. CLASS A S,CA82509L1076,NSY,,60,35.0260,USD,-2101.56,USD,-2014.13,EUR,1.0434,,,-2014.13,EUR,",
                "29-06-2022,00:00,SHOPIFY INC. CLASS A S,CA82509L1076,NSY,,-6,350.2600,USD,2101.56,USD,2014.13,EUR,1.0434,,,2014.13,EUR",
                "20-05-2022,17:25,CONFLUENT INC. - CLASS A COMMON STOCK, US20717M1036, NDQ, ARCX,4,17.9900, USD,-71.96, USD,-68.22, EUR,1.0549,-0.50, EUR,-68.72, EUR,12cb1021-f15d-4fbe-b1e7-46761af52029");
            var list = topCsv.Get<Transaction>(reader).ToList();
            Assert.AreEqual(3, list.Count());
            Assert.AreEqual(new DateTime(2022, 6, 29), list[0].Date);

            Assert.AreEqual(0, list[0].Time.Hour);
            Assert.AreEqual(0, list[0].Time.Minute);
            Assert.AreEqual("SHOPIFY INC. CLASS A S", list[0].Product);
            Assert.AreEqual("CA82509L1076", list[0].ISIN);
            Assert.AreEqual("NSY", list[0].Exchange);
            Assert.AreEqual(String.Empty, list[0].Place);
            Assert.AreEqual(60, list[0].NumShares);


            Assert.AreEqual(new DateTime(2022, 5, 20), list[2].Date);
            Assert.AreEqual(17, list[2].Time.Hour);
            Assert.AreEqual(25, list[2].Time.Minute);
            Assert.AreEqual("CONFLUENT INC. - CLASS A COMMON STOCK", list[2].Product);         
            Assert.AreEqual("US20717M1036", list[2].ISIN.Trim());
            Assert.AreEqual("NDQ", list[2].Exchange.Trim());
            Assert.AreEqual(4, list[2].NumShares);
            Assert.AreEqual("ARCX", list[2].Place.Trim());
            Assert.AreEqual(4, list[2].NumShares);

        }

        [Test]
        public void ImportDateAndTime()
        {
            TopCsv topCsv = new TopCsv();
            ReaderForTests reader = new ReaderForTests("Datum,Uhrzeit", "29-06-2022,00:00", "20-06-2022,17:25");
            var list = topCsv.Get<Transaction>(reader).ToList();
            Assert.AreEqual(2, list.Count());
            Assert.AreEqual(new DateTime(2022, 6, 29), list[0].Date);
            Assert.AreEqual(0, list[0].Time.Hour);
            Assert.AreEqual(0, list[0].Time.Minute);

            Assert.AreEqual(new DateTime(2022, 6, 20), list[1].Date);
            Assert.AreEqual(17, list[1].Time.Hour);
            Assert.AreEqual(25, list[1].Time.Minute);
        }
    }
}
