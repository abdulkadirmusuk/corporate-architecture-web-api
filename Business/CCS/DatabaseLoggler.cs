using System;

namespace Business.CCS
{
    public class DatabaseLoggler : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Veritabanına loglandı");
        }
    }
}
