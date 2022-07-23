using System;
using TopCsvProject;

namespace TopCsvTests
{
    [TestFixture]
    public class TimeConverterTests
    {
        [Test]
        public void TestConvertTime()
        {
            TimeConverter timeConverter = new TimeConverter(new ConsoleLogger());
            DateTime parsed = timeConverter.FromStringTyped("14:45".AsSpan());
            Assert.AreEqual(14, parsed.Hour);
            Assert.AreEqual(45, parsed.Minute);


            parsed = timeConverter.FromStringTyped("02:01".AsSpan());
            Assert.AreEqual(2, parsed.Hour);
            Assert.AreEqual(1, parsed.Minute);
        }
    }
}
