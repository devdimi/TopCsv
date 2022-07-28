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
            var results = topCsv.Get<PortfolioEntryDegiro>(new ReaderForTests(lines)).ToList();

            Assert.That(results.Count(), Is.EqualTo(3));
            
            Assert.That(-0.56M, Is.EqualTo(results[0].MoneyAmount.Amount));
            Assert.That(results[0].MoneyAmount.Currency, Is.EqualTo(Currency.EUR));

            Assert.That(results[0].ValueInEUR, Is.EqualTo(-0.56M));
           
            Assert.That(results[1].SymbolISIN, Is.EqualTo("US00724F1012"));
            Assert.That(results[1].ClosingPrice, Is.EqualTo(123.45M));
            Assert.That(results[1].MoneyAmount.Amount, Is.EqualTo(1234.12M));

            Assert.That(results[2].SymbolISIN, Is.EqualTo("US0090661010"));
            Assert.That(results[2].ClosingPrice, Is.EqualTo(12.05M));
            Assert.That(results[2].MoneyAmount.Amount, Is.EqualTo(123.30M));
        }

    }
}
