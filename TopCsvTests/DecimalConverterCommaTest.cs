using TopCsvProject;

namespace TopCsvTests
{
    [TestFixture]
    public class DecimalConverterCommaTest
    {
        [Test]
        public void TestConvertComma()
        {
            DecimalConverterComma converter = new DecimalConverterComma(DecimalSeparator.Comma);
            decimal converted = converter.FromStringTyped("103,23");
            Assert.That(converted, Is.EqualTo(103.23m));
        }

        [Test]
        public void TestConvertDot()
        {
            DecimalConverterComma converter = new DecimalConverterComma(DecimalSeparator.Dot);
            decimal converted = converter.FromStringTyped("103.23");
            Assert.That(converted, Is.EqualTo(103.23m));
        }
    }
}
