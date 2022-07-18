using TopCsvProject;

namespace TopCsvTests
{
    [TestFixture]
    public class DecimalConverterCommaTest
    {
        [Test]
        public void TestConvert()
        {
            DecimalConverterComma converter = new DecimalConverterComma();
            decimal converted = converter.FromStringTyped("103,23");
            Assert.That(converted, Is.EqualTo(103.23m));
        }
    }
}
