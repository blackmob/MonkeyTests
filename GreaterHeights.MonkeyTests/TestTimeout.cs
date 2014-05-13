using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreaterHeights.MonkeyTests
{
    public static class TestTimeout
    {
        private static bool firstTime = true;
        public static TimeSpan Timeout
        {
            get
            {
                if (firstTime)
                {
                    firstTime = false;
                    Console.WriteLine("First time of asking - returning long timeout of 30 seconds");
                    return TimeSpan.FromSeconds(30);
                }

                return TimeSpan.FromSeconds(20);
            }
        }
    }
}
