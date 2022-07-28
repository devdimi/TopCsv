using MinimalFileSystemApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopCsvProject;

namespace TopCsvTests
{
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
            var list = topCsv.Get<TransactionDegiro>(reader).ToList();
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
            var list = topCsv.Get<TransactionDegiro>(reader).ToList();
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
