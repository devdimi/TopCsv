namespace TopCsvProject
{
    [Flags]
    public enum LogFlags
    {
        ParseError,
        DateError,
        TimeError
    }

    public interface ITopCsvLogger
    {
        void Log(LogFlags flags, string msg);
    }
}
