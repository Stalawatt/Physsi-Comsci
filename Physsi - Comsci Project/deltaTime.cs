using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physsi___Comsci_Project
{
    public class deltaTime
    {
        private static DateTime time1;
        private static DateTime time2;

        public static void Start() // takes the time in ticks
        {
            time1 = DateTime.Now;
        }

        public static void End()  // sets the start time to the end time
        {
            time1 = time2;
        }

        public static float GetDeltaTime() // returns the time between Start() and End() calls
        {
            return (float)((time2.Ticks - time1.Ticks) / 10000000f);
        }

    }
}
