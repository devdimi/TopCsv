using MinimalFileSystemApi;
using System.Collections;
using TopCsvProject;

namespace TopCsvTests
{
    public class GameEntry : CsvBaseRecord
    {
        [CsvField(Header = "Game Number", Converter = TopCsvConverterTypes.IntConverter)]
        public Int32 GameNumber { get; set; }

        [CsvField(Header = "Game Length", Converter = TopCsvConverterTypes.IntConverter)]
        public Int32 GameLength { get; set; }

        [CsvField(Header = "Game Name", Converter = TopCsvConverterTypes.StringConverter)]
        public String GameName { get; set; }
    }   

    public class Tests
    {
        [Test]
        public void GameTest()
        {
            TopCsv topCsv = new TopCsv();
            
            ReaderForTests reader = new ReaderForTests(new[] {
            "\"Game Number\", \"Game Length\", \"Game Name\"",
            "1, 30, \"The first game\"",
            "2, 29, \"The second game\"",
            "3, 31, \"The third game\""
            });
            var gameEntries = topCsv.Get<GameEntry>(reader).ToList();
            Assert.That(gameEntries.Count, Is.EqualTo(3));
            Assert.That(gameEntries[0].GameNumber, Is.EqualTo(1));
            Assert.That(gameEntries[0].GameName, Is.EqualTo("The first game"));
        }
    }
}