using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopCsvProject
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]  
    public class CsvFieldAttribute : Attribute
    {
        public string Header { get; set; }

        public TopCsvConverterTypes Converter { get; set; }

        public Boolean AllowEmpty { get; set; }
    }
}
