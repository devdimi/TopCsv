using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopCsvProject
{
    public class CsvTokenizer
    {
        char[] separators;
        char[] escapechars;
        public CsvTokenizer(char[] separators, char[] escapechars)
        {
            this.separators = separators;
            this.escapechars = escapechars;
        }
    }
    public static class StringExtensions
    {
        public static CsvTokensEnumerator GetTokens(this string str, char[] separators, char[] escapechars)
        {
            // LineSplitEnumerator is a struct so there is no allocation here
            var r = new CsvTokensEnumerator(str.AsSpan(), separators, escapechars) { };
            return r;
        }
    }

    public ref struct CsvTokensEnumerator
    {
        private ReadOnlySpan<char> _str;
        char[] separators;
        char[] escapechars;

        public CsvTokensEnumerator(ReadOnlySpan<char> str, char[] separators, char[] escapechars)
        {
            _str = str;
            Current = default;
            this.separators = separators;
            this.escapechars = escapechars;
        }

        // Needed to be compatible with the foreach operator
        public CsvTokensEnumerator GetEnumerator() => this;
        public ReadOnlySpan<char> Current { get; private set; }

        public bool MoveNext()
        {
            bool inToken = false;
            ReadOnlySpan<char> span = this._str;
            int startOfToken = 0;
            Int32 length;
            if(span.Length == 0)
            {
                return false;
            }

            for (Int32 i = 0; i < span.Length; i++)
            {
                if (this.separators.Contains(span[i]))
                {
                    if (inToken)
                    {
                        continue;
                    }

                    length = i - startOfToken;
                    if(length == 0)
                    {
                        startOfToken = i+1;
                        continue;
                    }


                    if (span.Length > startOfToken + length - 1  && this.escapechars.Contains(span[startOfToken+length-1]))
                    {
                        length--;
                    }

                    var newSlice = span.Slice(startOfToken, length);
                    startOfToken = i;
                    Current = newSlice;
                    ////if (i == span.Length - 1)
                    ////{
                    ////    this._str = new ReadOnlySpan<char>();
                    ////}
                    ////else
                    {
                        this._str = span.Slice(startOfToken);
                    }

                    return true;
                }
                else if (this.escapechars.Contains(span[i]))
                {
                    inToken = !inToken;
                    if(inToken)
                        startOfToken = i + 1;
                }
            }

            length = span.Length - startOfToken;
            var r = span.Slice(startOfToken, length);
            Current = r;
            this._str = new ReadOnlySpan<char>();
            return true;
        }
    }

    public readonly ref struct CsvToken
    {
        public CsvToken(ReadOnlySpan<char> token)
        {
            Token = token;
        }

        public ReadOnlySpan<char> Token { get; }
      
        // This method allow to deconstruct the type, so you can write any of the following code
        // foreach (var entry in str.SplitLines()) { _ = entry.Line; }
        // foreach (var (line, endOfLine) in str.SplitLines()) { _ = line; }
        // https://docs.microsoft.com/en-us/dotnet/csharp/deconstruct?WT.mc_id=DT-MVP-5003978#deconstructing-user-defined-types
        public void Deconstruct(out ReadOnlySpan<char> token)
        {
            token = this.Token;
        }

        // This method allow to implicitly cast the type into a ReadOnlySpan<char>, so you can write the following code
        // foreach (ReadOnlySpan<char> entry in str.SplitLines())
        public static implicit operator ReadOnlySpan<char>(CsvToken entry) => entry.Token;
    }
}
