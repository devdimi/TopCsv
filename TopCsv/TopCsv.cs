using MinimalFileSystemApi.Interfaces;


namespace TopCsvProject
{
    public class TopCsv
    {
        private CsvOptions options;

        private Dictionary<TopCsvConverterTypes, IConverter> map = new Dictionary<TopCsvConverterTypes, IConverter>()
        {
            { TopCsvConverterTypes.IntConverter, new Int32Converter() },
            { TopCsvConverterTypes.StringConverter, new StringConverter() },
            { TopCsvConverterTypes.DecimalConverterComma, new DecimalConverterComma(DecimalSeparator.Comma) },
            { TopCsvConverterTypes.DecimalConverterDot, new DecimalConverterComma(DecimalSeparator.Dot) },
            { TopCsvConverterTypes.MoneyConverterCurrencyDot, new MoneyConverter() },
            { TopCsvConverterTypes.DateConverterDD_MM_YYYY, new DateConverterDD_MM_YYYY(new ConsoleLogger()) },
            { TopCsvConverterTypes.TimeConverter, new TimeConverter(new ConsoleLogger()) },
            { TopCsvConverterTypes.CurrencyEnumConverter,  new CurrencyEnumConverter() },
        };

        public TopCsv(CsvOptions options)
        {
            this.options = options;
        }

        public TopCsv()
        {
            this.options = new CsvOptions()
            {
                HasHeader = true,
                Separators = new[] { ',' },
                EscapeChars = new[] { '"' }
            };
        }

        public IEnumerable<T> Get<T> (String fileName) where T : CsvBaseRecord
        {
            yield break;
        }

        public IEnumerable<T> Get<T>(ILineReader reader) where T : CsvBaseRecord, new()
        {
            Type type = typeof(T);
            var properties = type.GetProperties().ToList();
            List<CsvFieldAttribute> attributes = new List<CsvFieldAttribute>();
            foreach (var property in properties)
            {
                var attrs = property.GetCustomAttributes(typeof(CsvFieldAttribute), inherit: false);
                if (attrs != null && attrs.Any())
                {
                    attributes.Add(attrs.Single() as CsvFieldAttribute);
                }
            }

            if (this.options.HasHeader)
            {
                String header = reader.ReadLine();  // skip header.
                ////var headerFields = header.Split(',');
                ////for(Int32 i = 0; i < headerFields.Length; i++)
                ////{
                ////    var field = headerFields[i];
                ////    Int32 index = attributes.FindIndex(x => x.Header == field);
                ////    attributes.Swap(i, index);
                ////}
            }

            String line;
            while(null != (line = reader.ReadLine())) 
            {
                CsvTokensEnumerator enumerator = 
                    new CsvTokensEnumerator(
                        line, 
                        this.options.Separators,
                        this.options.EscapeChars
                        );
                Int32 i = 0;
                T entry = new T();
                while (enumerator.MoveNext())
                {
                    ReadOnlySpan<char> part = enumerator.Current;
                    object convertedValue = null;
                    if (attributes[i].Converter != TopCsvConverterTypes.None)
                    {
                        if (!part.IsEmpty)
                        {
                            convertedValue = this.map[attributes[i].Converter].FromString(part);
                        }
                        else
                        {
                            convertedValue = this.map[attributes[i].Converter].Default;
                        }
                    }

                    properties[i].SetValue(entry, convertedValue);
                    i++;
                }

                yield return entry;
            }
        }
    }
}
