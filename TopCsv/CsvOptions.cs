using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopCsvProject
{
    /// <summary>Class that specifies csv read options</summary>
    public class CsvOptions
    {
        /// <summary>Gets or sets a value defining whether the the csv file has a header line or not.</summary>
        public bool HasHeader { get; set; }

        /// <summary>Value that separates the entries</summary>
        public char Separator { get; set; }
    }
}
