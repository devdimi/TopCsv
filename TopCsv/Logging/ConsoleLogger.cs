namespace TopCsvProject
{
    public class ConsoleLogger : ITopCsvLogger
    {
        public void Log(LogFlags flags, String msg)
        {
            Console.WriteLine(msg);
        }
    }
}
