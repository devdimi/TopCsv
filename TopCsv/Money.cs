using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopCsvProject
{
    /// <summary>The infamous money class as presented by Kent Beck.
    /// It holds currency and amount
    /// </summary>
    public class Money
    {
        public Money(Currency currency, decimal amount)
        {
            this.Currency = currency;
            this.Amount = amount;
        }

        public Currency Currency { get; private set; }

        public decimal Amount { get; private set; }
    }
}
