using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopCsvProject
{
    public enum DecimalSeparator
    {
        Comma,
        Dot
    }

    public class DecimalConverterComma : IConverter<decimal>
    {
        DecimalSeparator decimalSeparator;
        private CultureInfo dotCultureInfo, commaCultureInfo;

        public DecimalConverterComma(DecimalSeparator decimalSeparator)
        {
            this.decimalSeparator = decimalSeparator;
        }

        public object FromString(ReadOnlySpan<char> input) => this.FromStringTyped(input);

        public CultureInfo GetCultureInfo(DecimalSeparator decimalSeparator)
        {
            if(decimalSeparator == DecimalSeparator.Dot)
            {
                if(this.dotCultureInfo == null)
                {
                    this.dotCultureInfo = new CultureInfo("en-US");
                }

                return this.dotCultureInfo;
            }

            if(decimalSeparator == DecimalSeparator.Comma)
            {
                if(this.commaCultureInfo == null)
                {
                    this.commaCultureInfo = new CultureInfo("de-DE");
                }

                return this.commaCultureInfo;
            }

            throw new NotSupportedException($"Not supported value: {decimalSeparator}");
        }

        public decimal FromStringTyped(ReadOnlySpan<char> input)
        {
            decimal result;
            var style = NumberStyles.Any;
            var ci = this.GetCultureInfo(this.decimalSeparator);
            if (!decimal.TryParse(input, style, ci, out result))
            {
                //// logging
            }

            return result;
        }

        object IConverter.Default => 0.0m;
    }
}
