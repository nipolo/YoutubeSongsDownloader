using System;
using System.IO;

namespace YoutubeSongsDownloader
{
    internal class ConsoleFileLogger : StreamWriter, IDisposable
    {
        public ConsoleFileLogger(string logFilePath, bool append) 
            : base(logFilePath, append)
        {
        }

        public override void WriteLine(string line)
        {
            base.WriteLine(line);
            Console.WriteLine(line);
        }
    }
}