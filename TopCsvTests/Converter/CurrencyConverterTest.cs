using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopCsvProject;

namespace TopCsvTests
{
    [TestFixture]
    public class CurrencyConverterTest
    {
        [Test]
        public void TestConvert()
        {
            CurrencyEnumConverter converter = new CurrencyEnumConverter();
            Currency currency = converter.FromStringTyped("USD".AsSpan());
            Assert.AreEqual(Currency.USD, currency);

            currency = converter.FromStringTyped("EUR".AsSpan());
            Assert.AreEqual(Currency.EUR, currency);
        }
    }
}
