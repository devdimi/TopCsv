namespace TopCsvProject
{
    public enum ParseMode
    {
        BreakOnFirstError,
        ParseWhatIsPossibleLogErrors
    }

    /// <summary>Class that specifies csv read options</summary>
    public class CsvOptions
    {
        /// <summary>Gets or sets a value defining whether the the csv file has a header line or not.</summary>
        public bool HasHeader { get; set; }

        /// <summary>Gets or sets the separator characters</summary>
        public char[] Separators { get; set; }

        /// <summary>Gets or sets the escaoe chars
        /// used when value contains e.g. the separator char</summary>
        public char[] EscapeChars { get; set; }

        /// <summary>Gets or sets the parse mode </summary>
        public ParseMode ParseMode { get; set; }
    }
}
