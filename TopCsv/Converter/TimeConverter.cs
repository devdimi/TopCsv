namespace TopCsvProject
{
    public class TimeConverter : IConverter<DateTime>
    {
        public object Default => default(DateTime);

        private ITopCsvLogger logger;

        public TimeConverter(ITopCsvLogger logger)
        {
            this.logger = logger;
        }

        public object FromString(ReadOnlySpan<char> input) => FromStringTyped(input);

        public DateTime FromStringTyped(ReadOnlySpan<char> input)
        {
            //00:00
            var hourSlice  = input.Slice(0, 2);
            var minuteSlie = input.Slice(3, 2);
            Int32 hour, minute;
            if(!int.TryParse(hourSlice, out hour))
            {
                this.logger.Log(LogFlags.ParseError, $"Error parsing {hourSlice.ToString()} from {input}");
                return default(DateTime);
            }

            if (!int.TryParse(minuteSlie, out minute))
            {
                this.logger.Log(LogFlags.ParseError, $"Error parsing {hourSlice.ToString()} from {input}");
                return default(DateTime);
            }

            DateTime defaultDateTime = default(DateTime);
            DateTime result = new DateTime(
                defaultDateTime.Year, defaultDateTime.Month, defaultDateTime.Day,
                hour, minute, second: 0);
            return result;
        }
    }
}
