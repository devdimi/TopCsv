using MinimalFileSystemApi;
using MinimalFileSystemApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopCsvProject
{
    public class TopCsv
    {
        private CsvOptions options;

        private Dictionary<TopCsvConverterTypes, IConverter> map = new Dictionary<TopCsvConverterTypes, IConverter>()
        {
            { TopCsvConverterTypes.IntConverter, new Int32Converter() },
            { TopCsvConverterTypes.StringConverter, new StringConverter() },
            { TopCsvConverterTypes.MoneyConverterNoCurrencyComma, new DecimalConverterComma() },
            { TopCsvConverterTypes.MoneyConverterCurrencyDot, new MoneyConverter() },
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
                Separator = ','
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
                //// ToDo split with span
                ////var lineSpan = line.AsSpan();
                var parts = line.Split(this.options.Separator);
                T entry = new T();
                for(Int32 i = 0; i < parts.Length; i++)
                {
                    var val = parts[i];
                    object convertedValue = val;
                    if (attributes[i].Converter != TopCsvConverterTypes.None )
                    {
                        if (!String.IsNullOrEmpty(val))
                        {
                            convertedValue = this.map[attributes[i].Converter].FromString(val);
                        } 
                        else
                        {
                            convertedValue = this.map[attributes[i].Converter].Default;
                        }
                    }

                    properties[i].SetValue(entry, convertedValue);

                }

                yield return entry;
            }
        }

        public static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }
    }
}
