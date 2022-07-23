using System.Globalization;

namespace TopCsvProject
{
    public class MoneyConverter : IConverter<Money>
    {
        public object FromString(ReadOnlySpan<char> input) => FromStringTyped(input);

        CultureInfo ci = new CultureInfo("en-US");

        public Money FromStringTyped(ReadOnlySpan<char> input)
        {
            Int32 indexOfSpace = input.IndexOf(' ');
            var strCurrency = input.Slice(0, indexOfSpace);
            var strMoney = input.Slice(indexOfSpace + 1);
            object currencyObj;
            
            if(!Enum.TryParse(typeof(Currency), strCurrency, out currencyObj))
            {
                // log
            }

            decimal result;
            var style = NumberStyles.Any;
            
            if (!decimal.TryParse(strMoney, style, this.ci, out result))
            {
                //// logging
            }

            Currency currency = (Currency)currencyObj;
            return new Money(currency, result);
        }

        object IConverter.Default => new Money(Currency.Unknown, 0.0m);
    }
}
