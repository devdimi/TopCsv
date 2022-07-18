using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopCsvProject;

namespace TopCsvTests
{
    [TestFixture]
    public class MoneyConverterTest
    {
        [Test]
        public void TestConvert()
        {
            MoneyConverter converter = new MoneyConverter();
            Money converted = converter.FromStringTyped("USD 103.23");
            Assert.That(converted.Amount, Is.EqualTo(103.23m));
            Assert.That(converted.Currency, Is.EqualTo(Currency.USD));
        }
    }
}
