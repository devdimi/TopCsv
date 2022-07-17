using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopCsvProject
{
    public interface IConverter
    {
        public object FromString(String input);
    }

    public interface IConverter<T> : IConverter
    {
        public T FromStringTyped(String input);
    }
}
