using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopCsvProject;
namespace TopCsvTests
{
    public class DateConverterYYYY_MM_DD_HH_mm_SSTest
    {
        [Test]
        public void TestParse()
        {
            String str = "";
            DateConverterYYYY_MM_DD_HH_mm_SS converter = new DateConverterYYYY_MM_DD_HH_mm_SS();

            str = "2021-07-05 14:04:38";
            DateTime dateTime = converter.FromStringTyped(str.AsSpan());
            Assert.AreEqual(new DateTime(2021, 7, 5, 14, 04, 38), dateTime);

            str = "2021-07-05 11:25:05";
            dateTime = converter.FromStringTyped(str.AsSpan());
            Assert.AreEqual(new DateTime(2021, 7, 5, 11, 25, 05), dateTime);
        }
    }
}
