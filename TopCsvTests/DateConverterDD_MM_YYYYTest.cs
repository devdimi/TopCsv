using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopCsvProject.Converter;

namespace TopCsvTests
{
    [TestFixture]
    public class DateConverterDD_MM_YYYYTest
    {
        [Test]
        public void TestParse()
        {
            DateConverterDD_MM_YYYY converter = new DateConverterDD_MM_YYYY(new ConsoleLogger());
            DateTime parsed = converter.FromStringTyped("24-05-2020");
            Assert.AreEqual(24, parsed.Day);
            Assert.AreEqual(5, parsed.Month);
            Assert.AreEqual(2020, parsed.Year);
        }
    }
}
