using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopCsvProject
{
    public interface IConverter
    {
        public object FromString(ReadOnlySpan<char> input);

        public object Default { get; }
    }

    public interface IConverter<T> : IConverter
    {
        public T FromStringTyped(ReadOnlySpan<char> input);
    }
}
