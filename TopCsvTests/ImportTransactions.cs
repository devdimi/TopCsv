using MinimalFileSystemApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopCsvProject;

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
        [CsvField(AllowEmpty =false, Converter = TopCsvConverterTypes.DateConverterDD_MM_YYYY, Header = "Datum")]
        public DateTime Date { get; set; }
    }

    [TestFixture]
    public class ImportTransactions
    {

        [Test]
        public void Import()
        {
            TopCsv topCsv = new TopCsv();
            ReaderForTests reader = new ReaderForTests("Datum", "29-06-2022", "20-06-2022");
            var list = topCsv.Get<Transaction>(reader).ToList();
            Assert.AreEqual(2, list.Count());
            Assert.AreEqual(new DateTime(2022, 6, 29), list[0].Date);
            Assert.AreEqual(new DateTime(2022, 6, 20), list[1].Date);
        }
    }
}
