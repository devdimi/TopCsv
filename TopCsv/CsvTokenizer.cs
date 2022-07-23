using System.Collections;

namespace TopCsvProject
{
    public static class StringExtensions
    {
        public static CsvTokensEnumerator GetTokens(this string str, char[] separators, char[] escapechars)
        {
            // LineSplitEnumerator is a struct so there is no allocation here
            var r = new CsvTokensEnumerator(str.AsSpan(), separators, escapechars) { };
            return r;
        }
    }

    /// <summary>
    /// This class implements tokenizer user ReadOnlySpan<char>
    /// </summary>
    public ref struct CsvTokensEnumerator
    {
        private ReadOnlySpan<char> remainingStringToTokenize;
        char[] separators;
        char[] escapechars;
        public ReadOnlySpan<char> Current { get; private set; }

        public CsvTokensEnumerator(ReadOnlySpan<char> stringToTokenize, char[] separators, char[] escapechars)
        {
            this.remainingStringToTokenize = stringToTokenize;
            this.Current = default;
            this.separators = separators;
            this.escapechars = escapechars;
        }

        // Needed to be compatible with the foreach operator
        public CsvTokensEnumerator GetEnumerator() => this;
        

        public bool MoveNext()
        {
            bool inToken = false;
            ReadOnlySpan<char> span = this.remainingStringToTokenize;
            int startOfToken = 0;
            
            if (span.Length == 0)
            {
                return false;
            }

            ///for 11,"2,2",33, tokens are 11 -> 2,2 -> 33 -> <empty token>
            for (Int32 i = 0; i < span.Length; i++)
            {
                if (this.separators.Contains(span[i]))
                {
                    if (inToken)
                    {
                        continue;
                    }

                    this.Current = CreateNewSlice(span, startOfToken, i);
                    return true;
                }
                else if (this.escapechars.Contains(span[i]))
                {
                    inToken = !inToken;
                    if (inToken)
                    {
                        //// skip first char as it is escape character
                        startOfToken = i + 1;
                    }

                    if(i == this.remainingStringToTokenize.Length - 1 && !inToken)
                    {
                        this.Current = CreateNewSlice(span, startOfToken, i);
                        return true;
                    }
                }
            }

            if(span.Length > 0)
            {
                //// one token, no separators
                this.Current = span.Slice(0, span.Length);
                this.remainingStringToTokenize = new ReadOnlySpan<char>(); 
                return true;
            }

            return false;
        }

        private ReadOnlySpan<char> CreateNewSlice(
            ReadOnlySpan<char> span, 
            int startOfToken,
            int i)
        {
            int length = i - startOfToken;
            if (length == 0)
            {
                Current = new ReadOnlySpan<char>();
                this.remainingStringToTokenize = span.Slice(i + 1);
                return Current;
            }

            var lastCharOfTokenIndex = startOfToken + length - 1;
            if (lastCharOfTokenIndex >= 0 &&
                span.Length > lastCharOfTokenIndex &&
                this.escapechars.Contains(span[lastCharOfTokenIndex]))
            {
                //// skip last char as it is escape character
                length--;
            }

            var newSlice = span.Slice(startOfToken, length);
            startOfToken = i;
            Current = newSlice;
            if (i == span.Length - 1 && this.separators.Contains(span[span.Length - 1]))
            {
                /// special handling of case "3," ending with separator
                /// in this case we keep the separator to emit one more empty token
                this.remainingStringToTokenize = span.Slice(startOfToken);
            }
            else
            {
                this.remainingStringToTokenize = span.Slice(startOfToken + 1);
            }

            return Current;
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
