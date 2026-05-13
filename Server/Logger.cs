using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class Logger
    {
        public static void Log(string massage, string loggerRepo = "Server")
        {
            var nowTime = DateTime.Now;
            string text = $"[{nowTime}][{loggerRepo}] {massage}";

            Console.WriteLine(text);
        }
    }
}
